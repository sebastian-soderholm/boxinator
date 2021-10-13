import { Component, OnInit } from '@angular/core';
import { FormGroup, FormControl, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { passwordsMatch } from 'src/app/login/pages/register/fields-match';
import { LoginService } from 'src/app/login/services/login.service';
import { RegisterService } from 'src/app/login/services/register.service';
import { User } from '../../models/user.model';
import { AccountService } from '../../services/account.service';

@Component({
  selector: 'app-edit-account',
  templateUrl: './edit-account.page.html',
  styleUrls: ['./edit-account.page.scss'],
})
export class EditAccountPage implements OnInit {
  private _editUser: User = {
    id: 1,
    firstName: 'Martta',
    lastName: 'Johnsson',
    email: 'awesomemartta@gs.com',
    password: '610650',
    dateOfBirth: new Date(Date.now()),
    countryId: 1,
    zip: '610650',
    contactNumber: '16064650210',
  };
  private _editUserForm: any;
  private _confirmPassword: string = '';

  constructor(
    private readonly _loginService: LoginService,
    private readonly _accountService: AccountService,
    private readonly _router: Router
  ) {}

  ngOnInit(): void {
    this._editUserForm = new FormGroup(
      {
        firstName: new FormControl(this._editUser.firstName, [
          Validators.required,
          //Must contain letters
          Validators.pattern(/[a-z]/gi),
        ]),
        lastName: new FormControl(this._editUser.lastName, [
          Validators.required,
          //Must contain letters
          Validators.pattern(/[a-z]/gi),
        ]),
        email: new FormControl(this._editUser.email, [
          Validators.required,
          //Must be in email format
          Validators.pattern(
            /^[a-zA-Z0-9.!#$%&'*+/=?^_`{|}~-]+@[a-zA-Z0-9-]+(?:\.[a-zA-Z0-9-]+)*$/
          ),
        ]),
        password: new FormControl(this._editUser.password, [
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
        dateOfBirth: new FormControl(this._editUser.dateOfBirth, [
          // Validators.pattern(/a-zA-Z/)
        ]),
        countryId: new FormControl(this._editUser.countryId, [
          // Validators.pattern(/[a-z]/gi)
        ]),
        zip: new FormControl(this._editUser.zip, [
          //Must be a minimum length
          Validators.minLength(5),
          //Must contain only numbers
          Validators.pattern(/^[0-9]*$/),
        ]),
        contactNumber: new FormControl(this._editUser.contactNumber, [
          //Must be a valid phone number format
          // Validators.pattern(/^(\+\d{1,2}\s)?\(?\d{3}\)?[\s.-]\d{3}[\s.-]\d{4}$/)
        ]),
      },
      { validators: passwordsMatch }
    );
  }

  updateUser() {
    this._editUser.firstName = this._editUserForm.get('firstName').value;
    this._editUser.lastName = this._editUserForm.get('lastName').value;
    this._editUser.email = this._editUserForm.get('email').value;
    this._editUser.password = this._editUserForm.get('password').value;
    // this._editUser.dateOfBirth = this._editUserForm.get('dateOfBirth').value
    // this._editUser.dateOfBirth = "2021-10-12T18:00:15.956Z"
    this._editUser.countryId = this._editUserForm.get('countryId').value;
    this._editUser.zip = this._editUserForm.get('zip').value;
    this._editUser.contactNumber = this._editUserForm.get('contactNumber').value;


    console.log("Editing user: " + JSON.stringify(this._editUser))
    // this._accountService.updateUser(this._editUser, function () {
    //   console.log('User updated successfully!');
    // });
  }
  get editUserForm() {
    return this._editUserForm;
  }
  //Form fields
  get firstName() {
    return this._editUserForm.get('firstName');
  }
  get lastName() {
    return this._editUserForm.get('lastName');
  }
  get email() {
    return this._editUserForm.get('email');
  }
  get password() {
    return this._editUserForm.get('password');
  }
  get confirmPassword() {
    return this._editUserForm.get('confirmPassword');
  }
  get dateOfBirth() {
    return this._editUserForm.get('dateOfBirth');
  }
  get countryId() {
    return this._editUserForm.get('countryId');
  }
  get zip() {
    return this._editUserForm.get('zip');
  }
  get contactNumber() {
    return this._editUserForm.get('contactNumber');
  }
}
