import {
  HttpClient,
	HttpErrorResponse,
	HttpResponse,
	HttpHeaders,
  HttpParams
} from '@angular/common/http';

import { Injectable } from '@angular/core';
import { ShipmentTableData, Status } from '../models/shipment-table.model';
import { environment } from 'src/environments/environment';
import { SessionService } from './shipment-session.service';

const apiUrl = environment.baseURL;

@Injectable({
  providedIn: 'root'
})

//Service for getting ongoing/completed shipments and creating shipments for customer/admin and guest
export class ShipmentService {
  private _error: string = '';

  constructor(private readonly http: HttpClient, private readonly sessionService: SessionService) {
  }

  // get all current shipments
  public getAllCurrent(onSuccess: () => void): void {
    this.http.get<ShipmentTableData[]>(apiUrl + '/shipments')
    .subscribe((shipments: ShipmentTableData[]) => { 
      console.log(shipments)
      this.sessionService.setShipments(shipments);
      onSuccess();
    },
    (error: HttpErrorResponse) => {
      this._error = error.message;
    })
  }

  // get filtered shipments
  public getFilteredShipments(statusFilter: number | null, dateFromFilter: Date[], dateToFilter: Date[], onSuccess: () => void): void {
    const params = new HttpParams()
    .set("statusFilter", statusFilter != null ? statusFilter.toString() : "")
    .set("dateFromFilter", dateFromFilter != null ? dateFromFilter.toString() : "")
    .set("dateToFilter", dateToFilter != null ? dateToFilter.toString() : "")

    this.http.get<ShipmentTableData[]>(apiUrl + "/shipments/filtered", {params: params})
    .subscribe((shipments: ShipmentTableData[]) => { 
      console.log(shipments)
      this.sessionService.setShipments(shipments);
      onSuccess();

    },(error: HttpErrorResponse) => {
      this._error = error.message;
    })
  }

  public getError(): string {
    return this._error;
  }
}

