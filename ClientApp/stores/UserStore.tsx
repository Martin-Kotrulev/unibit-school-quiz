import Dispatcher from '../Dispatcher'
import UserActions from '../actions/UserActions'
import UserData from '../data/UserData'

const { EventEmitter } = require('events')

class UserStore extends EventEmitter {
  registerUser (user: any) {
    UserData.registerUser(user)
      .then(data => this.emit(this.eventTypes.USER_REGISTERED, data))
  }

  loginUser (user: any) {
    UserData.loginUser(user)
      .then(data => this.emit(this.eventTypes.USER_LOGGED_IN, data))
  }

  handleAction (action: any) {
    switch (action.type) {
      case UserActions.types.REGISTER_USER:
        this.registerUser(action.payload)
        break
      case UserActions.types.LOGIN_USER:
        this.loginUser(action.payload)
        break
      default:
        break
    }
  }
}

let userStore = new UserStore()

userStore.eventTypes = {
  USER_REGISTERED: 'USER_REGISTERED',
  USER_LOGGED_IN: 'USER_LOGGED_IN'
}

Dispatcher.register(userStore.handleAction.bind(userStore))

export default userStore
