"use strict"

const calculateButton = document.getElementById("calculate-btn")
const resultDiv = document.getElementById("result-div")

calculateButton.addEventListener('click', clickHandler)

async function clickHandler() {
  let X, Y;
  try {
    const result = await getResultFromServer()
    X = result.Solution.X
    Y = result.Solution.Y
    showSolution(`x:${X}; y:${Y}`)
  } catch(error) {
    showSolution(error.message)
  }
}

async function getResultFromServer() {
  setLoadingMessage()
  const response = await fetch("http://localhost:7022/result")
  const data = await response.json()
  console.log(data)
  return data
}

function showSolution(message) {
  resultDiv.textContent = message 
}

function setLoadingMessage() {
  resultDiv.textContent = "loading..."
}