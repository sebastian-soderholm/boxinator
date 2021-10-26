import { Component, Input, OnInit } from '@angular/core';
import { ShipmentTableData } from '../../models/shipment-table.model';

@Component({
  selector: 'app-dashboard-list',
  templateUrl: './dashboard-list.component.html',
  styleUrls: ['./dashboard-list.component.scss']
})
export class DashboardListComponent implements OnInit {
  @Input() shipments: ShipmentTableData[] | undefined;
  constructor() { }

  ngOnInit(): void {
  }

}
