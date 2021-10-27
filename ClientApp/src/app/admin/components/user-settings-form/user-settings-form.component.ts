import { DatePipe } from '@angular/common';
import { Component, EventEmitter, Input, OnChanges, OnInit, Output } from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { MatSnackBar } from '@angular/material/snack-bar';
import { Router } from '@angular/router';
import { EditUser } from 'src/app/account/models/edit-user.model';
import { User } from 'src/app/account/models/user.model';
import { AccountService } from 'src/app/account/services/account.service';
import { Country } from 'src/app/login/models/country.model';
import { passwordsMatch } from 'src/app/login/pages/register/fields-match';
import { CountryService } from 'src/app/login/services/country.service';
import { LoginService } from 'src/app/login/services/login.service';
import { SessionService } from 'src/app/shared/session.service';

@Component({
  selector: 'app-user-settings-form',
  templateUrl: './user-settings-form.component.html',
  styleUrls: ['./user-settings-form.component.scss']
})
export class UserSettingsFormComponent implements OnInit, OnChanges  {
  minDate = new Date(1900, 1, 1);
  maxDate = new Date(); // Today
  private _editUser: EditUser | undefined;
  private _countries: Country[] = [];
  private _editUserForm: any;
  private _confirmPassword: string = '';
  private _selectedDoB? : string | null;
  @Input() showAdminSelection: boolean = false;
  @Input() incomingUser: User | undefined;
  date1 = new FormControl(new Date());
  @Output()
  formSubmit: EventEmitter<EditUser> = new EventEmitter();

  constructor(
    private readonly _loginService: LoginService,
    private readonly _accountService: AccountService,
    private readonly _sessionService: SessionService,
    private readonly _router: Router,
    private readonly _countryService: CountryService,
    private readonly _datepipe: DatePipe,
    private _snackBar: MatSnackBar
  ) {

  }

  ngOnChanges() {
    this._editUser = this.showAdminSelection == true ? this._sessionService.userForAdmin : this._sessionService.user;
  }

  ngOnInit(): void {
    this._editUser = this.showAdminSelection == true ? this._sessionService.userForAdmin : this._sessionService.user;
    console.log(this._editUser)

    this._countryService.fetchCountriesToSession(async () => {
      this._countries = this._sessionService.countries!;
    });

    this._editUserForm = new FormGroup(
      {
        firstName: new FormControl(this._editUser!.firstName, [
          Validators.required,
          //Must contain letters
          Validators.pattern("[a-zA-ZÆæØøßÅÄÖåäö ]*"),
        ]),
        lastName: new FormControl(this._editUser!.lastName, [
          Validators.required,
          //Must contain letters
          Validators.pattern("[a-zA-ZÆæØøßÅÄÖåäö ]*"),
        ]),
        email: new FormControl(this._editUser!.email, [
          Validators.required,
          //Must be in email format
          Validators.pattern(
            /^[a-zA-Z0-9.!#$%&'*+/=?^_`{|}~-]+@[a-zA-Z0-9-]+(?:\.[a-zA-Z0-9-]+)*$/
          ),
        ]),
        dateOfBirth: new FormControl(this._editUser!.dateOfBirth/*"2021-10-19 12:17:18.6767435"*/, [
          // Validators.pattern(/a-zA-Z/)
        ]),
        countryId: new FormControl(this._editUser!.countryId, [
          // Validators.pattern(/[a-z]/gi)
        ]),
        address: new FormControl(this._editUser!.address, [
          Validators.required,
        ]),
        zipCode: new FormControl(this._editUser!.zipCode, [
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
    this._editUser!.dateOfBirth = this._editUserForm.get('dateOfBirth').value;
    this._editUser!.address = this._editUserForm.get('address').value;
    this._editUser!.countryId = this._editUserForm.get('countryId').value;
    this._editUser!.zipCode = this._editUserForm.get('zipCode').value;
    this._editUser!.phoneNumber = this._editUserForm.get('phoneNumber').value;

    //Emit event for parents
    this.formSubmit.emit(this._editUser);
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
  get countries() {
    return this._countries;
  }
  get zipCode() {
    return this._editUserForm.get('zipCode');
  }
  get address() {
    return this._editUserForm.get('address');
  }
  get phoneNumber() {
    return this._editUserForm.get('phoneNumber');
  }
  get userDateOfBirth() {
    return this._editUser?.dateOfBirth
  }

}
