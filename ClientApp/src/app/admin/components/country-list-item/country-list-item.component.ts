import { Component, Input, OnInit } from '@angular/core';
import { Country } from 'src/app/login/models/country.model';
import { CountryService } from 'src/app/login/services/country.service';
import { SessionService } from 'src/app/shared/session.service';
import { ZoneService } from '../../services/zone.service';
import { Zone } from '../../models/zone.model';
import { FormControl, FormGroup, Validators } from '@angular/forms';


@Component({
  selector: 'app-country-list-item',
  templateUrl: './country-list-item.component.html',
  styleUrls: ['./country-list-item.component.scss']
})
export class CountryListItemComponent implements OnInit {
  @Input()
  set country(country: Country) {
    this._country = country;
  }
  get country() {
    return this._country!
  }
  @Input()
  set zones(zones: Zone[]) {
    this._zones = zones;
  }


  private _country: Country | undefined

  private _zones: Zone[] = []
  private _selectedZone: Zone | undefined;
  private _countryForm: FormGroup | any;

  constructor(
    private readonly sessionService: SessionService,
    private readonly countryService: CountryService,
    private readonly zoneService: ZoneService
  ) { }

  ngOnInit(): void {

    this._countryForm = new FormGroup({
      countryName: new FormControl(this.country.name, [
        Validators.required,
        Validators.pattern(/[a-z]/)
      ]),
      countryZone: new FormControl(this.country.zoneName, [
        Validators.required,
        Validators.pattern(/[a-z]/)
      ])
    })
  }

  zoneSelected(selectedZone: Zone) {
    this._selectedZone = selectedZone;
  }
  saveCountry() {
    const postCountry = {
      id: this._country?.id,
      name: this._countryForm.get("countryName").value,
      zoneId: this._selectedZone!.id
    }

    console.log("Post country: ", postCountry)
    // this.countryService.updateCountry(this.country, () => console.log("country saved!"));
  }

  get countryForm() {
    return this._countryForm;
  }
  get zones() {
    return this._zones;
  }
}
