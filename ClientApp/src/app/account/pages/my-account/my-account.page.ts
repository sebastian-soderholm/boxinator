import { Component, OnInit } from '@angular/core';
import { User } from '../../models/user.model';
import { AccountService } from '../../services/account.service';
import { SessionService } from 'src/app/shared/session.service'
import { CountryService } from 'src/app/login/services/country.service';
import { Country } from 'src/app/login/models/country.model';

@Component({
  selector: 'app-my-account',
  templateUrl: './my-account.page.html',
  styleUrls: ['./my-account.page.scss']
})
export class MyAccountPage implements OnInit {
  private _user: User | undefined;
  private _readonly: boolean = true;
  //private _adminSelectedUser : number;
  public userCountry: Country | undefined;

  constructor(
    private readonly _accountService: AccountService,
    private readonly _sessionService: SessionService,
    private readonly _countryService: CountryService
    ) { }

  ngOnInit(): void {
    this._user = this._sessionService.user;

    this._countryService.fetchCountriesToSession(async () => {
      this.userCountry = this._sessionService.countries?.find((country: Country) => {
        return country.id === this._user?.countryId
      })
    })
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
