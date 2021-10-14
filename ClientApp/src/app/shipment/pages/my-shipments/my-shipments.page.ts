import { Component, OnInit } from '@angular/core';
import { ShipmentService } from '../../services/shipment.service';
import { SessionService } from '../../services/shipment-session.service';
import { ShipmentTableData } from '../../models/shipment-table.model';
import { MappedData } from '../../models/shipment-table.model';
import { Box } from '../../models/shipment-table.model';
import {animate, state, style, transition, trigger} from '@angular/animations';
import { Data } from '@angular/router';

@Component({
  selector: 'app-my-shipments',
  templateUrl: './my-shipments.page.html',
  styleUrls: ['./my-shipments.page.scss'],
  animations: [
    trigger('detailExpand', [
      state('collapsed', style({height: '0px', minHeight: '0'})),
      state('expanded', style({height: '*'})),
      transition('expanded <=> collapsed', animate('225ms cubic-bezier(0.4, 0.0, 0.2, 1)')),
    ]),
  ],
})
export class MyShipmentsPage implements OnInit {

  displayedColumns: string[] = ['id', 'cost', 'weight', 'status', 'receiverName', 'date'];
  columnsToDisplay: string[] = this.displayedColumns.slice();
  dataSource: MappedData[] = [];
  dateVisibility: boolean = true;

  constructor(
    private readonly shipmentService: ShipmentService,
    private readonly sessionService: SessionService
  ) {}

  ngOnInit() {
    this.shipmentService.getAllCurrent(async () => {
      const mappedData = this.mapShipments(this.sessionService.shipmentTableData!);
      this.dataSource = mappedData;
    });

    console.log(this.shipmentService.getError())
  }

  onValChange(selection: any, state : any){
    console.log(state)
    if(state == true) {
      this.removeColumn(selection);
    }
    else {
      this.addColumn(selection);
    }
  }

  addColumn(selection: any) {
    if(!this.columnsToDisplay.includes(selection)) {
      this.columnsToDisplay.push(selection);
    }
  }

  removeColumn(selection: any) {
    var index = this.columnsToDisplay.indexOf(selection);

    if (index !== -1) {
      var filteredAry = this.columnsToDisplay.filter(function(e) { return e !== selection })
      this.columnsToDisplay = filteredAry;
    }
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
        date: obj.date.toString(),
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
