import { Component, OnInit } from '@angular/core';
import { ShipmentTableData } from '../../models/shipment-table.model';
import { SessionService } from '../../services/shipment-session.service';


@Component({
  selector: 'app-dashboard',
  templateUrl: './dashboard.page.html',
  styleUrls: ['./dashboard.page.scss']
})
export class DashboardPage implements OnInit {

  constructor(
    private readonly sessionService: SessionService,
  ) { }

  ngOnInit(): void {
  }

  get shipments(): ShipmentTableData[] {
    return this.sessionService.shipmentTableData!;
  }

}
