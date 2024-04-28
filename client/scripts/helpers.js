const HELPERS_MODULE = (function () {
  function formatServerResponse(response) {
    response.Steps = orderSteps(response.Steps)
    return response
  }

  function orderSteps(steps) {
    const result = []

    let first = steps[0].Best
    let second = steps[0].Good
    let third = steps[0].Worst

    for (let i = 0; i < steps.length; i++) {
      const step = steps[i]
      points = Object.values(step)
      if (!points.map(JSON.stringify).includes(JSON.stringify(first))) {
        first = points.find(
          (point) =>
            JSON.stringify(point) !== JSON.stringify(first) &&
            JSON.stringify(point) !== JSON.stringify(second) &&
            JSON.stringify(point) !== JSON.stringify(third)
        )
      } else if (!points.map(JSON.stringify).includes(JSON.stringify(second))) {
        second = points.find(
          (point) =>
            JSON.stringify(point) !== JSON.stringify(first) &&
            JSON.stringify(point) !== JSON.stringify(second) &&
            JSON.stringify(point) !== JSON.stringify(third)
        )
      } else if (!points.map(JSON.stringify).includes(JSON.stringify(third))) {
        third = points.find(
          (point) =>
            JSON.stringify(point) !== JSON.stringify(first) &&
            JSON.stringify(point) !== JSON.stringify(second) &&
            JSON.stringify(point) !== JSON.stringify(third)
        )
      }
      result.push([first, second, third])
    }
    return result
  }

  return {
    formatServerResponse,
  }
})()
