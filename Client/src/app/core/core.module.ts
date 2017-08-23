import { CommonModule } from '@angular/common';
import { RouterModule } from '@angular/router';
import { NgModule } from '@angular/core';

import { NavbarComponent } from './navbar.component';
import { HttpService } from './http.service';
import { MessageHandlerComponent } from './message.handler.component';
import { PrivateRoute } from './private.route';
import { CoreActions } from './../store/core/core.actions';

@NgModule({
  imports: [
    RouterModule,
    CommonModule
  ],
  providers: [
    HttpService,
    CoreActions,
    PrivateRoute
  ],
  declarations: [
    NavbarComponent,
    MessageHandlerComponent
  ],
  exports: [
    NavbarComponent,
    MessageHandlerComponent
  ]
})
export class CoreModule {}
