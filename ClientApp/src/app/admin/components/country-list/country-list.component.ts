import { Component, Input, OnInit } from '@angular/core';
import { Country } from 'src/app/login/models/country.model';
import { CountryService } from 'src/app/login/services/country.service';
import { SessionService } from 'src/app/shared/session.service';
import { ZoneService } from '../../services/zone.service';
import { Zone } from '../../models/zone.model';
import { CountryListItemComponent } from '../country-list-item/country-list-item.component';

@Component({
  selector: 'app-country-list',
  templateUrl: './country-list.component.html',
  styleUrls: ['./country-list.component.scss']
})
export class CountryListComponent implements OnInit {
  @Input()
  set countries(countries: Country[]) {
    this._countries = countries
  }
  @Input()
    set zones(zones: Zone[]) {
    this._zones = zones;
    console.log("country-list zones: ", this._zones)
  }

  private _countries: Country[] = []
  private _zones: Zone[] = []

  constructor(
    private readonly zoneService: ZoneService,
    private readonly countryService: CountryService,
    private readonly sessionService: SessionService
  ) {}

  ngOnInit(): void {
  }

  get countries(){
    return this._countries
  }
  get zones(){
    return this._zones
  }

}
