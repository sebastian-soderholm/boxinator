import { Component, OnInit } from '@angular/core';
import { ShipmentService } from '../../services/shipment.service';
import { SessionService } from 'src/app/login/services/session.service';
import { Shipment } from 'src/app/login/models/shipment.model';

@Component({
  selector: 'app-my-shipments',
  templateUrl: './my-shipments.page.html',
  styleUrls: ['./my-shipments.page.scss']
})
export class MyShipmentsPage implements OnInit {

  displayedColumns: string[] = ['status', 'date', 'statusId'/*,'receiverName', 'cost'*/];
  columnsToDisplay: string[] = this.displayedColumns.slice();
  data: Shipment[] = [];


  constructor(
    private readonly shipmentService: ShipmentService,
    private readonly sessionService: SessionService
  ) {}

  ngOnInit() {
    this.shipmentService.getShipments(async () => {
      console.log(this.sessionService.shipments);
      this.data = this.sessionService.shipments!;
    });
    console.log(this.shipmentService.getError())
  }

  addColumn() {
    const randomColumn = Math.floor(Math.random() * this.displayedColumns.length);
    this.columnsToDisplay.push(this.displayedColumns[randomColumn]);
  }

  removeColumn() {
    if (this.columnsToDisplay.length) {
      this.columnsToDisplay.pop();
    }
  }

  shuffle() {
    let currentIndex = this.columnsToDisplay.length;
    while (0 !== currentIndex) {
      let randomIndex = Math.floor(Math.random() * currentIndex);
      currentIndex -= 1;

      // Swap
      let temp = this.columnsToDisplay[currentIndex];
      this.columnsToDisplay[currentIndex] = this.columnsToDisplay[randomIndex];
      this.columnsToDisplay[randomIndex] = temp;
    }
  }


}
