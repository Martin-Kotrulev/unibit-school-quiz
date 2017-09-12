import * as React from 'react'
import { Link } from 'react-router-dom'
import Auth from '../../Auth'

interface NavbarState { currentTime: string }

class Navbar extends React.Component<any, NavbarState> {
  private clockInterval: any;

  constructor(props: NavbarState) {
    super(props)

    this.state = {
      currentTime: Date.now().toLocaleString()
    }
  }
  
  componentWillMount() {
    this.clockInterval = setInterval(() => {
      let currentTime = Date.now().toLocaleString();
      this.setState({ currentTime })
    }, 1000);
  }

  componentWillUnmount() {
    clearInterval(this.clockInterval);
  }

  render () {
    return (
      <div>
        <nav className='navbar navbar-default'>
          <div className='container-fluid'>
            <div className='navbar-header'>
              <Link className='navbar-brand' to='/'>UniQuizBit</Link>
            </div>
            {Auth.isAuthenticated() ? (
                <ul className='nav navbar-nav'>
                  <li><Link to='/pets/create'>Create Group</Link></li>
                  <li><Link to='/pets/create'>Create Quiz</Link></li>

                </ul>
              ) : (
                null
              )}
            {Auth.isAuthenticated() ? (
              <ul className='nav navbar-nav navbar-right'>
                <li><Link to='/users/logout'><span className='glyphicon glyphicon-glyphicon-hourglass' /> {  }</Link></li>
                <li><Link to='/users/profile'><span className='glyphicon glyphicon-user' /> {Auth.getUser().name}</Link></li>
                <li><Link to='/users/logout'><span className='glyphicon glyphicon-log-in' /> Logout</Link></li>
              </ul>
            ) : (
              <ul className='nav navbar-nav navbar-right'>
                <li><Link to='/users/register'><span className='glyphicon glyphicon-user' /> Register</Link></li>
                <li><Link to='/users/login'><span className='glyphicon glyphicon-log-in' /> Login</Link></li>
              </ul>
            )}
          </div>
        </nav>
      </div>
    )
  }
}

export default Navbar
