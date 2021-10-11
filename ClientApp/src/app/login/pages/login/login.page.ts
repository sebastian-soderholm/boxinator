import { Component, OnInit } from '@angular/core';
import { NgForm } from '@angular/forms';
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
    private readonly router: Router,
    // private readonly sessionService: SessionService
  ) {}

  ngOnInit(): void {
    if(this.loginService.loggedIn) {
      this.router.navigate(['dashboard'])
    }
  }

  public onLoginClick(loginUser: NgForm) {
    // this.loginService.handleLogin(loginUser, async () => {
    //   await this.router.navigate(['catalogue']);
    // });
    console.log("Logging in: ")
  }
}
