import { Component, OnInit } from '@angular/core';
import { FormGroup, FormControl } from '@angular/forms';
import { Country } from 'src/app/login/models/country.model';
import { CountryService } from 'src/app/login/services/country.service';
import { SessionService } from 'src/app/shared/session.service';

@Component({
  selector: 'app-settings',
  templateUrl: './settings.page.html',
  styleUrls: ['./settings.page.scss']
})
export class SettingsPage implements OnInit {
  panelOpenState = false;
  private _countries: Country[] = []
  private _countrySettingsForm: any;
  private _selectedCountry: Country = {
    id: 0,
    name: '',
    zoneId: 0,
  }

  constructor(
    private readonly countryService: CountryService,
    private readonly sessionService: SessionService,
  ) {
    this.countryService.fetchCountriesToSession(async () => {
      this._countries = this.sessionService.countries!;
      console.table(this._countries)
    });

    this._countrySettingsForm = new FormGroup({
      countrySelect: new FormControl(1, [
      ]),
    });

  }

  ngOnInit(): void {

    this.onChanges();
  }

  onChanges() {
    //Change selected country on select change
    this._countrySettingsForm.get("countrySelect").valueChanges.subscribe((id:any) => {
      this._selectedCountry = this._countries.find(country => {
        country.id === this._countrySettingsForm.get("countrySelect").value
      })!
      console.log(this._selectedCountry)
   })
  }
  get countries() {
    return this._countries
  }
  get countrySettingsForm() {
    return this._countrySettingsForm
  }
  get selectedCountry() {
    return this._selectedCountry
  }

}
