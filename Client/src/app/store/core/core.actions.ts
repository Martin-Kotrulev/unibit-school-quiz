import { Injectable } from '@angular/core';
import { NgRedux } from 'ng2-redux';

import { AuthService } from '../../users/auth.service';
import { USER_LOGGEDIN } from './../users/users.actions';
import { IAppState } from './../app.state';

export const ROUTES_CHANGED = 'ROUTES_CHANGED';
export const AUTO_LOGIN = 'AUTO_LOGIN';

@Injectable()
export class CoreActions {
  constructor(
    private ngRedux: NgRedux<IAppState>,
    private authService: AuthService
  ) {}

  routesChanged() {
    this.ngRedux
      .dispatch({
        type: ROUTES_CHANGED
      });
  }

  autoLogin() {
    this.ngRedux
      .dispatch({
        type: USER_LOGGEDIN,
        result: {
          success: this.authService.isUserAuthenticated(),
          token: this.authService.getToken(),
          user: {
            name: this.authService.getUser()
          }
        }
      });
  }
}
