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
      // this.selectedZone = this.sessionService.zones![0]
      // this.zoneSelected()
    });
  }
  ngOnInit(): void {
    this.zoneSelectForm = new FormGroup({
      zoneSelectControl: new FormControl(this.zones, [
        Validators.required
      ]),
      zoneNameControl: new FormControl([]),
      zoneMultiplierControl: new FormControl([])
    });
  }
  zoneSelected() {
    // this.selectedZone = this.zoneSelectForm.get("zoneSelectControl").value
    // console.log(this.zoneSelectForm.get("zoneSelectControl").value)

    this.zoneService.fetchZoneCountriesToSession(this.zoneSelectForm.get("zoneSelectControl").value.id, async () => {
      this.countries = this.sessionService.countries
      // console.log("Fetching countries: ", this.countries)
    })
    this.selectedZone = this.zoneSelectForm.get("zoneSelectControl").value


  }
  //Update zone info
  saveZone(zone: Zone) {

    // console.log(this.zoneSelectForm.get("zoneNameControl").value.name, this.zoneSelectForm.get("zoneNameControl").value.countryMultiplier)

    this.selectedZone!.countryMultiplier = this.zoneSelectForm.get("zoneMultiplierControl").value

    const putZone: Zone = {
      id: this.zoneSelectForm.get("zoneSelectControl").value.id,
      name: this.zoneSelectForm.get("zoneNameControl").value,
      countryMultiplier: this.zoneSelectForm.get("zoneMultiplierControl").value
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
