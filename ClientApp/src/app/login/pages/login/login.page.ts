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
      ]),
    });
  }
  login(): void {
    this._loginUser.email = this._loginForm.get('email').value
    this._loginUser.password = this._loginForm.get('password').value
    // console.log(this._loginUser);
/*     this.loginService.loginUserTEST(this._loginUser, function(){
      console.log("User logged in!")
    }) */
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
      const token = localStorage.getItem('token') as string
      let user = localStorage.getItem('user')
      let newUser = JSON.parse(user!) as LoginUser
      //await this.loginService.loginUserTEST(token, newUser);
      //console.log(token)
      await this.loginService.loginUserTEST(token);
    });
  }
}
