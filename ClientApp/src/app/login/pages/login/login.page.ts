import { Component, OnInit } from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { LoginUser } from '../../models/login-user.model';
import { LoginService } from '../../services/login.service';
import { SessionService } from 'src/app/shared/session.service';

@Component({
  selector: 'app-login',
  templateUrl: './login.page.html',
  styleUrls: ['./login.page.scss'],
})
export class LoginPage implements OnInit {
  private _loginUser: LoginUser = { email: '', password: '' };
  private _loginForm: any;

  constructor(
    private readonly loginService: LoginService,
    private readonly router: Router,
    private readonly sessionService: SessionService
  )
  {}

  ngOnInit(): void {
    if (this.loginService.loggedIn) {
      this.router.navigate(['dashboard']);
    }
    this._loginForm = new FormGroup({
      email: new FormControl(this._loginUser.email, [
        Validators.required,
        Validators.pattern(/^[a-zA-Z0-9.!#$%&'*+/=?^_`{|}~-]+@[a-zA-Z0-9-]+(?:\.[a-zA-Z0-9-]+)*$/),
      ]),
      password: new FormControl(this._loginUser.password, [
        Validators.required,
      ]),
    });
  }
  login(): void {
    this._loginUser.email = this._loginForm.get('email').value
    this._loginUser.password = this._loginForm.get('password').value
  }

  get email() {
    return this._loginForm.get('email');
  }
  get password() {
    return this._loginForm.get('password');
  }
  get loginForm() {
    return this._loginForm
  }

  public googleLogin(): void {
    this.loginService.googleLogin( async () => {
      const token = sessionStorage.getItem('token') as string
      await this.loginService.verifyUser(token);
    });
  }
}
