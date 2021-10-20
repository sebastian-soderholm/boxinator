import { Injectable } from '@angular/core';
import { ActivatedRouteSnapshot, CanActivate, Router, RouterStateSnapshot, UrlTree } from '@angular/router';
import { Observable } from 'rxjs';
import { LoginService } from './login.service';

@Injectable({
  providedIn: 'root'
})
export class AuthGuard implements CanActivate {

  constructor(private readonly router: Router, private readonly loginService: LoginService){}

  canActivate(
    route: ActivatedRouteSnapshot,
    state: RouterStateSnapshot): Observable<boolean | UrlTree> | Promise<boolean | UrlTree> | boolean | UrlTree {

    if(this.loginService.user !== undefined) {
      if(this.router.url === '/login'){
        this.router.navigate(['dashboard'])
      }else if(this.router.url === '/guest'){
        this.router.navigate(['new-shipment'])
      }
      return true;
    }
    this.router.navigate(['login'])
    return false;
  }

}
