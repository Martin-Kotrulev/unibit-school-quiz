import { initialCoreState } from './core.state';
import { ROUTES_CHANGED } from './core.actions';

function changeMessage(state, action) {
  const result = action.result;
  if (result) {
    let message = result.message;
    const errors = result.errors;

    if (errors) {
      const keys = Object.keys(errors);
      if (keys.length > 0) {
        message = errors[keys[0]];
      }
    }

    if (result.message) {
      return Object.assign({}, state, { message });
    }
  }

  return state;
}

export function coreReducer(state = initialCoreState, action) {

  switch (action.type) {
    case ROUTES_CHANGED:
      return Object.assign({}, state, {
        message: null
      });
    default:
      return changeMessage(state, action);
  }
}
