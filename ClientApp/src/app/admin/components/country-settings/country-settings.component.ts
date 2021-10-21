import { Component, OnInit } from '@angular/core';
import { FormGroup, FormControl, FormArray, Validators } from '@angular/forms';
import { Country } from 'src/app/login/models/country.model';
import { CountryService } from 'src/app/login/services/country.service';
import { SessionService } from 'src/app/shared/session.service';
import { CountriesAndZones } from '../../models/countries-zones.model';
import { Zone } from '../../models/zone.model';
import { ZoneService } from '../../services/zone.service';

@Component({
  selector: 'app-country-settings',
  templateUrl: './country-settings.component.html',
  styleUrls: ['./country-settings.component.scss'],
})
export class CountrySettingsComponent implements OnInit {

  // private _countries: Country[] = [];
  // private _zones: Zone[] = [];

  zoneSelectForm: FormGroup | any
  zoneSelectControl: FormControl | any
  selectedZone: Zone | undefined
  countries: Country[] | undefined
  zones: Zone[] | undefined

  constructor(
    private readonly zoneService: ZoneService,
    private readonly countryService: CountryService,
    private readonly sessionService: SessionService
  ) {
    this.zoneService.fetchZonesToSession(async () => {
      this.zones = this.sessionService.zones!;
      // console.log("Settings page zones: ", this.zones)
    });
  }

  ngOnInit(): void {
    this.zoneSelectForm = new FormGroup({
      zoneSelectControl: new FormControl(this.zones, [
        Validators.required
      ]),
    });
  }

  zoneSelected() {


    this.zoneService.fetchZoneCountriesToSession(this.zoneSelectForm.get("zoneSelectControl").value.id, async () => {
      this.countries = this.sessionService.countries
      // console.log("Fetching countries: ", this.countries)
    })
  }
  //Update zone info
  saveZone() {

    console.log("ZoneControl",this.zoneSelectForm.get("zoneSelectControl"))
    const putZone: Zone = {
      id: this.zoneSelectForm.get("zoneSelectControl").value.id,
      name: this.zoneSelectForm.get("zoneSelectControl").value.name,
      countryMultiplier: this.zoneSelectForm.get("zoneSelectControl").value.countryMultiplier
    }
    console.log("Zone saved: ", putZone)

    // this.zoneService.updateZone(zone)
  }
  onChanges() {
  }

  countrySave(editedCountry: any) {
  }

  setCountriesForm() {}

}
