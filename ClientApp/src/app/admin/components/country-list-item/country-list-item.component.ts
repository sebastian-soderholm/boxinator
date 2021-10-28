import { Component, EventEmitter, Input, OnInit, Output } from '@angular/core';
import { Country } from 'src/app/login/models/country.model';
import { CountryService } from 'src/app/login/services/country.service';
import { SessionService } from 'src/app/shared/session.service';
import { ZoneService } from '../../services/zone.service';
import { Zone } from '../../models/zone.model';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { MatSnackBar } from '@angular/material/snack-bar';


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
  @Output() countrySavedEvent = new EventEmitter<Country>()

  private _country: Country | undefined
  private _zones: Zone[] = []

  //Set selected zone properties based on this country
  private _selectedZone: Zone | undefined;

  private _countryForm: FormGroup | any;

  constructor(
    private readonly sessionService: SessionService,
    private readonly countryService: CountryService,
    private readonly zoneService: ZoneService,
    private _snackBar: MatSnackBar
  ) { }

  ngOnInit(): void {
    this._selectedZone = {
      id: this.country?.zoneId,
      name: this.country?.zoneName,
      countryMultiplier: this.country?.countryMultiplier
    }

    this._countryForm = new FormGroup({
      countryName: new FormControl(this.country.name, [
        Validators.required,
        Validators.pattern("[a-zA-ZÆæØøßÅÄÖåäö]*")
      ]),
      countryZone: new FormControl(this._selectedZone, [
        Validators.required,
      ])
    })
    this._countryForm.get("countryZone").valueChanges.subscribe((zone: Zone) => {
      this._selectedZone = zone
    })
  }

  saveCountry() {

    this.country.name = this._countryForm.get("countryName").value
    this.country.zoneId = this._selectedZone!.id
    this.country.zoneName = this._selectedZone!.name
    this.country.countryMultiplier = this._selectedZone!.countryMultiplier

    this.countryService.updateCountry(this.country);

    //Update countries in localStorage
    this.countryService.updateCountry(this.country).subscribe(response => {
      this.sessionService.updateCountry(this.country)
      this.countrySavedEvent.emit(this.country)
      this._snackBar.open('Country updated!', 'OK', {
        duration: 1500
      });
    },
    (error)=> {
      this._snackBar.open('Could not update country, please try again.', 'OK', {
        duration: 1500
      });
    })
  }

  get countryForm() {
    return this._countryForm;
  }
  get zones() {
    return this._zones;
  }
  get selectedZone() {
    return this._selectedZone
  }
}
