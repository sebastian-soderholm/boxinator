import { HttpClient, HttpErrorResponse, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { environment } from 'src/environments/environment';
import { Country } from '../models/country.model';
import { SessionService } from 'src/app/shared/session.service';
import { ExtensionsService } from 'src/app/shared/extensions.service';
import { CountryAdd } from 'src/app/admin/models/country-add';

@Injectable({
  providedIn: 'root'
})
export class CountryService {
  private _apiUrl = environment.baseURL;
  private _error: string = '';

  constructor(
    private readonly http: HttpClient,
    private readonly sessionService: SessionService,
    private readonly extensionService: ExtensionsService) { }

  public fetchCountriesToSession(onSuccess: () => void): void {
    this.http.get<Country[]>(this._apiUrl + '/settings/countries')
    .subscribe((countries: Country[]) => {
      this.sessionService.setCountries(countries)
      onSuccess()
    },
    (error: HttpErrorResponse) => {
      this._error = error.message;
    })
  }
  //Add new country to DB & sessionService
  public postCountry(country: CountryAdd) {
    return this.http.post<Country>(this._apiUrl + '/settings/countries', country, this.extensionService.authenticationHeadersFull)
  }
  //Add new country to DB & sessionService
  public updateCountry(country: Country) {
    return this.http.put<Country>(this._apiUrl + '/settings/countries/' + country.id, country, this.extensionService.authenticationHeadersFull)
  }




  public getError(): string {
    return this._error;
  }

}
