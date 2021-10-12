import { Component, OnInit } from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { RegisterUser } from '../../models/register-user.model';
import { LoginService } from '../../services/login.service';
import { FieldsMatch } from './fields-match'

@Component({
  selector: 'app-register',
  templateUrl: './register.page.html',
  styleUrls: ['./register.page.scss']
})
export class RegisterPage implements OnInit {
  private _registerUser: RegisterUser = {
    firstName: "",
    lastName: "",
    email: "",
    password: "",
    dateOfBirth: new Date(Date.now()),
    country: "",
    zip: "",
    contactNumber: "",
  }
  private _registerForm: any
  private _confirmPassword: string = ""

  constructor(
    private readonly loginService: LoginService,
    private readonly router: Router,
  ) { }

  ngOnInit(): void {
    if(this.loginService.loggedIn) {
      this.router.navigate(['dashboard'])
    }
    this._registerForm = new FormGroup({
      firstName: new FormControl(this._registerUser.firstName, [
        Validators.required,
        //Must contain letters
        Validators.pattern(/[a-z]/gi)
      ]),
      lastName: new FormControl(this._registerUser.lastName, [
        Validators.required,
        //Must contain letters
        Validators.pattern(/[a-z]/gi)
      ]),
      email: new FormControl(this._registerUser.email, [
        Validators.required,
        //Must be in email format
        Validators.pattern(/^[a-zA-Z0-9.!#$%&'*+/=?^_`{|}~-]+@[a-zA-Z0-9-]+(?:\.[a-zA-Z0-9-]+)*$/),
      ]),
      password: new FormControl(this._registerUser.password, [
        Validators.required,
        Validators.minLength(5),
        //At least one lowercase letter, one uppercase letter, one number, one special character
        // Validators.pattern(/^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[@$!%*?&])[A-Za-z\d@$!%*?&]$/)
      ]),
      confirmPassword: new FormControl(this._confirmPassword, [
        Validators.required,
        Validators.minLength(5),
        //At least one lowercase letter, one uppercase letter, one number, one special character
        // Validators.pattern(/^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[@$!%*?&])[A-Za-z\d@$!%*?&]$/),
        // FieldsMatch("password", "confirmPassword")
      ]),
      dateOfBirth: new FormControl(this._registerUser.dateOfBirth, [
        // Validators.pattern(/a-zA-Z/)
      ]),
      country: new FormControl(this._registerUser.country, [
        //Must contain only letters
        Validators.pattern(/[a-z]/gi)
      ]),
      zip: new FormControl(this._registerUser.zip, [
        //Must be a minimum length
        Validators.minLength(5),
        //Must contain only numbers
        Validators.pattern(/^[0-9]*$/)
      ]),
      contactNumber: new FormControl(this._registerUser.contactNumber, [
        //Must be a valid phone number format
        // Validators.pattern(/^(\+\d{1,2}\s)?\(?\d{3}\)?[\s.-]\d{3}[\s.-]\d{4}$/)
      ]),
    });
  }

  register(): void {
    this._registerUser.firstName = this._registerForm.get('firstName').value
    this._registerUser.lastName = this._registerForm.get('lastName').value
    this._registerUser.email = this._registerForm.get('email').value
    this._registerUser.password = this._registerForm.get('password').value
    this._registerUser.dateOfBirth = this._registerForm.get('dateOfBirth').value
    this._registerUser.dateOfBirth = this._registerForm.get('country').value
    this._registerUser.dateOfBirth = this._registerForm.get('zip').value
    this._registerUser.dateOfBirth = this._registerForm.get('contactNumber').value

    console.log("Register user: " + JSON.stringify(this._registerUser))
  }
  get registerForm() {
    return this._registerForm
  }
  //Form fields
  get firstName() {
    return this._registerForm.get('firstName')
  }
  get lastName() {
    return this._registerForm.get('lastName')
  }
  get email() {
    return this._registerForm.get('email')
  }
  get password() {
    return this._registerForm.get('password')
  }
  get confirmPassword() {
    return this._confirmPassword
  }
  get dateOfBirth() {
    return this._registerForm.get('dateOfBirth')
  }
  get country() {
    return this._registerForm.get('country')
  }
  get zip() {
    return this._registerForm.get('zip')
  }
  get contactNumber() {
    return this._registerForm.get('contactNumber')
  }
}
