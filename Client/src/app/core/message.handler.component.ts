import { Component, OnInit } from '@angular/core';
import { NgRedux } from 'ng2-redux';
import { IAppState } from './../store/app.state';
import { ICoreState } from './../store/core/core.state';

@Component({
  selector: 'message-handler',
  template: `
    <div>{{ message }}</div>
  `
})
export class MessageHandlerComponent implements OnInit {
  message: String;

  constructor(private ngRedux: NgRedux<IAppState>) {}

  ngOnInit(): void {
    this.ngRedux
      .select(state => state.core.message)
      .subscribe(message => this.message = message);
  }
}
