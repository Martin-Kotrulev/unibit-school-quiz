import { USER_REGISTERED, USER_LOGGEDIN, USER_LOGOUT } from './users.actions';
import { initialUserState } from './users.state';

function userRegistration(state, action) {
  const result = action.result;
  return Object.assign({}, state, {
    userRegistered: result.success
  });
}

function userLogin(state, action) {
  const result = action.result;
  if (result.success) {
    return Object.assign({}, state, {
      userAuthenticated: result.success,
      token: result.token,
      username: result.user.name
    });
  }
  return state;
}

function userLogout(state, action) {
  return Object.assign({}, state, initialUserState);
}

export function usersReducer(state = initialUserState, action) {
  switch (action.type) {
    case USER_REGISTERED:
      return userRegistration(state, action);
    case USER_LOGGEDIN:
      return userLogin(state, action);
    case USER_LOGOUT:
      return userLogout(state, action);
    default:
      return state;
  }
}
