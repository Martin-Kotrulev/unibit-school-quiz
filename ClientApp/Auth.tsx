class Auth {
  static saveUser (user: object) {
    window.localStorage.setItem('user', JSON.stringify(user))
  }

  static getUser () {
    let user: string = window.localStorage.getItem('user') || ''
    return JSON.parse(user)
  }

  static authenticateUser (token: string) {
    window.localStorage.setItem('token', token)
  }

  static isAuthenticated () {
    return window.localStorage.getItem('token') !== null
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
