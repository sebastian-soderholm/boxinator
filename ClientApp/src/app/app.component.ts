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
  window = window.innerWidth;
  disableClose = false;
  opened = true;

  constructor(
    private readonly loginService: LoginService,
    ) {
    if(this.window > 768) {
      this.disableClose=true;
      this.opened = false;
    } else {
      this.disableClose=false;
    }
    }

  get isLoggedIn(): boolean {
    return this.loginService.loggedIn
  }

  public toggleSidenav(): void {
    this.opened = !this.opened;
  }
}
