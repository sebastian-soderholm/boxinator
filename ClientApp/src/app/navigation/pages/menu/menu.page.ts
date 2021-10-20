import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { LoginService } from 'src/app/login/services/login.service';

@Component({
  selector: 'app-menu',
  templateUrl: './menu.page.html',
  styleUrls: ['./menu.page.scss']
})
export class MenuPage implements OnInit {

  constructor(
    private readonly loginService: LoginService,
    private readonly router: Router
    ) { }

  ngOnInit(): void {
  }

  logout() {
    this.loginService.logout();
    console.log("Logging out..." + " sessionStorage: " + this.loginService.user)
    this.router.navigate([''])
  }

  get isLoggedIn(): boolean {
    return this.loginService.loggedIn
  }

}
