import { Component, OnInit } from '@angular/core';
import { FormGroup, FormControl, FormArray, Validators } from '@angular/forms';
import { MatSnackBar } from '@angular/material/snack-bar';
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
  zoneSelectControl: FormControl | undefined;
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
    private readonly sessionService: SessionService,
    private _snackBar: MatSnackBar
  ) {

  }

  ngOnInit(): void {
    this.zoneService.fetchZonesToSession(async () => {
      this.zones = this.sessionService.zones!;
    });

    this.zoneSelectForm = new FormGroup({
      zoneSelectControl: new FormControl([Validators.required]),
      zoneNameControl: new FormControl(this.selectedZone?.name, [
        Validators.required,
        Validators.pattern("[a-zA-ZÆæØøßÅÄÖåäö ]*"),
      ]),
      zoneMultiplierControl: new FormControl([
        Validators.required
      ]),
    });

    this.addCountryForm = new FormGroup({
      addCountryName: new FormControl(this.addCountry!.name, [
        Validators.required,
        Validators.pattern("[a-zA-ZÆæØøßÅÄÖåäö ]*"),
      ]),
      // addCountryZone: new FormControl("",[
      //   Validators.required,
      //   Validators.pattern(/[a-z]/),
      // ]),
    })

    //Select zone event listener, fetch countries every time zone changes
    this.zoneSelectForm.get("zoneSelectControl")!.valueChanges.subscribe(() => {
      this.countries = []
      this.zoneService.fetchZoneCountriesToSession(
        this.zoneSelectForm.get('zoneSelectControl').value.id, async () => {
          this.countries = this.sessionService.countries;
        }
      );

      this.selectedZone = this.zoneSelectForm.get('zoneSelectControl').value
    })

    //Add country to zone select event listener
    this.addCountryForm.get("addCountryZone")?.valueChanges.subscribe((zone: Zone) => {
      this.selectedCountryZone = zone
    })
  }

  //Event from child components when country data is saved
  countrySaved(country: Country) {
    //If country zone was changed away from current selected zone, filter out from countries list
    if(country.zoneId !== this.selectedZone?.id) {
      console.log("Removing country from this zone...")
      this.countries = this.countries?.filter((c: Country) => {
        return c.id !== country.id
      })
    }

    // this.countries = this.sessionService.countries
  }

  addCountryToZone() {
    this.addCountry.name = this.addCountryForm.get("addCountryName").value
    this.addCountry.zoneId =  this.selectedZone!.id

    this.countryService.postCountry(this.addCountry).subscribe((responseCountry: Country) => {
      //Get zone name info for country
      this.zones?.forEach((zone: Zone) => {
        if(zone.id === responseCountry.zoneId) {
          responseCountry.zoneId = zone.id;
          responseCountry.zoneName = zone.name;
          responseCountry.countryMultiplier = zone.countryMultiplier
        }
      })
      //Add country to sessionService & countries array
      this.sessionService.addCountry(responseCountry)

      this._snackBar.open('Country added!', 'OK', {
        duration: 1500
      });
    },
    (error)=> {
      this._snackBar.open('Could not add country, please try again.', 'OK');
    })
  }

  //Update zone info
  saveZone() {
    // if (this.editedZone.id === 0) return;

    this.editedZone.id = this.selectedZone?.id!;
    this.editedZone.name = this.zoneSelectForm.get('zoneNameControl').value;
    this.editedZone.countryMultiplier = this.zoneSelectForm.get('zoneMultiplierControl').value;

    this.zoneService.updateZone(this.editedZone).subscribe((responseZone: Zone) => {
      //Add country to sessionService & countries array
      this.sessionService.updateZone(responseZone)
      this.zones = this.sessionService.zones;

      this._snackBar.open('Zone updated!', 'OK', {
        duration: 1500
      });
    },
    (error)=> {
      this._snackBar.open('Could not update zone, please try again.', 'OK');
    })

  }
  onChanges() {}

  countrySave(editedCountry: any) {}

  setCountriesForm() {}

}
