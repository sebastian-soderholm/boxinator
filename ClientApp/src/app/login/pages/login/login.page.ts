import { Component, OnInit } from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { LoginUser } from '../../models/login-user.model';
import { LoginService } from '../../services/login.service';

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
    private readonly router: Router
  ) // private readonly sessionService: SessionService
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
        Validators.minLength(5),
      ]),
    });
  }
  login(): void {
    // this._loginUser.email = this.emailControl.value
    // this._loginUser.password = this.passwordControl.value
    console.log(this._loginUser);
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

  googleLogin() {
    this.loginService.googleLogin()
  }
}
