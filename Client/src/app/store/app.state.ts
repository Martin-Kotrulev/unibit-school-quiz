import { ICoreState } from './core/core.state';
import { IUserState } from './users/users.state';

export interface IAppState {
  users: IUserState;
  core: ICoreState;
}
