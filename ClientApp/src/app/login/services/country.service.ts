import { HttpClient, HttpErrorResponse } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { environment } from 'src/environments/environment';
import { Country } from '../models/country.model';
import { SessionService } from './session.service';

@Injectable({
  providedIn: 'root'
})
export class CountryService {
  private _apiUrl = environment.baseURL;
  private _error: string = '';
  // private _countries: Country[] = [];

  constructor(
    private readonly http: HttpClient,
    private readonly sessionService: SessionService) { }

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

  public getError(): string {
    return this._error;
  }

}
