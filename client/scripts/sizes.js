const SIZES_MODULE = (function () {
  const rootWidth = 600
  const rootHeight = 600
  const marginTop = 10
  const marginBottom = 20
  const marginRight = 10
  const marginLeft = 20
  const graphWidth = rootWidth - marginLeft - marginRight
  const graphHeight = rootHeight - marginBottom - marginTop

  return {
    rootWidth,
    rootHeight,
    marginTop,
    marginBottom,
    marginLeft,
    marginRight,
    graphWidth,
    graphHeight,
  }
})()
