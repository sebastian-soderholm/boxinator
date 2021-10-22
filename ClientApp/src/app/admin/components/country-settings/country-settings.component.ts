import { Component, OnInit } from '@angular/core';
import { FormGroup, FormControl, FormArray, Validators } from '@angular/forms';
import { Country } from 'src/app/login/models/country.model';
import { CountryService } from 'src/app/login/services/country.service';
import { SessionService } from 'src/app/shared/session.service';
import { CountryAdd } from '../../models/country-add';
import { Zone } from '../../models/zone.model';
import { ZoneService } from '../../services/zone.service';

@Component({
  selector: 'app-country-settings',
  templateUrl: './country-settings.component.html',
  styleUrls: ['./country-settings.component.scss'],
})
export class CountrySettingsComponent implements OnInit {
  zoneSelectForm: FormGroup | any;
  zoneSelectControl: FormControl | any;
  selectedZone: Zone | undefined;
  countries: Country[] | undefined;
  zones: Zone[] | undefined;
  editedZone: Zone = {
    id: 0,
    name: '',
    countryMultiplier: 0,
  };

  addCountryForm: FormGroup | any;
  addCountry: CountryAdd = {
    name: '',
    zoneId: 0,
  };

  selectedCountryZone: Zone | undefined;

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
      zoneSelectControl: new FormControl(this.zones, [Validators.required]),
      zoneNameControl: new FormControl([]),
      zoneMultiplierControl: new FormControl([]),

      addCountryName: new FormControl(this.addCountry!.name, [
        Validators.required,
        Validators.pattern(/[a-z]/),
      ]),
      addCountryZone: new FormControl(this.zones, [
        Validators.required,
        Validators.pattern(/[a-z]/),
      ]),
    });
  }

  zoneSelected() {
    this.countries = []
    // this.selectedZone = this.zoneSelectForm.get("zoneSelectControl").value
    console.log(this.zoneSelectForm.get('zoneSelectControl').value);

    this.zoneService.fetchZoneCountriesToSession(
      this.zoneSelectForm.get('zoneSelectControl').value.id,
      async () => {
        this.countries = this.sessionService.countries;
        // console.log("Fetching countries: ", this.countries)
      }
    );
    this.selectedZone = this.zoneSelectForm.get('zoneSelectControl').value;
  }

  addCountryToZone() {
    if(!this.zoneSelectForm.get("addCountryName").value || !this.zoneSelectForm.get("addCountryZone").value) return

    this.addCountry.name = this.zoneSelectForm.get("addCountryName").value
    this.addCountry.zoneId =  this.zoneSelectForm.get("addCountryZone").value.id
    console.log('Adding country', this.addCountry);

    this.countryService.postCountry(this.addCountry, () => {console.log("Country added", this.addCountry)})
  }
  addCountryChanged() {
    this.countryService.postCountry(this.addCountry, () => {})
  }

  //Update zone info
  saveZone() {
    // if (this.editedZone.id === 0) return;

    this.editedZone.id = this.selectedZone?.id!;
    this.editedZone.name = this.zoneSelectForm.get('zoneNameControl').value;
    this.editedZone.countryMultiplier = this.zoneSelectForm.get('zoneMultiplierControl').value;

    console.log('Saving zone: ', this.editedZone);
    this.zoneService.updateZone(this.editedZone, () => this.zones = this.sessionService.zones!);
  }
  onChanges() {}

  countrySave(editedCountry: any) {}

  setCountriesForm() {}
}
