import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { User } from 'src/app/account/models/user.model';
import { LoginService } from 'src/app/login/services/login.service';
import { SessionService } from 'src/app/shared/session.service';

@Component({
  selector: 'app-menu',
  templateUrl: './menu.page.html',
  styleUrls: ['./menu.page.scss']
})
export class MenuPage implements OnInit {
  user: User = this.sessionService.user!;
  canEdit: boolean = false;

  constructor(
    private readonly loginService: LoginService,
    private readonly sessionService: SessionService,
    private readonly router: Router
    ) { }

  ngOnInit(): void {
    this.canEdit = this.loginService.isAdmin;
  }

  logout() {
    this.loginService.logout();
    this.router.navigate([''])
  }
}
