import Dispatcher from '../Dispatcher'

const userActions = {
  types: {
    REGISTER_USER: 'REGISTER_USER',
    LOGIN_USER: 'LOGIN_USER'
  },
  register (user) {
    console.log(user)
    Dispatcher.dispatch({
      type: this.types.REGISTER_USER,
      payload: user
    })
  },
  login (user) {
    Dispatcher.dispatch({
      type: this.types.LOGIN_USER,
      payload: user
    })
  }
}

export default userActions
