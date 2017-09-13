import './css/site.css'
import React from 'react'
import ReactDOM from 'react-dom'
import { BrowserRouter } from 'react-router-dom'
import App from './App'

function renderApp () {
  // This code starts up the React app when it runs in a browser. It sets up the routing
  // configuration and injects the app into a DOM element.
  const baseUrl = document.getElementsByTagName('base')[0].getAttribute('href')
  ReactDOM.render(
    <BrowserRouter>
      <App />
    </BrowserRouter>,
    document.getElementById('react-app')
  )
}

renderApp()

// Allow Hot Module Replacement
if (module.hot) {
  module.hot.accept()
}
