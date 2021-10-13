import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Router } from '@angular/router';
import { environment } from 'src/environments/environment';
import { RegisterUser } from '../models/register-user.model';

@Injectable({
  providedIn: 'root'
})
export class RegisterService {
  private _apiUrl = environment.baseURL;

  constructor(
    private readonly http: HttpClient,
    private readonly router: Router
  ) {}

  public registerUser(registerUserInfo: RegisterUser, onSuccess: () => void): void {
    const httpOptions = {
      headers: new HttpHeaders({
        'Content-Type': 'application/json'
        //'X-API-Key': API_KEY,
      }),
    };
    const body = JSON.stringify(registerUserInfo);
    this.http.post<RegisterUser>(this._apiUrl + '/account', body, httpOptions)
    .subscribe((user: RegisterUser) => {
      //this.sessionService.setUser(user);
      onSuccess();
    });
  }


}

