import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Router } from '@angular/router';
import { User } from '../models/user.model';

const apiURL = "";

@Injectable({
  providedIn: 'root'
})
export class LoginService {

  private _user: User | undefined;
  private _jwt: string = "";
  private _loggedIn: boolean = false;

  constructor(
    private readonly http: HttpClient,
    private readonly router: Router
  ) {
    const localStorageUser = localStorage.getItem('user');
    if (localStorageUser) {
      this._user = JSON.parse(localStorageUser) as User;
      console.log(localStorageUser + " logged in")
      this._loggedIn = true
      // this.userLoggedIn(this._jwt);
    }
  }

  private async userLoggedIn(token: string) {
    await this.http
      .get<User[]>(`${apiURL}/loggedin?token=${token}`)
      .subscribe((user) => {
        if (user) {
          this._loggedIn = true;
        } else {
          this.logout();
        }
      });
  }

  get user(): User | undefined {
    return this._user;
  }
  setUser(user: User): void {
    this._user = user;
    localStorage.setItem('user', JSON.stringify(user));
    this._loggedIn = true;
  }

  logout() {
    this._user = undefined;
    localStorage.removeItem('user');
    this._loggedIn = false;
    this.router.navigate(['']);
  }
  get loggedIn(): boolean {
    return this._loggedIn;
  }
}
