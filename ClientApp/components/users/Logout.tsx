import * as React from 'react'
import Auth from '../../Auth'

class Logout extends React.Component<any, any> {
  componentWillMount() {
    Auth.deauthenticateUser()
    this.props.history.push('/users/login')
  }

  render () {
    return null
  }
}

export default Logout