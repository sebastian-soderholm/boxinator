import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Router } from '@angular/router';
import { User } from 'src/app/account/models/user.model';
import { SessionService } from 'src/app/shared/session.service';
import { environment } from 'src/environments/environment';
import { RegisterUser } from '../models/register-user.model';
import { LoginService } from './login.service';

@Injectable({
  providedIn: 'root'
})
export class RegisterService {
  private _apiUrl = environment.baseURL;

  constructor(
    private readonly http: HttpClient,
    private readonly router: Router,
    private readonly sessionService: SessionService,
    private readonly loginService: LoginService
  ) {}

  public registerUser(registerUserInfo: RegisterUser, onSuccess: () => void): void {
    const token = this.sessionService.token;
    const id = this.sessionService.user!.id;
    const httpOptions = {
      headers: new HttpHeaders({
        'Content-Type': 'application/json',
        'Authorization': `Bearer ${token}`
      }),
    };
    const body = JSON.stringify(registerUserInfo);
    this.http.put<User>(this._apiUrl + '/account/' + id, body, httpOptions)
    .subscribe((user: User) => {
      this.sessionService.setUser(user);
      this.loginService.setLoggedIn(true);
      this.router.navigate(['/dashboard']);
      onSuccess();
    });
  }
}

