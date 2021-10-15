import { Component, Input, OnInit } from '@angular/core';
import { ShipmentTableData } from '../../models/shipment-table.model';

@Component({
  selector: 'app-dashboard-list-item',
  templateUrl: './dashboard-list-item.component.html',
  styleUrls: ['./dashboard-list-item.component.scss']
})
export class DashboardListItemComponent implements OnInit {
  @Input() shipment!: ShipmentTableData;
  constructor() { }

  ngOnInit(): void {
  }

}
