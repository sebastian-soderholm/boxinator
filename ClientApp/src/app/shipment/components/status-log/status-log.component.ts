import { Component, Input, OnInit } from '@angular/core';
import { ShipmentStatusLog } from '../../models/shipment-table.model';

@Component({
  selector: 'app-status-log',
  templateUrl: './status-log.component.html',
  styleUrls: ['./status-log.component.scss']
})
export class StatusLogComponent implements OnInit {
  @Input() status!: ShipmentStatusLog;
  constructor() { }

  ngOnInit(): void {
  }

}
