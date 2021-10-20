import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { LoginService } from '../../services/login.service';

@Component({
  selector: 'app-login',
  templateUrl: './login.page.html',
  styleUrls: ['./login.page.scss'],
})
export class LoginPage implements OnInit {
  
  constructor(
    private readonly loginService: LoginService,
    private readonly router: Router
  )
  {}

  ngOnInit(): void {
    if (this.loginService.loggedIn) {
      this.router.navigate(['dashboard']);
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
      const token = sessionStorage.getItem('token') as string
      this.loginService.verifyUser(token)
    });
  }
}
