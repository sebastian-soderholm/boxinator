import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Router } from '@angular/router';
import { User } from '../models/user.model';


const apiURL = ''


@Injectable({
  providedIn: 'root',
})
export class SessionService {
  private _user: User | undefined;
  private _jwt: string = "";
  public loggedIn: boolean = false;

  constructor(
    private readonly http: HttpClient,
    private readonly router: Router
  ) {
    const storedUser = localStorage.getItem('user');
    if (storedUser) {
      this._user = JSON.parse(storedUser) as User;
      this.userLoggedIn(this._jwt);
    }
  }

  private async userLoggedIn(token: string) {
    await this.http
      .get<User[]>(`${apiURL}/loggedin?token=${token}`)
      .subscribe((user) => {
        if (user) {
          this.loggedIn = true;
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
    this.loggedIn = true;
  }

  logout() {
    this._user = undefined;
    localStorage.removeItem('user');
    this.loggedIn = false;
    this.router.navigate(['']);
  }
}
