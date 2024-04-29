const API_MODULE = (function () {
  async function getServerResponse(initialSimplex) {
    const response = await fetch('/result', {
      method: 'POST',
      headers: {
        'Content-Type': 'application/json;charset=utf-8',
      },
      body: JSON.stringify(initialSimplex),
    })
    const data = await response.json()
    return data
  }

  return {
    getServerResponse,
  }
})()
