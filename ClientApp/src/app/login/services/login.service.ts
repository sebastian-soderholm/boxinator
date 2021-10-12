import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Router } from '@angular/router';
import { User } from '../models/user.model';
import firebase from 'firebase/compat/app'
import { AngularFireAuth } from '@angular/fire/compat/auth';
import { AngularFirestore, AngularFirestoreDocument } from '@angular/fire/compat/firestore';
import { Observable, of } from 'rxjs';
import { switchMap } from 'rxjs/operators';

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
      switchMap(user => {
        if(user) {
          return this.afs.doc<User>('users/${user.uid}').valueChanges();
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
    await this.afAuth.signOut();
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

  private updateUserData(user: any) {
    const userRef: AngularFirestoreDocument<User> = this.afs.doc('users/${user.uid')
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
    return userRef.set(data, { merge: true })
  }

}
