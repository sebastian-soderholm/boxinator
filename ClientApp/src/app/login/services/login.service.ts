import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Router } from '@angular/router';
import firebase from 'firebase/compat/app';
import { AngularFireAuth } from '@angular/fire/compat/auth';
import { AngularFirestore } from '@angular/fire/compat/firestore';
import { Observable, of } from 'rxjs';
import { switchMap } from 'rxjs/operators';
import { environment } from 'src/environments/environment';
import { LoginUser } from '../models/login-user.model';
import { User } from '../../account/models/user.model';
import { SessionService } from './session.service';

@Injectable({
  providedIn: 'root',
})
export class LoginService {
  user$: Observable<User | null | undefined>;
  private _user: User | undefined;
  private _loginUser: LoginUser | undefined;
  private _jwt: string = '';
  private _loggedIn: boolean = false;
  private _apiUrl = environment.baseURL;

  constructor(
    private readonly http: HttpClient,
    private readonly router: Router,
    private readonly sessionService: SessionService,
    private afAuth: AngularFireAuth,
    private afs: AngularFirestore
  ) {
    this.user$ = this.afAuth.authState.pipe(
      // Get the auth state, then fetch the Firestore user document or return null
      switchMap((user) => {
        // Logged in
        if (user) {
          return this.afs.doc<User>('users/${user.uid}').valueChanges();
          // Logged out
        } else {
          return of(null);
        }
      })
    );
    const localStorageUser = localStorage.getItem('user');
    if (localStorageUser) {
      this._user = JSON.parse(localStorageUser) as User;
      console.log(localStorageUser + ' logged in');
      this._loggedIn = true;
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

  async logout() {
    await this.afAuth.signOut(); // Sign out from Firebase
    this._user = undefined;
    localStorage.removeItem('user');
    this._loggedIn = false;
    this.router.navigate(['']);
  }

  get loggedIn(): boolean {
    return this._loggedIn;
  }

  // get req, call another method (post) if necessary
  public verifyUser(token: string): void {
    console.log("testi",token);
    const httpOptions = {
      headers: new HttpHeaders({
        'Content-Type': 'application/json',
        Authorization: `Bearer ${token}`,
      }),
    };
    this.http
      .get<User>(this._apiUrl + '/login/verify', httpOptions)
      .subscribe((user: User) => {
        this.sessionService.setUser(user);
        console.log(user);
      });
  }

  async googleLogin(onSuccess: () => void) {
    const provider = new firebase.auth.GoogleAuthProvider();
    await this.afAuth.signInWithPopup(provider).then(function (result: any) {
      result.user.getIdToken().then((token:any) => {
        console.log("oikee",token);
        localStorage.setItem('token', token);
        
      })
      /*       
      console.log(result.credential.idToken)
      var token = result.credential.accessToken;
      var user = result.user;
      let newUser: LoginUser = {
        email: user.email,
        password: 'x',
      }
      localStorage.setItem("user", JSON.stringify(newUser)); */
      localStorage.setItem('token', result.credential.idToken);
      onSuccess();
    });
    /*console.log(result.credential.idToken)
      var token = user.getIdToken(true).then((idToken: any) => {
        const data = {
          email: user.email,
          password: 'x',
        }
        let newUser: LoginUser = {
          email: user.email,
          password: 'x',
        }
        localStorage.setItem("user", JSON.stringify(newUser));
        localStorage.setItem("token", JSON.stringify(result.credential.idToken));
        console.log(idToken);

      })
      alert("login OK" + token);
      const data = {
        email: user.email,
        password: 'x',
      }
      let newUser: LoginUser = data
      localStorage.setItem("token", JSON.stringify(token));
      localStorage.setItem("user", JSON.stringify(newUser));
      .then((result: any) => {
      console.log(result)
      const userRef: AngularFirestoreDocument<LoginUser> = this.afs.doc(`users/${result.user.uid}`)
      userRef.set(result.user, { merge: true })
    })
    */
  }
}
