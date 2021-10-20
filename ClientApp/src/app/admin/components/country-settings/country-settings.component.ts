import { Component, OnInit } from '@angular/core';
import { FormGroup, FormControl, FormArray, Validators } from '@angular/forms';
import { Country } from 'src/app/login/models/country.model';
import { CountryService } from 'src/app/login/services/country.service';
import { SessionService } from 'src/app/shared/session.service';
import { Zone } from '../../models/zone.model';
import { ZoneService } from '../../services/zone.service';

@Component({
  selector: 'app-country-settings',
  templateUrl: './country-settings.component.html',
  styleUrls: ['./country-settings.component.scss'],
})
export class CountrySettingsComponent implements OnInit {
  private _countries: Country[] = [];
  private _zones: Zone[] = [];
  private _settingsForm: any;
  private _zonesSelect: any;
  private _countryForm: any;
  private _countriesFormArray: FormArray | any;
  private _selectedCountry: Country = {
    id: 0,
    name: '',
    zoneId: 0,
    zoneName: '',
    countryMultiplier: 0,
  };
  private _selectedZone: Zone = {
    id: 0,
    name: '',
    countryMultiplier: 0,
  };

  constructor(
    private readonly zoneService: ZoneService,
    private readonly countryService: CountryService,
    private readonly sessionService: SessionService
  ) {
    this.zoneService.fetchZonesToSession(async () => {
      this._zones = this.sessionService.zones!;
    });
    this.countryService.fetchCountriesToSession(async () => {
      this._countries = this.sessionService.countries!;
    });

    this._settingsForm = new FormGroup({
      zoneSelect: this._zonesSelect = new FormControl(1, []),
      countriesFormArray: this._countriesFormArray = new FormArray([]),
    });
  }

  ngOnInit(): void {
    // this.onChange({});
  }

  onChanges() {
    this._selectedZone = this._settingsForm.get('zoneSelect').value;

    this.setCountriesForm();
  }

  countrySave(editedCountry: Country) {
    console.table(editedCountry)
    const countryFormArray = this._countriesFormArray as FormArray;

    // const country: Country = {
    //   id: countryFormArray.at(i).get('countryName')?.value,
    //   name: countryFormArray.at(i).get('countryName')?.value,
    //   zoneId: 0,
    //   zoneName: '',
    //   countryMultiplier: 0
    // }
  }

  setCountriesForm() {
    this._countriesFormArray = new FormArray([]);

    this._countries.forEach((country) => {
      if (country.zoneId === this.selectedZone.id) {
        console.log("Populating form with country: ", country)
        //New form group & add to form array
        const countryForm = new FormGroup({
          uneditedCountry: new FormControl(country),
          countryName: new FormControl(country.name, [Validators.required]),
          countryZone: new FormControl(country.zoneName, [Validators.required]),
        })
        console.table(countryForm)
        this._countriesFormArray.push(countryForm)
      }
    });
  }

  get zones() {
    return this._zones;
  }
  get selectedZone() {
    return this._selectedZone;
  }
  get countries() {
    return this._countries;
  }
  get countriesFormArray() {
    return this._settingsForm.get("countriesFormArray") as FormArray;
  }
  // get countriesFormArray() {
  //   return this._countriesFormArray as FormArray;
  // }
  get settingsForm() {
    return this._settingsForm;
  }
  get selectedCountry() {
    return this._selectedCountry;
  }
}
