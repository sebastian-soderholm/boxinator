import { Component, OnInit } from '@angular/core';
import { User } from '../../models/user.model';
import { AccountService } from '../../services/account.service';
import { SessionService } from 'src/app/shared/session.service'

@Component({
  selector: 'app-my-account',
  templateUrl: './my-account.page.html',
  styleUrls: ['./my-account.page.scss']
})
export class MyAccountPage implements OnInit {
  private _user: User | undefined;
  private _readonly: boolean = true;
  //private _adminSelectedUser : number;

  constructor(
    private readonly _accountService: AccountService,
    private readonly _sessionService: SessionService,
    ) { }

  ngOnInit(): void {
    this._user = this._sessionService.user;
    /*
    if(this._sessionService.user.role == admin) {
      this._accountService.getUserById(this._loggedInUserId, async () => {
        await console.log('retrieved user info')
        this._user = this._sessionService._userForAdmin;
      });    
    }
    */
  }

  get readonly(): boolean {
    return this._readonly;
  }

  get userInfo(): User | undefined {
    return this._user;
  }

}
