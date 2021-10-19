import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Router } from '@angular/router';
import { User } from '../account/models/user.model';
import { Shipment } from '../login/models/shipment.model'
import { environment } from 'src/environments/environment';
import { Country } from '../login/models/country.model';
import { ShipmentTableData } from '../shipment/models/shipment-table.model'

const apiURL = environment.baseURL;

@Injectable({
  providedIn: 'root',
})
export class SessionService {
  private _shipments: Shipment[] | undefined
  private _countries: Country[] = []
  private _user: User | undefined
  private _userForAdmin: User | undefined
  private _shipmentTableData: ShipmentTableData[] | undefined
  private _token: string | undefined;

  constructor() {
		const storedUser = sessionStorage.getItem('user');
		if (storedUser) {
		  this._user = JSON.parse(storedUser) as User;
		}
	}

  setToken(token: string) :void {
    this._token = token;
    sessionStorage.setItem('token', token)
  }

  setShipments(shipments: Shipment[]): void {
    this._shipments = shipments;
    sessionStorage.setItem('shipments', JSON.stringify(shipments))
  }

  setShipmentsTableData(shipments: ShipmentTableData[]): void {
    this._shipmentTableData = shipments;
    sessionStorage.setItem('shipmentTableData', JSON.stringify(shipments))
  }

  setCountries(countries: Country[]): void {
    this._countries = countries
    sessionStorage.setItem('countries', JSON.stringify(countries))
  }
  addCountry(country: Country) {
    this._countries.push(country)
  }

  updateCountry(country: Country) {
    const indexToReplace = this._countries.findIndex(c => country.id === c.id)

    if(indexToReplace) this._countries[indexToReplace] = country
  }
  setUser(user: User): void {
		this._user = user;
		sessionStorage.setItem('user', JSON.stringify(user))
	}

  // for admin only, set when admin is editing user's information
  setFetchedUserInfo(fetchedUser: User): void {
		this._userForAdmin = fetchedUser;
		sessionStorage.setItem('userForAdmin', JSON.stringify(fetchedUser))
	}

  get shipments(): Shipment[] | undefined {
    return this._shipments;
  }

  get shipmentTableData(): ShipmentTableData[] | undefined {
    return this._shipmentTableData;
  }

  get countries(): Country[] | undefined {
    return this._countries
  }

  get user(): User | undefined {
		return this._user;
	}

  get userForAdmin(): User | undefined {
		return this._userForAdmin;
	}

  get token(): string | undefined {
		return this._token;
	}
}
