import { Component, ViewEncapsulation } from '@angular/core';
import { Router } from '@angular/router';
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
    private readonly router: Router,
    ) {
    if(this.window > 768) {
      this.disableClose=true;
    } else {
      this.disableClose=false;
      this.opened = false;
    }
    // If routing changes, close the side nav in mobile view
    this.router.events.subscribe(event => {
      if(!this.disableClose) {
        this.opened = false;
      }
    });
    }

  get isLoggedIn(): boolean {
    return this.loginService.loggedIn
  }

  get isRegistered(): boolean {
    if(this.loginService.user?.firstName) {
      return true;
    }
    return false;
  }

  public toggleSidenav(): void {
    this.opened = !this.opened;
  }

  // Detects window size change and changes layout id need to
  onResize(event: any) {
    if(event.target.innerWidth > 768) {
      this.disableClose=true;
      this.opened = true;
    } else {
      this.disableClose=false;
      this.opened = false;
    }
  }
}
