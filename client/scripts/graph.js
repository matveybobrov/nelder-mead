const {
  rootWidth,
  rootHeight,
  marginTop,
  marginRight,
  marginBottom,
  marginLeft,
  graphWidth,
  graphHeight,
} = SIZES_MODULE

const { func, x, y, n, m, z } = FUNCTION_MODULE
const { getServerResponse } = API_MODULE
const { formatServerResponse, setTooltipText, setResultText } = HELPERS_MODULE

class Graph {
  root = null
  graphArea = null
  xAxis = null
  yAxis = null

  startingPoint = null

  animationDuration = 10000
  strokeColor = '#030'
  contourColor = '#5ced73'

  constructor({ animationDuration, strokeColor, contourColor }) {
    // ??= operator allows us to use default class values if constuctor attributes were not provided
    this.animationDuration ??= animationDuration
    this.strokeColor ??= strokeColor
    this.contourColor ??= contourColor
  }

  init() {
    this.initRootElemet()
    this.initGraphArea()
    this.initAxes()
    this.initContour()
    this.bindEventHandlers()
  }

  bindEventHandlers() {
    this.graphArea
      .on('click', () => {
        this.startAlgorithm(this.xAxis, this.yAxis)
      })
      .on('mousemove', (event) => {
        this.showCurrentCoords(event, this.xAxis, this.yAxis)
      })
      .on('mouseleave', () => {
        this.hideCurrentCoords()
      })
  }
  unbindEventHandlers() {
    this.graphArea.on('mousemove', null).on('mouseleave', null).on('click', null)
  }

  async startAlgorithm(xAxis, yAxis) {
    this.disableUserActions()

    const initialSimplex = this.getInitialSimplex(xAxis, yAxis)
    console.log('initialSimplex', initialSimplex)
    let result
    try {
      result = await getServerResponse(initialSimplex)
      result = formatServerResponse(result)
      console.log('result', result)
    } catch (err) {
      console.log('error', err.message)
      this.showError()
      this.enableUserActions()
      return
    }

    this.showAnimation(result)
  }

  showAnimation(result) {
    // Ограничиваем количество шагов до 30
    let steps = result.Steps.filter((_, i) => i < 30)
    let stepDuration = this.animationDuration / steps.length
    let triangles = steps
    let currTriangle = this.graphArea
      .append('path')
      .attr('id', 'triangle')
      .attr('stroke', 'red')
      .attr('fill', 'none')
      .attr('d', this.trianglePath([this.startingPoint, this.startingPoint, this.startingPoint]))

    triangles.forEach((d, index) => {
      currTriangle = currTriangle
        .transition()
        .duration(stepDuration)
        .attr('d', this.trianglePath(d))

      if (index < triangles.length - 1) {
        return
      }

      currTriangle = currTriangle.on('end', function () {
        console.log('end')
      })
    })

    setTimeout(() => {
      this.showSolution(result.Solution)
      d3.select('#triangle').remove()
      this.enableUserActions()
    }, 10000)
  }

  showSolution(solution) {
    const x = Math.round(solution.X)
    const y = Math.round(solution.Y)
    setResultText(`x:${x}; y:${y}; f(x,y): ${func(x, y)}`)
  }
  showError() {
    setResultText(`Не удалось вычислить решение`)
  }

  getCursorCoords(event, xAxis, yAxis) {
    let x = xAxis.invert(d3.pointer(event)[0]).toFixed(2)
    let y = yAxis.invert(d3.pointer(event)[1]).toFixed(2)
    let value = func(x, y).toFixed(2)
    return {
      x,
      y,
      value,
    }
  }
  showCurrentCoords(event, xAxis, yAxis) {
    const { x, y, value } = this.getCursorCoords(event, xAxis, yAxis)
    setTooltipText(`x:${x}; y:${y}; f(x,y): ${value}`)
  }
  hideCurrentCoords() {
    setTooltipText(`Выберите точку на графике`)
  }

  disableUserActions() {
    document.getElementById('graph').style.cursor = 'default'
    this.unbindEventHandlers()
    setResultText(`Решение вычисляется...`)
  }
  enableUserActions() {
    document.getElementById('graph').style.cursor = 'pointer'
    this.bindEventHandlers()
  }

  getInitialSimplex(xAxis, yAxis) {
    const stepSize = 1

    let mouseX = xAxis.invert(d3.pointer(event)[0])
    let mouseY = yAxis.invert(d3.pointer(event)[1])

    this.startingPoint = { X: mouseX, Y: mouseY }
    const initialSimplex = []
    initialSimplex.push(this.startingPoint)

    if (mouseX + stepSize < d3.max(x)) {
      initialSimplex.push({
        X: mouseX + stepSize,
        Y: mouseY,
      })
    } else {
      initialSimplex.push({
        X: mouseX - stepSize,
        Y: mouseY,
      })
    }

    if (mouseY + stepSize < d3.max(y)) {
      initialSimplex.push({
        X: mouseX,
        Y: mouseY + stepSize,
      })
    } else {
      initialSimplex.push({
        X: mouseX,
        Y: mouseY - stepSize,
      })
    }
    return initialSimplex
  }

  trianglePath(triangles) {
    var path = 'M ' + this.xAxis(triangles[0].X) + ',' + this.yAxis(triangles[0].Y)
    path += ' L ' + this.xAxis(triangles[1].X) + ',' + this.yAxis(triangles[1].Y)
    path += ' L ' + this.xAxis(triangles[2].X) + ',' + this.yAxis(triangles[2].Y) + ' Z'

    return path
  }

  initRootElemet() {
    this.root = d3
      .select('#graph-container')
      .append('svg')
      .attr('width', rootWidth)
      .attr('height', rootHeight)
  }

  initGraphArea() {
    this.graphArea = this.root
      .append('g')
      .attr('id', 'graph')
      .attr('width', graphWidth)
      .attr('height', graphHeight)
      .attr('transform', `translate(${marginLeft},${marginTop})`)
      .attr('clip-path', 'url(#clip)')
  }

  initAxes() {
    this.xAxis = d3
      .scaleLinear()
      // Правильно центрирует ось относительно графика
      .domain([d3.min(x), d3.max(x)])
      .range([0, graphWidth])
    this.yAxis = d3
      .scaleLinear()
      .domain([d3.min(y), d3.max(y)])
      .range([graphHeight, 0])
    this.root
      .append('g')
      .attr('class', 'axis')
      .attr('transform', `translate(${marginLeft},${rootHeight - marginBottom})`)
      .call(d3.axisBottom(this.xAxis))
    this.root
      .append('g')
      .attr('class', 'axis')
      .attr('transform', `translate(${marginLeft},${marginTop})`)
      .call(d3.axisLeft(this.yAxis))
  }

  initContour() {
    let thresholds = d3.merge([d3.range(d3.min(z), 25, 5), d3.range(50, d3.max(z), 50)])
    let contours = d3.contours().size([n, m]).thresholds(thresholds)
    let color = d3
      .scaleLinear()
      .domain(d3.extent(thresholds))
      .range(['#000', this.contourColor])
      .interpolate(d3.interpolateHcl)

    this.graphArea
      .selectAll('path')
      .data(contours(z))
      .enter()
      .append('path')
      .attr('d', d3.geoPath(d3.geoIdentity().scale(graphWidth / m)))
      .attr('fill', (d) => {
        return color(d.value)
      })
      .attr('stroke', this.strokeColor)
  }
}
