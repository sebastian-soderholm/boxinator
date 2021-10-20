import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Router } from '@angular/router';
import firebase from 'firebase/compat/app';
import { AngularFireAuth } from '@angular/fire/compat/auth';
import { environment } from 'src/environments/environment';
import { User } from '../../account/models/user.model';
import { SessionService } from 'src/app/shared/session.service';

@Injectable({
  providedIn: 'root',
})
export class LoginService {
  private _user: User | undefined;
  private _loggedIn: boolean = false;
  private _apiUrl = environment.baseURL;

  constructor(
    private readonly http: HttpClient,
    private readonly router: Router,
    private readonly sessionService: SessionService,
    private afAuth: AngularFireAuth,
  ) {
    const sessionStorageUser = sessionStorage.getItem('user');
    if (sessionStorageUser) {
      this._user = JSON.parse(sessionStorageUser) as User;
      this._loggedIn = true;
    }
  }

  get user(): User | undefined {
    return this._user;
  }

  setUser(user: User): void {
    this._user = user;
    sessionStorage.setItem('user', JSON.stringify(user));
    this._loggedIn = true;
  }

  get loggedIn(): boolean {
    return this._loggedIn;
  }

  setLoggedIn(loggedIn: boolean): void {
    this._loggedIn = loggedIn;
  }

  async logout() {
    await this.afAuth.signOut(); // Sign out from Firebase
    this._user = undefined;
    sessionStorage.removeItem('user');
    sessionStorage.removeItem('token');
    this._loggedIn = false;
    this.router.navigate(['']);
  }




  // get req, call another method (post) if necessary
  public verifyUser(token: string): void {
    const httpOptions = {
      headers: new HttpHeaders({
        'Content-Type': 'application/json',
        'Authorization': `Bearer ${token}`,
      }),
    };
    this.http
      .get<User>(this._apiUrl + '/login/verify', httpOptions)
      .subscribe((user: User) => {
        this.sessionService.setUser(user);
        this._user = user;
        if(this._user.firstName != null ) {
          this._loggedIn = true;
          this.router.navigate(['/dashboard']);
        } else {
          this.router.navigate(['/register']);
        }
      });
  }

  async googleLogin(onSuccess: () => void) {
    const provider = new firebase.auth.GoogleAuthProvider();
    await this.afAuth.signInWithPopup(provider).then(async function (result: any) {
      await result.user.getIdToken().then((token:any) => {
        sessionStorage.setItem('token', token);
      })
      onSuccess();
    });
  }
}
