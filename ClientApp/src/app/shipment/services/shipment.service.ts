import {
  HttpClient,
	HttpErrorResponse,
	HttpHeaders,
  HttpParams
} from '@angular/common/http';

import { Injectable } from '@angular/core';
import { ShipmentTableData, Status } from '../models/shipment-table.model';
import { environment } from 'src/environments/environment';
//import { SessionService } from './shipment-session.service';
import { SessionService } from 'src/app/shared/session.service';
import { GuestShipment } from '../models/guest-shipment.model';
import { CreateShipment } from '../models/create-shipment.model';
import { ExtensionsService } from 'src/app/shared/extensions.service';

const apiUrl = environment.baseURL;

@Injectable({
  providedIn: 'root'
})

//Service for getting ongoing/completed shipments and creating shipments for customer/admin and guest
export class ShipmentService {
  private _error: string = '';

  constructor(private readonly http: HttpClient, 
    private readonly sessionService: SessionService, 
    private readonly extensionService: ExtensionsService) {
  }

  // get all current shipments
  public getAllCurrent(onSuccess: () => void): void {
    this.http.get<ShipmentTableData[]>(apiUrl + '/shipments', this.extensionService.authenticationHeadersFull)
    .subscribe((shipments: ShipmentTableData[]) => {
      console.log(shipments)
      this.sessionService.setShipmentsTableData(shipments);
      onSuccess();
    },
    (error: HttpErrorResponse) => {
      this._error = error.message;
    })
  }

  // get filtered shipments
  public getFilteredShipments(path: string | null, dateFromFilter: Date[], dateToFilter: Date[], onSuccess: () => void): void {
    const params = new HttpParams()
    .set("dateFromFilter", dateFromFilter != null ? dateFromFilter.toString() : "")
    .set("dateToFilter", dateToFilter != null ? dateToFilter.toString() : "")

    const token = this.sessionService.token;

		const httpOptions = {
		  headers: new HttpHeaders({
			'Content-Type': 'application/json',
			'Authorization': `Bearer ${token}`,
		  }),
		};

    this.http.get<ShipmentTableData[]>(apiUrl + path, { headers: httpOptions.headers, params: params})
    .subscribe((shipments: ShipmentTableData[]) => { 
      console.log(shipments)
      this.sessionService.setShipmentsTableData(shipments);
      onSuccess();

    },(error: HttpErrorResponse) => {
      this._error = error.message;
    })
  }
  //post new guest shipment
  public postNewGuestShipment(shipment: GuestShipment, onSuccess: () => void) : void {
    const body = shipment;
    const httpOptions = {
      headers: new HttpHeaders({
        'Content-Type': 'application/json',
      }),
    };
    console.log("Sending shipment..." + JSON.stringify(shipment))
    this.http.post<GuestShipment[]>(apiUrl + '/shipments/guest', body, httpOptions)
    .subscribe((createdShipment: any) => {
      onSuccess();
    },
    (error: HttpErrorResponse) => {
      this._error = error.message;
      console.table(error)
    })
  }
  //post new guest shipment
  public postNewShipment(shipment: CreateShipment, onSuccess: () => void) : void {
    const body = shipment;
    const httpOptions = {
      headers: new HttpHeaders({
        'Content-Type': 'application/json',
      }),
    };
    console.log("Sending shipment..." + JSON.stringify(shipment))
    this.http.post<CreateShipment[]>(apiUrl + '/shipments/', body, httpOptions)
    .subscribe((createdShipment: any) => {
      onSuccess();
    },
    (error: HttpErrorResponse) => {
      this._error = error.message;
      console.table(error)
    })
  }


  public getError(): string {
    return this._error;
  }
}

