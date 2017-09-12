import * as React from 'react'
import './css/site.css'
import Navbar from './components/common/Navbar'
import Routes from './components/common/Routes'

class App extends React.Component {
  render () {
    return (
      <div className='App'>
        <Navbar />
        <Routes />
      </div>
    )
  }
}

export default App
