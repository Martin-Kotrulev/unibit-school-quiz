import { Injectable } from '@angular/core';

@Injectable()
export class AuthService {
  authenticateUser(token) {
    window.localStorage.setItem('token', token);
  }

  isUserAuthenticated() {
    return window.localStorage.getItem('token') !== null;
  }

  deauthenticateUser() {
    window.localStorage.removeItem('token');
    window.localStorage.removeItem('user');
  }

  getToken() {
    return window.localStorage.getItem('token');
  }

  setUser(user) {
    window.localStorage.setItem('user', user.username);
  }

  getUser() {
    return window.localStorage.getItem('user');
  }
}
