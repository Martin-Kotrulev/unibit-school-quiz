import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { HttpModule } from '@angular/http';
import { Router, NavigationStart } from '@angular/router';

import { NgRedux, NgReduxModule} from 'ng2-redux';

import { IAppState, store } from './store';
import { AuthService } from './users/auth.service';
import { CoreActions } from './store/core/core.actions';

import { CoreModule } from './core/core.module';
import { UsersModule } from './users/users.module';
import { RoutesModule } from './routes.module';

import { AppComponent } from './app.component';

@NgModule({
  declarations: [
    AppComponent
  ],
  imports: [
    RoutesModule,
    BrowserModule,
    UsersModule,
    CoreModule,
    HttpModule,
    NgReduxModule
  ],
  providers: [],
  bootstrap: [AppComponent]
})
export class AppModule {
  constructor(
    private ngRedux: NgRedux<IAppState>,
    private router: Router,
    private coreActions: CoreActions,
    private authService: AuthService
  ) {
    this.ngRedux.provideStore(store);
    this.coreActions.autoLogin();
    this.router.events
      .subscribe(e => {
        if (e instanceof NavigationStart) {
          this.coreActions.routesChanged();
        }
      });
  }
}
