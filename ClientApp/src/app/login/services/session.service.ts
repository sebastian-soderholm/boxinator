import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Router } from '@angular/router';
import { User } from '../models/user.model';
import { Shipment } from '../models/shipment.model'
import { environment } from 'src/environments/environment';


const apiURL = environment.baseURL;

@Injectable({
  providedIn: 'root',
})
export class SessionService {
  private _shipments: Shipment[] | undefined

  setShipments(shipments: Shipment[]): void {
    this._shipments = shipments;
    localStorage.setItem('shipments', JSON.stringify(shipments))
  }

  get shipments(): Shipment[] | undefined {
    return this._shipments;
  }
}
