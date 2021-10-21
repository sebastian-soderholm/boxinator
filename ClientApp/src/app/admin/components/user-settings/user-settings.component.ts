import { Component, OnInit } from '@angular/core';
import { NgForm } from '@angular/forms';
import { AccountService } from 'src/app/account/services/account.service';
import { SessionService } from 'src/app/shared/session.service';
import { User } from '../../../account/models/user.model';

@Component({
  selector: 'app-user-settings',
  templateUrl: './user-settings.component.html',
  styleUrls: ['./user-settings.component.scss']
})
export class UserSettingsComponent implements OnInit {
  searchvalue: string = "";
  loadComponent: boolean = false;
  loadList: boolean = false;
  users: User[] | undefined;
  selectedUser: User | undefined;

  constructor(
    private readonly _accountService: AccountService,
    private readonly _sessionService: SessionService,
  ) { }

  ngOnInit(): void {
  }

  selectUser(user: User) {
    this.loadComponent = true;
    this.selectedUser = user;
    this._sessionService.setFetchedUserInfo(user)
  }

  public onSearch(input: string): void {
    this.loadComponent = false;
    if(Number(input)) {
      this._accountService.getUserById(Number(input), async () => {
        await console.log("success"), this.loadComponent = true;
      });
    }
    else {
      this._accountService.getBySearchTerm(input, async () => {
        await console.log("success"),
        this.users = this._sessionService.usersForAdmin;
        this.loadList = true//, this.loadComponent = true;
      });
    }

  }
}
