import { Component, OnInit } from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { CountryService } from '../../services/country.service';
import { Country } from '../../models/country.model';
import { RegisterUser } from '../../models/register-user.model';
import { LoginService } from '../../services/login.service';
import { SessionService } from '../../../shared/session.service';
import { passwordsMatch } from './fields-match';
import { MatSnackBar } from '@angular/material/snack-bar';
import { User } from 'src/app/account/models/user.model';
import { DateAdapter } from '@angular/material/core';
import { AccountService } from 'src/app/account/services/account.service';

@Component({
  selector: 'app-register',
  templateUrl: './register.page.html',
  styleUrls: ['./register.page.scss'],

})
export class RegisterPage implements OnInit {
  minDate = new Date(1900, 1, 1);
  maxDate = new Date(); // Today
  private _registerUser: RegisterUser = {
    id: this.sessionService.user!.id,
    firstName: '',
    lastName: '',
    email: this.sessionService.user!.email,
    countryId: null,
    zipCode: null,
    address: "",
    dateOfBirth: null,
    phoneNumber: null,
  };
  private _countries: Country[] = [];
  private _registerForm: any;

  constructor(
    private readonly loginService: LoginService,
    private readonly accountService: AccountService,
    private readonly router: Router,
    private readonly countryService: CountryService,
    private readonly sessionService: SessionService,
    private _snackBar: MatSnackBar,
    private _adapter: DateAdapter<any>
  ) {}

  ngOnInit(): void {
    //If logged in user is already registered or admin, redirect to dashboard
    if (this.loginService.loggedIn && this.loginService.user?.accountType !== 'GUEST') {
      this.router.navigate(['dashboard']);
    }
    this.countryService.fetchCountriesToSession(async () => {
      this._countries = this.sessionService.countries!;

    });

    this._registerForm = new FormGroup(
      {
        firstName: new FormControl(this._registerUser.firstName, [
          Validators.required,
          //Must contain letters
          Validators.pattern("[a-zA-ZÆæØøßÅÄÖåäö ]*"),
        ]),
        lastName: new FormControl(this._registerUser.lastName, [
          Validators.required,
          //Must contain letters
          Validators.pattern("[a-zA-ZÆæØøßÅÄÖåäö ]*"),
        ]),
        dateOfBirth: new FormControl(this._registerUser.dateOfBirth, [
          // Validators.pattern(/a-zA-Z/)
        ]),
        countries: new FormControl(this._registerUser.countryId, [
          Validators.required
          //Must contain only letters
          // Validators.pattern(/[a-z]/gi)
        ]),
        address: new FormControl(this._registerUser!.address, [
          Validators.required,
        ]),
        zipCode: new FormControl(this._registerUser.zipCode, [
          //Must be a minimum length
          Validators.minLength(5),
          //Must contain only numbers
          Validators.pattern(/^[0-9]*$/),
        ]),
        phoneNumber: new FormControl(this._registerUser.phoneNumber, [
          //Must be a valid phone number format
          // Validators.pattern(/^(\+\d{1,2}\s)?\(?\d{3}\)?[\s.-]\d{3}[\s.-]\d{4}$/)
        ]),
      },
      { validators: passwordsMatch }
    );
  }

  async register() {
    this._registerUser.firstName = this._registerForm.get('firstName').value;
    this._registerUser.lastName = this._registerForm.get('lastName').value;
    this._registerUser.dateOfBirth = this._registerForm.get('dateOfBirth').value;
    this._registerUser.address = this._registerForm.get('address').value;
    this._registerUser.countryId = this._registerForm.get('countries').value; //Apply countryId only if selected
    this._registerUser.zipCode = this._registerForm.get('zipCode').value;
    this._registerUser.phoneNumber = this._registerForm.get('phoneNumber').value;

    // Format date
    // this._registerUser.dateOfBirth = this._adapter.format(this._registerUser.dateOfBirth, "DD/MM/YYYY")

    //Send request
    await this.accountService.registerUser(this._registerUser!).subscribe(async (responseUser: User) => {
      await this.sessionService.setUser(responseUser);
      await this.loginService.setLoggedIn(true)

      this._snackBar.open('Thank you for registering, welcome to Boxinator!', "Ok")
      .afterDismissed()
      .subscribe(() => {
        this.router.navigate(['/dashboard']);
      });
    },
    (error)=> {
      this._snackBar.open('Could not register, please try again.', 'OK');
    },
    )
  }
  get registerForm() {
    return this._registerForm;
  }
  //Form fields
  get firstName() {
    return this._registerForm.get('firstName');
  }
  get lastName() {
    return this._registerForm.get('lastName');
  }
  get email() {
    return this._registerForm.get('email');
  }
  get dateOfBirth() {
    return this._registerForm.get('dateOfBirth');
  }
  get country() {
    return this._registerForm.get('country');
  }
  get countries() {
    return this._countries;
  }
  get address() {
    return this._registerForm.get('address')
  }
  get zipCode() {
    return this._registerForm.get('zipCode');
  }
  get phoneNumber() {
    return this._registerForm.get('phoneNumber');
  }
}

/*
      Email & password validation

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
        //At least one lowercase letter, one uppercase letter, one number, one special character
        // Validators.pattern(/^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[@$!%*?&])[A-Za-z\d@$!%*?&]$/),
      ]),
      */
