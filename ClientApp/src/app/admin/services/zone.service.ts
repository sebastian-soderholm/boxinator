import { HttpClient, HttpErrorResponse, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Country } from 'src/app/login/models/country.model';
import { ExtensionsService } from 'src/app/shared/extensions.service';
import { SessionService } from 'src/app/shared/session.service';
import { environment } from 'src/environments/environment';
import { Zone } from '../models/zone.model';

@Injectable({
  providedIn: 'root'
})
export class ZoneService {
  private _apiUrl = environment.baseURL;
  private _error: string = '';

  constructor(
    private readonly http: HttpClient,
    private readonly sessionService: SessionService,
    private readonly extensionService: ExtensionsService
  ) { }

  // Zones
  public fetchZonesToSession(onSuccess: () => void): void {
    this.http.get<Zone[]>(this._apiUrl + '/settings/zones')
    .subscribe((zones: Zone[]) => {
      this.sessionService.setZones(zones)
      onSuccess()
    },
    (error: HttpErrorResponse) => {
      this._error = error.message;
    })
  }

  public fetchZoneCountriesToSession(zoneId: number, onSuccess: () => void) {
    this.http.get<Country[]>(this._apiUrl + '/settings/zones/' + zoneId)
    .subscribe((countries: Country[]) => {
      console.log("Requesting: " + this._apiUrl + '/settings/zones/' + zoneId)
      console.log("Recieved countries: ", countries)
      this.sessionService.setCountries(countries)
      onSuccess()
    },
    (error: HttpErrorResponse) => {
      this._error = error.message;
    })
  }

  //Update country
  public updateZone(zone: Zone): void {

    this.http.put<Zone>(this._apiUrl + '/settings/zones/' + zone.id, zone, this.extensionService.authenticationHeadersFull)
    .subscribe((zone: Zone) => {
      //Update country in sessionService
      this.sessionService.updateZone(zone)
    },
    (error: HttpErrorResponse) => {
      this._error = error.message;
    })
  }
}
