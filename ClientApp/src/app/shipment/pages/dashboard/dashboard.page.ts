import { Component, OnInit } from '@angular/core';
import {
  Box,
  MappedData,
  ShipmentStatusLog,
  ShipmentTableData,
} from '../../models/shipment-table.model';
import { ShipmentService } from '../../services/shipment.service';
import { SessionService } from 'src/app/shared/session.service';

@Component({
  selector: 'app-dashboard',
  templateUrl: './dashboard.page.html',
  styleUrls: ['./dashboard.page.scss'],
})
export class DashboardPage implements OnInit {
  sortedData: MappedData[] = [];

  constructor(
    private readonly shipmentService: ShipmentService,
    private readonly sessionService: SessionService
  ) {}

  ngOnInit(): void {
    this.shipmentService.getAllCurrent(async () => {
      const mappedData = this.mapShipments(
        this.sessionService.shipmentTableData!
      );
      this.sortedData = mappedData;
    });
  }

  get shipments(): ShipmentTableData[] {
    return this.sessionService.shipmentTableData!;
  }

  public mapShipments(shipments: ShipmentTableData[]) {
    return shipments.map((obj) => {
      const currentBoxes = this.mapBoxes(obj.boxes);
      //const expandedData = this.createExpandedData(obj.id, currentBoxes, obj.shipmentStatusLogs)

      return {
        id: obj.id,
        cost: obj.cost,
        weight: 55,
        status: this.getStatusFromList(obj.id, obj.shipmentStatusLogs),
        address: obj.receiverAddress,
        receiverName: obj.receiverFirstName + ' ' + obj.receiverLastName,
        date: this.getDateFromList(obj.id, obj.shipmentStatusLogs),
        //expandedData: expandedData//this.createExpandedData(obj.id, obj.boxes, currentdate)
        //boxes: this.mapBoxes(obj.boxes),
        //shipmentStatusLogs: obj.shipmentStatusLogs
      };
    });
  }
  public getDateFromList(shipmentId: number, logs: ShipmentStatusLog[]) {
    const date = logs.find((o) => o.shipmentId === shipmentId)!.date;
    return new Date(date).toDateString();
  }

  public getStatusFromList(shipmentId: number, logs: ShipmentStatusLog[]) {
    return logs.find((o) => o.shipmentId === shipmentId)!.status.name;
  }
  public mapBoxes(boxes: Box[]) {
    return boxes.map((obj) => {
      return {
        color: obj.color.toString(),
      };
    });
  }
}
