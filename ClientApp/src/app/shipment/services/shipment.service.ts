import {
  HttpClient,
	HttpErrorResponse,
	HttpResponse,
	HttpHeaders
} from '@angular/common/http';
  
import { Injectable } from '@angular/core';
import { Shipment } from '../../login/models/shipment.model';
import { environment } from 'src/environments/environment';
import { SessionService } from 'src/app/login/services/session.service';

const apiUrl = environment.baseURL;

@Injectable({
  providedIn: 'root'
})

//Service for getting ongoing/completed shipments and creating shipments for customer/admin and guest
export class ShipmentService {
  private _error: string = '';

  constructor(private readonly http: HttpClient, private readonly sessionService: SessionService) { 
  }
  
  public getShipments(onSuccess: () => void): void {
    this.http.get<Shipment[]>(apiUrl + '/shipments')
    .subscribe((shipments: Shipment[]) => { 
      this.sessionService.setShipments(shipments);
      onSuccess();
    },
    (error: HttpErrorResponse) => {
      this._error = error.message;
    })
  }

  public getError(): string {
    return this._error;
  }
}

