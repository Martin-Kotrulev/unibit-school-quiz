import { Component } from '@angular/core';
import { NgRedux } from 'ng2-redux';
import { Router } from '@angular/router';

import { IAppState } from './../store/app.state';
import { UserActions } from './../store/users/users.actions';
import { RegisterUserModel } from './register-user.model';
import { UsersService } from './users.service';

@Component({
  selector: 'register',
  templateUrl: './register.component.html'
})
export class RegisterComponent {
  user: RegisterUserModel = new RegisterUserModel();

  constructor(
    private userActions: UserActions,
    private ngRedux: NgRedux<IAppState>,
    private router: Router
  ) { }

  register() {
    this.userActions.register(this.user);
    this.ngRedux
      .select(state => state.users.userRegistered)
      .subscribe(userRegistered => {
        if (userRegistered) {
          this.router.navigateByUrl('/users/login');
        }
      });
  }
}
