import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { SessionService } from 'src/app/shared/session.service';
import { LoginService } from '../../services/login.service';

@Component({
  selector: 'app-login',
  templateUrl: './login.page.html',
  styleUrls: ['./login.page.scss'],
})
export class LoginPage implements OnInit {

  constructor(
    private readonly loginService: LoginService,
    private readonly sessionService: SessionService,
    private readonly router: Router
  )
  {}

  ngOnInit(): void {
    if (this.loginService.loggedIn) {
      //If logged in user is registered or admin, go to dashboard
      if(this.loginService.user?.accountType !== 'GUEST') this.router.navigate(['dashboard']);
      //User is a guest, redirect to register
      else this.router.navigate(['register']);
    }
    /* Form validation
      this._loginForm = new FormGroup({
      email: new FormControl(this._loginUser.email, [
        Validators.required,
        Validators.pattern(/^[a-zA-Z0-9.!#$%&'*+/=?^_`{|}~-]+@[a-zA-Z0-9-]+(?:\.[a-zA-Z0-9-]+)*$/),
      ]),
      password: new FormControl(this._loginUser.password, [
        Validators.required,
      ]),
    });
    */
  }

  public googleLogin(): void {
    this.loginService.googleLogin( () => {
      const token = this.sessionService.token!;
      this.loginService.verifyUser(token)
    });
  }
}
