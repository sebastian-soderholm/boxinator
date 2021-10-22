import {
  HttpClient,
	HttpErrorResponse,
	HttpHeaders,
  HttpParams
} from '@angular/common/http';

import { Injectable } from '@angular/core';
import { ShipmentStatusLog, ShipmentTableData, Status } from '../models/shipment-table.model';
import { environment } from 'src/environments/environment';
//import { SessionService } from './shipment-session.service';
import { SessionService } from 'src/app/shared/session.service';
import { GuestShipment } from '../models/guest-shipment.model';
import { CreateShipment } from '../models/create-shipment.model';
import { ExtensionsService } from 'src/app/shared/extensions.service';
import { EditShipment } from '../models/edit-shipment.model';

const apiUrl = environment.baseURL;

@Injectable({
  providedIn: 'root'
})

//Service for getting ongoing/completed shipments and creating shipments for customer/admin and guest
export class ShipmentService {
  private _error: string = '';
  private _shipment: CreateShipment | undefined;

  constructor(private readonly http: HttpClient, 
    private readonly sessionService: SessionService, 
    private readonly extensionService: ExtensionsService) {
  }

  // get shipment by id
  public getById(shipmentId: number, onSuccess: () => void): any {
    this.http.get<CreateShipment>(apiUrl + '/shipments/' + shipmentId, this.extensionService.authenticationHeadersFull)
    .subscribe((shipment: CreateShipment) => {
      this.sessionService.setEditableShipment(shipment);
      onSuccess();
    },
    (error: HttpErrorResponse) => {
      this._error = error.message;
    })
  }

  // get shipment observable by id
  public getByIdObservable(shipmentId: number): any {
    return this.http.get<EditShipment>(apiUrl + '/shipments/' + shipmentId, this.extensionService.authenticationHeadersFull);
  }

  // get all current shipments
  public getAllCurrent(onSuccess: () => void): void {
    this.http.get<ShipmentTableData[]>(apiUrl + '/shipments', this.extensionService.authenticationHeadersFull)
    .subscribe((shipments: ShipmentTableData[]) => {
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

    console.log("Sending shipment..." + JSON.stringify(shipment))
    this.http.post<CreateShipment[]>(apiUrl + '/shipments/', body, this.extensionService.authenticationHeadersFull)
    .subscribe((createdShipment: any) => {
      onSuccess();
    },
    (error: HttpErrorResponse) => {
      this._error = error.message;
      console.table(error)
    })
  }

  //add new status
  public addNewStatusLog(shipmentId: number, onSuccess: () => void) : void {
    this.http.get<ShipmentStatusLog>(apiUrl + '/shipments/log/' +shipmentId, this.extensionService.authenticationHeadersFull)
    .subscribe((newStatusLog: ShipmentStatusLog) => {
      let shipmentTableDataArray = this.sessionService!.shipmentTableData;
      let shipment = shipmentTableDataArray!.find(l => l.id == newStatusLog.shipmentId);
      shipment?.shipmentStatusLogs.push(newStatusLog);

      this.sessionService.setShipmentsTableData(shipmentTableDataArray!);

      onSuccess();
    },
    (error: HttpErrorResponse) => {
      this._error = error.message;
      console.table(error)
    })
  }

  // update shipment
  public updateShipment(shipmentId: number, shipment: EditShipment, onSuccess: () => void) : void {
    const body = shipment;

    this.http.put<EditShipment>(apiUrl + '/shipments/' +shipmentId, body, this.extensionService.authenticationHeadersFull)
    .subscribe((updatedShipment: EditShipment) => {
      console.log(updatedShipment);
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

  public getFetchedShipment(): CreateShipment {
    return this._shipment!;
  }
}

