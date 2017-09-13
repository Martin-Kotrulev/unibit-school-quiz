import toastr from 'toastr'
import Auth from '../../Auth'

export default class ResponseHelper {
  static handleResponse (data, redirectPath) {
    console.log(data)
    if (data.success) {
      toastr.success(data.message)

      if (data.result.token) {
        Auth.authenticateUser(data.result.token)
      }

      if (data.result.user) {
        Auth.saveUser(data.result.user)
      }

      if (data.result.expires) {
        Auth.setTokenExpiration(data.result.expires)
      }

      if (redirectPath) {
        this.props.history.push(redirectPath)
      }
    } else {
      if (data.errors) {
        let firstError = Object.keys(data.errors)
          .map(k => data.errors[k])[0]
        this.setState({ error: firstError })
      } else {
        this.setState({ error: data.message })
      }
    }
  }
}
