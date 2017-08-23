import { Router } from '@angular/router';
import { NgRedux } from 'ng2-redux';
import { Component, OnInit, Input } from '@angular/core';

import { UserActions } from './../store/users/users.actions';
import { AuthService } from './../users/auth.service';
import { IAppState } from './../store/app.state';

@Component({
  selector: 'navbar',
  templateUrl: './navbar.component.html',
})
export class NavbarComponent implements OnInit {
  @Input() brand: string;
  authenticated: boolean;
  username: string;

  constructor(
    private ngRedux: NgRedux<IAppState>,
    private userActions: UserActions,
    private authService: AuthService,
    private router: Router
  ) {}

  ngOnInit(): void {
    this.ngRedux
      .select(state => state.users)
      .subscribe(user => {
        this.authenticated = user.userAuthenticated;
        this.username = user.username;
      });
  }

  logout() {
    this.userActions.logout();
    this.authService.deauthenticateUser();
    this.router.navigateByUrl('');
  }
}
