require('react-hot-loader/patch')
import './css/site.css';
import 'bootstrap';
import * as React from 'react';
import * as ReactDOM from 'react-dom';
import { AppContainer } from 'react-hot-loader';
import { BrowserRouter } from 'react-router-dom';
import App from './App'
import Routes from './components/common/Routes'

let routes = Routes

function renderApp() {
  // This code starts up the React app when it runs in a browser. It sets up the routing
  // configuration and injects the app into a DOM element.
  const baseUrl = document.getElementsByTagName('base')[0].getAttribute('href')!;
  ReactDOM.render(
    <AppContainer>
      <BrowserRouter basename={ baseUrl }>
        <App />
      </BrowserRouter>
    </AppContainer>,
    document.getElementById('react-app')
  );
}

renderApp()

// Allow Hot Module Replacement
if (module.hot) {
  module.hot.accept('./components/common/Routes', () => {
      routes = require<typeof Routes>('./components/common/Routes')
      renderApp()
  })
}


