import { Component } from 'react'
import Auth from '../../Auth'
import toastr from 'toastr'

export default class Logout extends Component {
  componentWillMount () {
    Auth.deauthenticateUser()
    toastr.success('You have successfully logged out.')
    this.props.history.push('/users/login')
  }

  render () {
    return null
  }
}
