import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Router } from '@angular/router';
import { ShipmentTableData, Status } from '../models/shipment-table.model'

@Injectable({
  providedIn: 'root',
})
export class SessionService {
  private _shipmentTableData: ShipmentTableData[] | undefined
  private _statusOptions: Status[] | undefined

  setShipments(shipments: ShipmentTableData[]): void {
    this._shipmentTableData = shipments;
    localStorage.setItem('shipmentTableData', JSON.stringify(shipments))
  }

  get shipmentTableData(): ShipmentTableData[] | undefined {
    return this._shipmentTableData;
  }

}
