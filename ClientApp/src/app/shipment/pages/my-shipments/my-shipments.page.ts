import { Component, OnInit } from '@angular/core';
import { ShipmentService } from '../../services/shipment.service';
import { SessionService } from '../../services/shipment-session.service';
import { ShipmentTableData } from '../../models/shipment-table.model';
import { MappedData } from '../../models/shipment-table.model';

@Component({
  selector: 'app-my-shipments',
  templateUrl: './my-shipments.page.html',
  styleUrls: ['./my-shipments.page.scss']
})
export class MyShipmentsPage implements OnInit {

  displayedColumns: string[] = ['id', 'cost', 'weight', 'status', 'receiverName', 'statusId', 'date'];
  columnsToDisplay: string[] = this.displayedColumns.slice();
  data: MappedData[] = [];

  constructor(
    private readonly shipmentService: ShipmentService,
    private readonly sessionService: SessionService
  ) {}

  ngOnInit() {
    this.shipmentService.getShipments(async () => {
      var mappedData = this.mapShipments(this.sessionService.shipmentTableData!);
      this.data = mappedData;
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
 
  public mapShipments(shipments: ShipmentTableData[]) {
		return shipments.map((obj) => {
			return {
				id: obj.shipmentReadDTO.id,
        cost: obj.shipmentReadDTO.cost.toString(),
        weight: 1,
        status: obj.statusReadDTO.name.toString(),
        receiverName: obj.shipmentReadDTO.receiverName.toString(),
        date: obj.date.toString()
			};    
		});
	 }

}
