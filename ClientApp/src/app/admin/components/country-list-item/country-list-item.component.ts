import { Component, EventEmitter, Input, OnInit, Output } from '@angular/core';
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
  @Output() countrySavedEvent = new EventEmitter<Country>()

  private _country: Country | undefined
  private _zones: Zone[] = []

  //Set selected zone properties based on this country
  private _selectedZone: Zone | undefined;

  private _countryForm: FormGroup | any;

  constructor(
    private readonly sessionService: SessionService,
    private readonly countryService: CountryService,
    private readonly zoneService: ZoneService
  ) { }

  ngOnInit(): void {
    this._selectedZone = {
      id: this.country?.zoneId,
      name: this.country?.zoneName,
      countryMultiplier: this.country?.countryMultiplier
    }
    console.log("Selected country zone: ", this._selectedZone)

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

    console.log("Post country: ", this.country)
    this.countryService.updateCountry(this.country, () => this.countrySavedEvent.emit(this.country));

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
