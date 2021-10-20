import { Component, ViewEncapsulation } from '@angular/core';
import { LoginService } from './login/services/login.service';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.scss'],
  // encapsulation: ViewEncapsulation.None
})
export class AppComponent {
  title = 'Boxinator';

  constructor(
    private readonly loginService: LoginService,
    ) { }

  get isLoggedIn(): boolean {
    return this.loginService.loggedIn
  }
}
