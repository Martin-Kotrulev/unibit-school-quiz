import { Injectable } from '@angular/core';
import { NgRedux } from 'ng2-redux';

import { UsersService } from './../../users/users.service';
import { IAppState } from './../app.state';

export const USER_REGISTERED = 'USER_REGISTERED';
export const USER_LOGGEDIN = 'USER_LOGGEDIN';
export const USER_LOGOUT = 'USER_LOGOUT';

@Injectable()
export class UserActions {
  constructor(
    private usersService: UsersService,
    private ngRedux: NgRedux<IAppState>
  ) {}

  register(user) {
    this.usersService
      .register(user)
      .subscribe(result => {
        this.ngRedux.dispatch({
          type: USER_REGISTERED,
          result
        });
      });
  }

  login(user) {
    this.usersService
      .login(user)
      .subscribe(result => {
        this.ngRedux.dispatch({
          type: USER_LOGGEDIN,
          result
        });
      });
  }

  logout() {
    this.ngRedux
      .dispatch({
        type: USER_LOGOUT
      });
  }
}
