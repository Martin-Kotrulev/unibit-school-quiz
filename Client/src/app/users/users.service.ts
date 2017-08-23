import { LoginUserModel } from './login-user.model';
import { Injectable } from '@angular/core';

import { HttpService } from './../core/http.service';
import { RegisterUserModel } from './register-user.model';

const registerUrl = '/auth/signup';
const loginUrl = '/auth/login';

@Injectable()
export class UsersService {
  constructor(private httpService: HttpService) {}

  register(user: RegisterUserModel) {
    return this.httpService
      .post(registerUrl, user);
  }

  login(user: LoginUserModel) {
    return this.httpService
      .post(loginUrl, user);
  }
}
