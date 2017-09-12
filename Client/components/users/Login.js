import React, { Component } from 'react'
import LoginForm from './LoginForm'
import FormHelper from '../common/FormHelper'
import ResponseHelper from '../common/ResponseHelper'
import userActions from '../../actions/UserActions'
import userStore from '../../stores/UserStore'

class Login extends Component {
  constructor (props) {
    super(props)

    this.state = {
      user: {
        email: '',
        password: ''
      },
      error: ''
    }

    this.handleUserLogin = this.handleUserLogin.bind(this)
    this.validateForm = this.validateForm.bind(this)

    userStore.on(
      userStore.eventTypes.USER_LOGGED_IN,
      this.handleUserLogin
    )
  }

  componentWillUnmount () {
    userStore.removeListener(
      userStore.eventTypes.USER_LOGGED_IN,
      this.handleUserLogin
    )
  }

  validateForm () {
    let user = this.state.user
    let error = ''

    if (!user.password) {
      error = 'No password provided.'
    }

    if (!user.email) {
      error = 'No email provided.'
    }

    if (error) {
      this.setState({error})
      return false
    }

    userActions.login(this.state.user)
  }

  handleUserLogin (data) {
    console.log(data)
    ResponseHelper.handleResponse.call(this, data, '/')
  }

  handleFormChange (event) {
    FormHelper.handleFormChange.call(this, event, 'user')
  }

  handleFormSubmit (event) {
    event.preventDefault()
    this.validateForm()
  }

  render () {
    return (
      <div>
        <h1>Login in to your account</h1>
        <LoginForm
          user={this.state.user}
          onChange={this.handleFormChange.bind(this)}
          error={this.state.error}
          onSubmit={this.handleFormSubmit.bind(this)} />
      </div>
    )
  }
}

export default Login
