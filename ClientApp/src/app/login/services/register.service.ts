import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Router } from '@angular/router';
import { User } from 'src/app/account/models/user.model';
import { ExtensionsService } from 'src/app/shared/extensions.service';
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
    private readonly loginService: LoginService,
    private readonly extensionService: ExtensionsService
  ) {}

  public registerUser(registerUserInfo: RegisterUser) {
    const token = this.sessionService.token;
    const id = this.sessionService.user!.id;
    const httpOptions = {
      headers: new HttpHeaders({
        'Content-Type': 'application/json',
        'Authorization': `Bearer ${token}`
      }),
    };
    console.log("Sending request: ", this._apiUrl + '/account/' + id, registerUserInfo, this.extensionService.authenticationHeadersFull)
    return this.http.put<User>(this._apiUrl + '/account/' + id, registerUserInfo, this.extensionService.authenticationHeadersFull)
  }
}

