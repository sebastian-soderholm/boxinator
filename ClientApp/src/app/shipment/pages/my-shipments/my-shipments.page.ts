import { Component, OnInit } from '@angular/core';
import { ShipmentService } from '../../services/shipment.service';
import { SessionService } from '../../services/shipment-session.service';
import { ShipmentTableData } from '../../models/shipment-table.model';
import { MappedData } from '../../models/shipment-table.model';
import { Box } from '../../models/shipment-table.model';
import {animate, state, style, transition, trigger} from '@angular/animations';
import { Data } from '@angular/router';
import {Sort} from '@angular/material/sort';

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
  sortedData: MappedData[] = [];

  constructor(
    private readonly shipmentService: ShipmentService,
    private readonly sessionService: SessionService
  ) {
    this.sortedData = this.dataSource.slice();
  }

  ngOnInit() {
    this.shipmentService.getAllCurrent(async () => {
      const mappedData = this.mapShipments(this.sessionService.shipmentTableData!);
      this.sortedData = mappedData;
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

   //---------------sorting
   sortData(sort: Sort) {
    const data = this.sortedData.slice();
    if (!sort.active || sort.direction === '') {
      this.sortedData = data;
      return;
    }

    this.sortedData = data.sort((a, b) => {
      const isAsc = sort.direction === 'asc';
      switch (sort.active) {
        case 'id': return compare(a.id, b.id, isAsc);
        case 'cost': return compare(a.cost, b.cost, isAsc);
        case 'weight': return compare(a.weight, b.weight, isAsc);
        case 'status': return compare(a.status, b.status, isAsc);
        case 'receiverName': return compare(a.receiverName, b.receiverName, isAsc);
        case 'date': return compare(a.date, b.date, isAsc);
        default: return 0;
      }
    });

    function compare(a: number | string, b: number | string, isAsc: boolean) {
      return (a < b ? -1 : 1) * (isAsc ? 1 : -1);
    }
  }

}
