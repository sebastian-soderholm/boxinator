import { Component, Input, OnInit } from '@angular/core';
import { Country } from 'src/app/login/models/country.model';
import { CountryService } from 'src/app/login/services/country.service';
import { SessionService } from 'src/app/shared/session.service';
import { ZoneService } from '../../services/zone.service';
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
    console.log("country-list: ", countries)
  }


  private _countries: Country[] = []

  constructor(
    private readonly zoneService: ZoneService,
    private readonly countryService: CountryService,
    private readonly sessionService: SessionService
  ) {}

  ngOnInit(): void {
    // this.countryService.fetchCountriesToSession(async () => {
    //   this.countries = this.sessionService.countries!;
    //   console.log("Countries fetched!", this.countries)
    // });
    // this.countries?.subscribe((countries: Country[]) => {

    // })
  }

  get countries(){
    return this._countries
  }

}
