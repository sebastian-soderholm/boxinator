import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Router } from '@angular/router';
import { environment } from 'src/environments/environment';
import { LoginUser } from '../models/login-user.model';
import { User } from '../../account/models/user.model';



@Injectable({
  providedIn: 'root'
})
export class LoginService {

  private _user: User | undefined;
  private _jwt: string = "";
  private _loggedIn: boolean = false;
  private _apiUrl = environment.baseURL;

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
      .get<User[]>(`${this._apiUrl}/loggedin?token=${token}`)
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

  public loginUserTEST(loginInfo: LoginUser, onSuccess: () => void): void {
    const httpOptions = {
      headers: new HttpHeaders({
			  'Content-Type': 'application/json'
			  //'X-API-Key': API_KEY,
      }),
    };

    const body = JSON.stringify(loginInfo);
    this.http.post<User>(this._apiUrl+'/login', body, httpOptions)
    .subscribe((user: User) => {
      //this.sessionService.setUser(user);
      onSuccess();
    });
  }
}
