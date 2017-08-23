import { Router } from '@angular/router';
import { Component } from '@angular/core';
import { NgRedux } from 'ng2-redux';

import { UserActions, USER_LOGGEDIN } from './../store/users/users.actions';
import { IAppState } from './../store/app.state';
import { LoginUserModel } from './login-user.model';
import { AuthService } from './auth.service';

@Component({
  selector: 'login',
  templateUrl: './login.component.html'
})
export class LoginComponent {
  user: LoginUserModel = new LoginUserModel();

  constructor (
    private userActions: UserActions,
    private ngRedux: NgRedux<IAppState>,
    private router: Router,
    private authService: AuthService
  ) {}

  login() {
    this.userActions.login(this.user);
    this.ngRedux
      .select(state => state.users)
      .subscribe(user => {
        if (user.userAuthenticated) {
          this.authService.authenticateUser(user.token);
          this.authService.setUser(user);
        }
      });
  }
}

