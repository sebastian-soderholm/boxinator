import { HttpClient, HttpHeaders, HttpParams } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { environment } from 'src/environments/environment';
import { User } from '../models/user.model';
import { SessionService } from 'src/app/shared/session.service';

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

  // for admin
  public getUserById(userId: number, onSuccess: () => void): void {
    this.http.get<User>(this._apiUrl + /account/+userId)
    .subscribe((user: User) => { 
      console.log(user)
      this.sessionService.setFetchedUserInfo(user);
      onSuccess();
    });
  }

  public getBySearchTerm(searchTerm: string , onSuccess: () => void): void {
    const params = new HttpParams()
    .set("searchTerm", searchTerm)

    this.http.get<User>(this._apiUrl + '/account', {params: params})
    .subscribe((user: User) => { 
      console.log(user)
      if(user != null){
        this.sessionService.setFetchedUserInfo(user);
        onSuccess();
      }
    });
  }
}
