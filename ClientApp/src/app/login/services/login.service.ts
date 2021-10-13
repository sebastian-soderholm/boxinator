import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Router } from '@angular/router';
import { User } from '../models/user.model';
import firebase from 'firebase/compat/app'
import { AngularFireAuth } from '@angular/fire/compat/auth';
import { AngularFirestore, AngularFirestoreDocument } from '@angular/fire/compat/firestore';
import { Observable, of } from 'rxjs';
import { switchMap } from 'rxjs/operators';
import { environment } from 'src/environments/environment';

const apiURL = "";

@Injectable({
  providedIn: 'root'
})
export class LoginService {
  user$: Observable<User | null | undefined>
  private _user: User | undefined;
  private _jwt: string = "";
  private _loggedIn: boolean = false;

  constructor(
    private readonly http: HttpClient,
    private readonly router: Router,
    private afAuth: AngularFireAuth,
    private afs: AngularFirestore
  ) {
    this.user$ = this.afAuth.authState.pipe(
      // Get the auth state, then fetch the Firestore user document or return null
      switchMap(user => {
        // Logged in
        if(user) {
          return this.afs.doc<User>('users/${user.uid}').valueChanges();
        // Logged out
        } else {
          return of(null)
        }
      })
    )
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

  async googleLogin() {
    const provider = new firebase.auth.GoogleAuthProvider();
    const credential = await this.afAuth.signInWithPopup(provider);
    return this.updateUserData(credential.user);
  }

  // Updates userdata in Firestore
  private updateUserData(user: any) {
    const userRef: AngularFirestoreDocument<User> = this.afs.doc(`users/${user.uid}`)
    console.log(user.email)
    const data = {
      id: user.uid,
      firstName: user.displayName,
      lastName: '',
      email: user.email,
      password: '',
      dateOfBirth: null,
      country: '',
      zip: '',
      contactNumber: ''
    }
    const newUser: User = data
    this.setUser(newUser)
    return userRef.set(data, { merge: true })
  }
} 

/*     const provider = new firebase.auth.GoogleAuthProvider();
    await this.afAuth.signInWithPopup(provider).then(function(result: any) {
      var token = result.credential.accessToken;
      var user = result.user;
      alert("login OK" + user.email);
      user.getToken().then(function (t: any) {
          token = t;
          this.addUser(user.email, token);
      }); 
        }).catch(function (error) {
            var errorCode = error.code;
            var errorMessage = error.message;
            alert(errorCode + " - " + errorMessage);
        });
  }

  public addUser(email: string, token: string) : Observable<User> {
    const httpOptions = {
      headers: new HttpHeaders({
        'Content-Type': 'application/json',
      }),
    };
    const body = JSON.stringify({
      email: email,
      token: token,
    });
    return this.http.post<User>(environment.API_URL + '/login', body, httpOptions);
  }

}  */