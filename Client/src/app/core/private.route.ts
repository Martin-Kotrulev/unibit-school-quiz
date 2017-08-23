import { AuthService } from './../users/auth.service';
import { Injectable } from '@angular/core';
import { CanActivate, Router } from '@angular/router';

@Injectable()
export class PrivateRoute implements CanActivate {
  constructor(
    private authService: AuthService,
    private router: Router
  ) {}
  canActivate(): boolean {
    if (!this.authService.isUserAuthenticated()) {
      this.router.navigateByUrl('/users/login');
    }

    return this.authService.isUserAuthenticated();
  }
}
