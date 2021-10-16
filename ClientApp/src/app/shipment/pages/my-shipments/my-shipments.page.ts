import { Component, OnInit } from '@angular/core';
import { ShipmentService } from '../../services/shipment.service';
import { SessionService } from '../../services/shipment-session.service';
import { ShipmentStatusLog, ShipmentTableData, MappedData, ExpandedData, Status, Box } from '../../models/shipment-table.model';
import {animate, state, style, transition, trigger} from '@angular/animations';
import { Data } from '@angular/router';
import {Sort} from '@angular/material/sort';
import { expand } from 'rxjs/operators';
import { TestBed } from '@angular/core/testing';
import { FormGroup, NgForm, SelectControlValueAccessor } from '@angular/forms';

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
  statusOptions : Status[] = [
    {id: 1, name: "Created"},
    {id: 2, name: "Received"},
    {id: 3, name: "Intransit"},
    {id: 4, name: "Completed"},
    {id: 5, name: "Cancelled"}
  ]
  fromDate = null;
  toDate = null;
  selectedStatus = null;
  selectedFromDate = null;
  selectedToDate = null;

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

  }

  filterTable() {
    console.log("filteriiinnnng")
    console.log(this.selectedStatus)
    console.log(this.selectedFromDate)
    console.log(this.selectedToDate)
    this.shipmentService.getAllCurrent(async () => {
      const mappedData = this.mapShipments(this.sessionService.shipmentTableData!);
      this.sortedData = mappedData;
    });
  }

  setSelectedFilterOption(type: any, selected : any) {
    if(type == 'status'){
      this.selectedStatus = selected;
    }
    if(type == 'from'){
      this.selectedFromDate = selected;
    }
    if(type == 'to'){
      this.selectedToDate = selected;
    }
  }

  // toggling columns
  onValChange(selection: any, state : any){
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
      const shipmentBoxes = this.mapBoxes(obj.boxes)
      const expandedData = this.createExpandedData(obj.id, shipmentBoxes, obj.shipmentStatusLogs)
      const infoData = this.getInfoData(obj.shipmentStatusLogs);
      const latestStatus = this.getLatestStatus(infoData);
      const latestDate = this.getLatestDate(infoData);

			return {
				id: obj.id,
        cost: obj.cost,
        weight: 55,
        status: latestStatus,
        address: obj.receiverAddress,
        receiverName: obj.receiverFirstName+" "+obj.receiverLastName,
        date: latestDate,
        expandedData: expandedData
			};    
		});
	 }

   // for getting newest data to parent rows
  public getInfoData(logs: ShipmentStatusLog[]) {
    const infoArray = logs.map((obj) => {
      return {
        date: obj.date,
        statusId: obj.status.id,
        statusName: obj.status.name
      };    
    });
    
    return infoArray;
  }
  
  public createExpandedData(shipmentId : number, boxes: Box[],  logs: ShipmentStatusLog[]) {
    let expandedData = <ExpandedData>{};
    expandedData.boxes = boxes;
    expandedData.shipmentStatusLogs = logs.filter(l => l.shipmentId == shipmentId);
    
    return expandedData;
  }
  
  public getLatestDate(infoArray: any[]) {
    const latestDate = infoArray.sort((a : any, b : any) => b.date - a.date)[0]
    return new Date(latestDate.date).toDateString();
  }
  
  public getLatestStatus(infoArray: any[]) {
    const latestStatus = infoArray.sort((a : any, b : any) => b.statusId - a.statusId)[0]  
    return latestStatus.statusName;
  }
  
  public mapBoxes(boxes: Box[]) {
    return boxes.map((obj) => {
      return {
        color: obj.color.toString()
      };    
    });
  }
  
  // sorting on column click
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
