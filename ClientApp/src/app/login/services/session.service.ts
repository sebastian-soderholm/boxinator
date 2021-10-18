import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Router } from '@angular/router';
import { User } from '../../account/models/user.model';
import { Shipment } from '../models/shipment.model'
import { environment } from 'src/environments/environment';
import { Country } from '../models/country.model';


const apiURL = environment.baseURL;

@Injectable({
  providedIn: 'root',
})
export class SessionService {
  private _user: User | undefined;
  private _shipments: Shipment[] | undefined
  private _countries: Country[] | undefined

  setUser(user: User): void {
    this._user = user;
    localStorage.setItem('user', JSON.stringify(user))
  }

  setShipments(shipments: Shipment[]): void {
    this._shipments = shipments;
    localStorage.setItem('shipments', JSON.stringify(shipments))
  }

  setCountries(countries: Country[]): void {
    this._countries = countries
    sessionStorage.setItem('countries', JSON.stringify(countries))
  }

  get user(): User | undefined {
    return this._user;
  }

  get shipments(): Shipment[] | undefined {
    return this._shipments;
  }

  get countries(): Country[] | undefined {
    return this._countries
  }
}
