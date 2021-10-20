import { Component, OnInit, Input } from '@angular/core';
import { FormGroup, FormControl, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { passwordsMatch } from 'src/app/login/pages/register/fields-match';
import { LoginService } from 'src/app/login/services/login.service';
import { RegisterService } from 'src/app/login/services/register.service';
import { User } from '../../models/user.model';
import { AccountService } from '../../services/account.service';
import { SessionService } from 'src/app/shared/session.service';

@Component({
  selector: 'app-edit-account',
  templateUrl: './edit-account.page.html',
  styleUrls: ['./edit-account.page.scss'],
})
export class EditAccountPage implements OnInit {
  private _editUser: User | undefined;
  private _editUserForm: any;
  private _confirmPassword: string = '';
  @Input() showAdminSelection: boolean = false;

  constructor(
    private readonly _loginService: LoginService,
    private readonly _accountService: AccountService,
    private readonly _sessionService: SessionService,
    private readonly _router: Router
  ) {}

  ngOnInit(): void {
    this._editUser = this.showAdminSelection == true ? this._sessionService.userForAdmin : this._sessionService.user;

    this._editUserForm = new FormGroup(
      {
        firstName: new FormControl(this._editUser!.firstName, [
          Validators.required,
          //Must contain letters
          Validators.pattern(/[a-z]/gi),
        ]),
        lastName: new FormControl(this._editUser!.lastName, [
          Validators.required,
          //Must contain letters
          Validators.pattern(/[a-z]/gi),
        ]),
        email: new FormControl(this._editUser!.email, [
          Validators.required,
          //Must be in email format
          Validators.pattern(
            /^[a-zA-Z0-9.!#$%&'*+/=?^_`{|}~-]+@[a-zA-Z0-9-]+(?:\.[a-zA-Z0-9-]+)*$/
          ),
        ]),
        dateOfBirth: new FormControl(this._editUser!.dateOfBirth, [
          // Validators.pattern(/a-zA-Z/)
        ]),
        countryId: new FormControl(this._editUser!.countryId, [
          // Validators.pattern(/[a-z]/gi)
        ]),
        zipCode: new FormControl(this._editUser!.zipCode, [
          //Must be a minimum length
          Validators.minLength(5),
          //Must contain only numbers
          Validators.pattern(/^[0-9]*$/),
        ]),
        phoneNumber: new FormControl(this._editUser!.phoneNumber, [
          //Must be a valid phone number format
          // Validators.pattern(/^(\+\d{1,2}\s)?\(?\d{3}\)?[\s.-]\d{3}[\s.-]\d{4}$/)
        ]),
      },
      { validators: passwordsMatch }
    );
  }

  updateUser() {
    this._editUser!.firstName = this._editUserForm.get('firstName').value;
    this._editUser!.lastName = this._editUserForm.get('lastName').value;
    this._editUser!.email = this._editUserForm.get('email').value;
    this._editUser!.dateOfBirth = this._editUserForm.get('dateOfBirth').value
    // this._editUser.dateOfBirth = "2021-10-12T18:00:15.956Z"
    this._editUser!.countryId = this._editUserForm.get('countryId').value;
    this._editUser!.zipCode = this._editUserForm.get('zipCode').value;
    this._editUser!.phoneNumber = this._editUserForm.get('phoneNumber').value;


    console.table(this._editUser)
    // this._accountService.updateUser(this._editUser?, function () {
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
  get confirmPassword() {
    return this._editUserForm.get('confirmPassword');
  }
  get dateOfBirth() {
    return this._editUserForm.get('dateOfBirth');
  }
  get countryId() {
    return this._editUserForm.get('countryId');
  }
  get zipCode() {
    return this._editUserForm.get('zipCode');
  }
  get phoneNumber() {
    return this._editUserForm.get('phoneNumber');
  }
}
