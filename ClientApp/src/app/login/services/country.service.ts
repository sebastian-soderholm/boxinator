import { HttpClient, HttpErrorResponse, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { environment } from 'src/environments/environment';
import { Country } from '../models/country.model';
import { SessionService } from 'src/app/shared/session.service';

@Injectable({
  providedIn: 'root'
})
export class CountryService {
  private _apiUrl = environment.baseURL;
  private _error: string = '';

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
  //Add new country to DB & sessionService
  public postCountry(country: Country, onSuccess: () => void): void {
    const httpOptions = {
      headers: new HttpHeaders({
        'Content-Type': 'application/json'
        //'X-API-Key': API_KEY,
      }),
    };

    this.http.post<Country>(this._apiUrl + '/settings/countries', country, httpOptions)
    .subscribe((country: Country) => {
      //Set country to session service on success
      this.sessionService.addCountry(country)
      onSuccess()
    },
    (error: HttpErrorResponse) => {
      this._error = error.message;
    })
  }
  //Add new country to DB & sessionService
  public updateCountry(country: Country, onSuccess: () => void): void {
    const httpOptions = {
      headers: new HttpHeaders({
        'Content-Type': 'application/json'
        //'X-API-Key': API_KEY,
      }),
    };

    this.http.put<Country>(this._apiUrl + '/settings/countries', country, httpOptions)
    .subscribe((country: Country) => {
      //Update country in sessionService
      this.sessionService.updateCountry(country)
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
