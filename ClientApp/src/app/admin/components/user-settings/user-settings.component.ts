import { Component, createPlatform, OnInit } from '@angular/core';
import { AccountService } from 'src/app/account/services/account.service';
import { SessionService } from 'src/app/shared/session.service';
import { User } from '../../../account/models/user.model';
import { FormGroup, FormControl, FormArray, Validators } from '@angular/forms';

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

  public onSearch(): void {
    const searchInput = this.searchForm.get('searchvalue').value;
    this.loadComponent = false;

    if(Number(searchInput)) {
      this._accountService.getUserById(Number(searchInput), async () => {
        await console.log("success"), this.loadComponent = true;
      });
    }
    else {
      this._accountService.getBySearchTerm(String(searchInput), async () => {
        await console.log("success"),
        this.users = this._sessionService.usersForAdmin;
        this.loadList = true//, this.loadComponent = true;
      });
    }

  }
}
