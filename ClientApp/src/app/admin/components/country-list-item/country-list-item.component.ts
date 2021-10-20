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
  @Input() country!: Country;

  private _zones: Zone[] = []
  private _countryForm: FormGroup | any;

  constructor(
    private readonly sessionService: SessionService,
    private readonly countryService: CountryService,
    private readonly zoneService: ZoneService
  ) {
    this._countryForm = new FormGroup({
      countryName: new FormControl(this.country?.name, [
        Validators.required,
        Validators.pattern(/[a-z]/)
      ]),
      countryZone: new FormControl(this.country?.zoneId, [
        Validators.required,
        Validators.pattern(/[a-z]/)
      ])
    })
  }

  ngOnInit(): void {
    this.zoneService.fetchZonesToSession(()=> {
      this._zones = this.sessionService.zones!;
    })
  }

  updateCountry(country: Country) {

    this.countryService.postCountry(country, () => console.log("country saved!"));

    // this.country.name = this._countryForm.get("countryName").value
    // this.country.zoneId = this._countryForm.get("countryZone").value

    console.table(this.country)
    // this.zoneService.fetchZonesToSession(async () => {
    //   this._zones = this.sessionService.zones!;
    // })
  }

  get countryForm() {
    return this._countryForm;
  }
  get zone() {
    return this._zones;
  }

}
