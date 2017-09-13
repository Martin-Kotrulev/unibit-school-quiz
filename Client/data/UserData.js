import Http from '../Http'

const baseUrl = '/users'

class UserData {
  static registerUser (user) {
    return Http.post(`${baseUrl}/signup`, user)
  }

  static loginUser (user) {
    return Http.post(`${baseUrl}/login`, user)
  }
}

export default UserData
