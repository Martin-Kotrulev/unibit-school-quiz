export interface IUserState {
  userRegistered: boolean;
  userAuthenticated: boolean;
  token: string;
  username: string;
}

export const initialUserState: IUserState = {
  userRegistered: false,
  userAuthenticated: false,
  token: null,
  username: null
};
