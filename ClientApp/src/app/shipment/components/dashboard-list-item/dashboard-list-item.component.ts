import { Component, Input, OnInit } from '@angular/core';
import { ShipmentTableData } from '../../models/shipment-table.model';

@Component({
  selector: 'app-dashboard-list-item',
  templateUrl: './dashboard-list-item.component.html',
  styleUrls: ['./dashboard-list-item.component.scss']
})
export class DashboardListItemComponent implements OnInit {
  @Input() shipment!: ShipmentTableData;
  totalWeight: number = 0;
  currentStatus: string = ""
  basicBoxes: string[] = [];
  humbleBoxes: string[] = [];
  deluxeBoxes: string[] = [];
  premiumBoxes: string[] = [];
  panelOpenState = false;

  constructor() {

  }

  ngOnInit(): void {
    this.shipment.boxes.forEach((box) => {
      this.totalWeight += box.weight
      if(box.name === "Basic") {
        this.basicBoxes.push(box.color)
      } else if(box.name === "Humble") {
        this.humbleBoxes.push(box.color)
      } else if(box.name === "Deluxe") {
      this.deluxeBoxes.push(box.color)
      } else if(box.name === "Premium") {
        this.premiumBoxes.push(box.color)
      }
    })
    this.currentStatus = this.shipment.shipmentStatusLogs[this.shipment.shipmentStatusLogs.length-1].status.name
  }

}
