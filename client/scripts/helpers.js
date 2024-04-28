const HELPERS_MODULE = (function () {
  function formatServerResponse(response) {
    response.Steps = orderSteps(response.Steps)
    return response
  }

  function setTooltipText(text) {
    d3.select('#tooltip').text(text)
  }
  function setResultText(text) {
    d3.select('#result').text(text)
  }

  function orderSteps(steps) {
    const result = []

    let first = steps[0].Best
    let second = steps[0].Good
    let third = steps[0].Worst

    for (let i = 0; i < steps.length; i++) {
      const step = steps[i]
      let stepPoints = Object.values(step)
      const changedPoint = stepPoints.find(
        (point) => !areEqual(point, first) && !areEqual(point, second) && !areEqual(point, third)
      )
      if (!doPointsIncludePoint(stepPoints, first)) {
        first = changedPoint
      } else if (!doPointsIncludePoint(stepPoints, second)) {
        second = changedPoint
      } else if (!doPointsIncludePoint(stepPoints, third)) {
        third = changedPoint
      }
      result.push([first, second, third])
    }
    return result
  }

  function areEqual(point1, point2) {
    return JSON.stringify(point2) === JSON.stringify(point1)
  }

  function doPointsIncludePoint(points, point) {
    return points.map(JSON.stringify).includes(JSON.stringify(point))
  }

  return {
    formatServerResponse,
    setTooltipText,
    setResultText,
  }
})()
