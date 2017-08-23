import { IUserState } from './users/users.state';
import { IAppState } from './app.state';

import { combineReducers } from 'redux';
import { usersReducer } from './users/users.reducer';
import { coreReducer } from './core/core.reducer';

export const reducer = combineReducers<IAppState>({
  users: usersReducer,
  core: coreReducer
});
