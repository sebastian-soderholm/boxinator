import { Component, createPlatform, OnInit } from '@angular/core';
import { AccountService } from 'src/app/account/services/account.service';
import { SessionService } from 'src/app/shared/session.service';
import { User } from '../../../account/models/user.model';
import { FormGroup, FormControl, FormArray, Validators } from '@angular/forms';
import { UserService } from '../../services/user.service';
import { MatSnackBar } from '@angular/material/snack-bar';


@Component({
  selector: 'app-user-settings',
  templateUrl: './user-settings.component.html',
  styleUrls: ['./user-settings.component.scss']
})
export class UserSettingsComponent implements OnInit {
  //searchvalue: string = "";
  loadComponent: boolean = false;
  loadList: boolean = false;
  users: User[] | undefined;
  selectedUser: User | undefined;
  searchForm: FormGroup | any;
  searchvalue = new FormControl('', [Validators.required]);

  constructor(
    private readonly _accountService: AccountService,
    private readonly _sessionService: SessionService,
    private _snackBar: MatSnackBar
  ) {}

  ngOnInit(): void {
    this.searchForm = new FormGroup({
      searchvalue: new FormControl('', [
        Validators.required,
        Validators.pattern("[a-zA-Z0-9-ZÆæØøßÅÄÖåäö ]*"),
      ])
    });
  }

  selectUser(user: User) {
    this.loadComponent = true;
    this.selectedUser = user;
    this._sessionService.setFetchedUserInfo(user)
  }

  deleteUser(user: User) {
    //Delete user
    this._accountService.deleteUser(user.id).subscribe(response => {
      this._snackBar.open('User deleted!', 'OK');
      //Delete user from list
      this.users = this.users?.filter((findUser: User) => {
        return findUser.id !== user.id
      })
    },
    (error)=> {
      this._snackBar.open('Could not delete user, please try again.', 'OK');
    })
  }

  updateUser(user: any) {
    this._accountService.updateUserAsAdmin(user).subscribe(response => {
      this._snackBar.open('User updated!', 'OK');
      //Update userlist with new user
      this.users!.forEach((findUser: User) => {
        if(findUser.id === user.id) findUser = user
      })
    },
    (error) => {
      this._snackBar.open('Could not update user, please try again.', 'OK');
    })
  }

  public onSearch(): void {
    this.selectedUser = undefined;
    this.users = [];
    const searchInput = this.searchForm.get('searchvalue').value;
    this.loadComponent = false;
    this._sessionService.removeFetchedUserInfo();
    this._sessionService.removeFetchedUsersInfo();

    if(Number(searchInput)) {
      this._accountService.getUserById(Number(searchInput), async () => {
        if(this._sessionService.userForAdmin){
          this.users?.push(this._sessionService.userForAdmin)
          this.loadComponent = true;
        }
        else{
          this._snackBar.open('User not found', 'OK');
        }
      });
    }
    else {
      this._accountService.getBySearchTerm(String(searchInput), async () => {
        if(this._sessionService.usersForAdmin){
          this.users = this._sessionService.usersForAdmin;
          this.loadList = true
        }
        else{
          console.log("wtf")
          this._snackBar.open('User not found', 'OK');

        }
      });
    }

  }
}
