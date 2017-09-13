class Auth {
  static saveUser (user) {
    window.localStorage.setItem('user', user)
  }

  static getUser () {
    return window.localStorage.getItem('user')
  }

  static setTokenExpiration (expires) {
    window.localStorage.setItem('expires', expires)
  }

  static authenticateUser (token) {
    window.localStorage.setItem('token', token)
  }

  static isAuthenticated () {
    let isValid = window.localStorage.getItem('token') !== null

    if (!isValid) return isValid

    isValid = Date.now() <= new Date(
      window.localStorage.getItem('expires'))

    if (!isValid) this.deauthenticateUser()

    return isValid
  }

  static deauthenticateUser () {
    window.localStorage.removeItem('token')
    window.localStorage.removeItem('user')
  }

  static getToken () {
    return window.localStorage.getItem('token')
  }
}

export default Auth
