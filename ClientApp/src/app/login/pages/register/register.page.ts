import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { LoginService } from '../../services/login.service';

@Component({
  selector: 'app-register',
  templateUrl: './register.page.html',
  styleUrls: ['./register.page.scss']
})
export class RegisterPage implements OnInit {

  constructor(
    private readonly loginService: LoginService,
    private readonly router: Router,
  ) { }

  ngOnInit(): void {
    if(this.loginService.loggedIn) {
      this.router.navigate(['dashboard'])
    }
  }

}
