import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { SessionService } from 'src/app/login/services/session.service';
import { environment } from 'src/environments/environment';
import { User } from '../models/user.model';

@Injectable({
  providedIn: 'root'
})
export class AccountService {

  private _apiUrl = environment.baseURL;

  constructor(
    private readonly http: HttpClient,
    private readonly sessionService: SessionService
  ) { }

  public updateUser(updateUserInfo: User, onSuccess: () => void): void {
    const httpOptions = {
      headers: new HttpHeaders({
        'Content-Type': 'application/json'
        //'X-API-Key': API_KEY,
      }),
    };
    const body = JSON.stringify(updateUserInfo);
    this.http.put<User>(this._apiUrl + '/account/' + updateUserInfo.id, body, httpOptions)
    .subscribe((user: User) => {
      //this.sessionService.setUser(user);
      onSuccess();
    });
  }
}
