import * as React from 'react'
import RegisterForm from './RegisterForm'
import userActions from '../../actions/UserActions'
import userStore from '../../stores/UserStore'
import ResponseHelper from '../common/ResponseHelper'
import FormHelper from '../common/FormHelper'

class Register extends React.Component<any, any> {
  constructor (props: any) {
    super(props)

    this.state = {
      user: {
        name: '',
        email: '',
        password: '',
        confirmPassword: ''
      },
      error: ''
    }

    this.handleUserRegistration = this.handleUserRegistration.bind(this)
    this.validateForm = this.validateForm.bind(this)

    userStore.on(
      userStore.eventTypes.USER_REGISTERED,
      this.handleUserRegistration
    )
  }

  componentWillUnmount () {
    userStore.removeListener(
      userStore.eventTypes.USER_REGISTERED,
      this.handleUserRegistration
    )
  }

  handleFormChange (event: any) {
    FormHelper.handleFormChange.call(this, event, 'user')
  }

  validateForm () {
    let user = this.state.user
    let error = ''

    if (user.password !== user.confirmPassword) {
      error = 'Password and confirm password do not match.'
    }

    if (user.password.length < 4) {
      error = 'Pasword should be at least 4 characters long.'
    }

    if (!user.email) {
      error = 'User email is required'
    }

    if (!user.name) {
      error = 'User name is required'
    }

    if (error) {
      this.setState({error})
      return false
    }

    return true
  }

  handleUserRegistration (data: any) {
    ResponseHelper.handleResponse.call(this, data, '/users/login')
  }

  handleFormSubmit (event: any) {
    event.preventDefault()
    if (this.validateForm()) {
      userActions.register(this.state.user)
    }
  }

  render () {
    return (
      <div>
        <h1>Register User</h1>
        <RegisterForm
          user={this.state.user}
          onChange={this.handleFormChange.bind(this)}
          error={this.state.error}
          onSubmit={this.handleFormSubmit.bind(this)} />
      </div>
    )
  }
}

export default Register
