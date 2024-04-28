const FUNCTION_MODULE = (function () {
  function func(x, y) {
    return Math.pow(x, 2) + x * y + Math.pow(y, 2) - 6 * x - 9 * y
  }

  let precision = 0.1
  let x = d3.range(-10, 10, precision)
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
