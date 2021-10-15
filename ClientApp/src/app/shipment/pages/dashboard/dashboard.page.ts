import { Component, OnInit } from '@angular/core';
import { Box, MappedData, ShipmentTableData } from '../../models/shipment-table.model';
import { SessionService } from '../../services/shipment-session.service';
import { ShipmentService } from '../../services/shipment.service';


@Component({
  selector: 'app-dashboard',
  templateUrl: './dashboard.page.html',
  styleUrls: ['./dashboard.page.scss']
})
export class DashboardPage implements OnInit {
  sortedData: MappedData[] = [];
  
  constructor(
    private readonly shipmentService: ShipmentService,
    private readonly sessionService: SessionService,
  ) { }

  ngOnInit(): void {
    this.shipmentService.getAllCurrent(async () => {
      const mappedData = this.mapShipments(this.sessionService.shipmentTableData!);
      this.sortedData = mappedData;
      console.log(this.sortedData)
    });

    console.log(this.shipmentService.getError())
  }

  get shipments(): ShipmentTableData[] {
    return this.sessionService.shipmentTableData!;
  }

  public mapShipments(shipments: ShipmentTableData[]) {
		return shipments.map((obj) => {
			return {
				id: obj.shipmentReadDTO.id,
        cost: obj.shipmentReadDTO.cost.toString(),
        weight: 1,
        status: obj.statusReadDTO.name.toString(),
        address: obj.shipmentReadDTO.address.toString(),
        receiverName: obj.shipmentReadDTO.firstName.toString()+" "+obj.shipmentReadDTO.lastName.toString(),
        date: new Date(obj.date).toDateString(),
        boxes: this.mapBoxes(obj.shipmentReadDTO.boxes)
			};    
		});
	 }

   public mapBoxes(boxes: Box[]) {
    return boxes.map((obj) => {
			return {
        color: obj.color.toString()
			};    
		});
   }

}
