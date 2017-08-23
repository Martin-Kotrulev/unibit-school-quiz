import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';

import { RegisterComponent } from './register.component';
import { UsersService } from './users.service';
import { AuthService } from './auth.service';
import { LoginComponent } from './login.component';
import { UserActions } from './../store/users/users.actions';

@NgModule({
  imports: [
    FormsModule,
    CommonModule
  ],
  declarations: [
    RegisterComponent,
    LoginComponent
  ],
  providers: [
    UsersService,
    UserActions,
    AuthService
  ],
  exports: []
})
export class UsersModule {}
