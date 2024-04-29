const FUNCTION_MODULE = (function () {
  // Создание парсера строки в функцию с помощью библиотеки expr-eval
  const Parser = exprEval.Parser
  const parser = new Parser({
    operators: {
      // These default to true, but are included to be explicit
      add: true,
      concatenate: true,
      conditional: true,
      divide: true,
      factorial: true,
      multiply: true,
      power: true,
      remainder: true,
      subtract: true,

      // Disable and, or, not, <, ==, !=, etc.
      logical: false,
      comparison: false,

      // Disable 'in' and = operators
      in: false,
      assignment: false,
    },
  })

  let func = parser.parse('x^2+x*y+y^2-6*x-9*y').toJSFunction('x,y')

  let precision = 0.1
  let x = d3.range(-9, 11, precision)
  let y = d3.range(-6, 14, precision)

  let n = x.length
  let m = y.length

  // Значение функции в каждой точке графика для отрисовки контура
  let z = []
  for (let i = m - 1; i >= 0; i--) {
    for (let j = 0; j < n; j++) {
      let k = j + (m - 1 - i) * n
      z[k] = func(x[j], y[i])
    }
  }

  return {
    func,
    x,
    y,
    n,
    m,
    z,
  }
})()
