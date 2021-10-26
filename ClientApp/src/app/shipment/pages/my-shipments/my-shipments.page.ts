import { Component, OnInit } from '@angular/core';
import { ShipmentService } from '../../services/shipment.service';
//import { SessionService } from '../../services/shipment-session.service';
import { SessionService } from 'src/app/shared/session.service';
import { ShipmentStatusLog, ShipmentTableData, MappedData, ExpandedData, Status, Box } from '../../models/shipment-table.model';
import {animate, state, style, transition, trigger} from '@angular/animations';
import {Sort} from '@angular/material/sort';
import { DatePipe } from '@angular/common';
import { LoginService } from 'src/app/login/services/login.service';
import { MatSnackBar } from '@angular/material/snack-bar';

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
  private _dataSource: MappedData[] = [];
  dateVisibility: boolean = true;
  sortedData: MappedData[] = [];
  statusOptions : Status[] = [
    {id: 1, name: "Current"},
    {id: 2, name: "Completed"},
    {id: 3, name: "Cancelled"}
  ]
  fromDate = null;
  toDate = null;
  selectedStatus? : number | null;
  selectedFromDate? : string | null;
  selectedToDate? : string | null;
  showEdit: boolean = false;
  cancelledStatus?: Status | null;
  completedStatus?: Status | null;
  canEdit: boolean = false;

  constructor(
    private readonly shipmentService: ShipmentService,
    private readonly sessionService: SessionService,
    private datePipe: DatePipe,
    private readonly loginService: LoginService,
    private _snackBar: MatSnackBar
  ) {
    this.sortedData = this._dataSource.slice();
  }

  ngOnInit() {
    this.canEdit = this.loginService.isAdmin;

    this.cancelledStatus = {
      id: 5,
      name: "CANCELLED"
    }
    this.completedStatus = {
      id: 4,
      name: "COMPLETED"
    }

    this.sessionService.removeShipmentsTableData();

    this.shipmentService.getAllCurrent(async () => {
      const mappedData = this.mapShipments(this.sessionService.shipmentTableData!);
      this.sortedData = mappedData;
    });
  }

  // for filtering data based on selected status and dates
  filterTable() {
    if(this.selectedStatus == null && this.selectedFromDate == null && this.selectedToDate == null) {
      console.log("no filters selected");
    }
    else {
      let path = "/shipments";

      if(this.selectedStatus == 1){ // Current
        path = "/shipments";
      }
      if(this.selectedStatus == 2){ // Completed
        path = "/shipments/complete";
      }
      if(this.selectedStatus == 3){ // Completed
        path = "/shipments/cancelled";
      }

      // fetch data by filters
      this.shipmentService.getFilteredShipments(path, this.selectedFromDate!, this.selectedToDate!, async () => {
        const mappedData = this.mapShipments(this.sessionService.shipmentTableData!);
        this.sortedData = mappedData;
      });

    }
  }

  addNewStatus(shipmentId: number) {
    console.log(shipmentId +" jee");
    console.log(this.sortedData)
    this.shipmentService.addNewStatusLog(shipmentId, async() =>{
      console.log("status added ");
      const mappedData = this.mapShipments(this.sessionService.shipmentTableData!);
      this.sortedData = mappedData;
    })
  }

  setSelectedStatus(selected : any) {
    this.selectedStatus = selected;
  }

  setSelectedDate(type: string, selected : any) {
    let formattedDate = this.datePipe.transform(selected.value, 'dd/MM/yyyy');

    if(type == 'from'){
      this.selectedFromDate = formattedDate!;
    }
    if(type == 'to'){
      this.selectedToDate = formattedDate!;
    }
  }

  deleteShipment(shipmentId: number) {
    this.shipmentService.deleteShipment(shipmentId).subscribe(response => {
      this._snackBar.open('Shipment deleted!', 'OK', {
        duration: 2000
      });
      //remove shipment from frontend
      this.sortedData = this.sortedData.filter((data: MappedData) => {
        return data.id !== shipmentId
      })
      // this.sessionService.setShipmentsTableData(this.sortedData);
    },
    (error) => {
      this._snackBar.open('Shipment could not be deleted, please try again.', 'OK', {
        duration: 2000
      });
    }
    )
  }
  // toggling columns
  onValChange(selection: any, state : any){

    if(state == true) {
      this.removeColumn(selection);
    }
    else {
      this.addColumn(selection);
    }

    // keeping the toggle status column and edit column last
    if(this.showEdit === true) {
    this.removeColumn('toggle');
    this.addColumn('toggle');
    this.removeColumn('edit');
    this.addColumn('edit');
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

  // mapping incoming data
  mapShipments(shipments: ShipmentTableData[]) {

		return shipments.map((obj) => {
      const boxesInShipment = this.mapBoxes(obj.boxes)
      const expandedData = this.createExpandedData(obj.id, boxesInShipment, obj.shipmentStatusLogs)
      const infoData = this.getInfoData(obj.shipmentStatusLogs);
      const latestStatus = this.getLatestStatus(infoData);
      const latestDate = this.getLatestDate(infoData);
      const combinedWeight = this.getCombinedWeight(boxesInShipment);

			return {
				id: obj.id,
        cost: obj.cost,
        weight: combinedWeight,
        status: latestStatus,
        address: obj.receiverAddress,
        receiverName: obj.receiverFirstName+" "+obj.receiverLastName,
        date: latestDate,
        expandedData: expandedData,
			};
		});
  }

  mapBoxes(boxes: Box[]) {
    return boxes.map((obj) => {
      return {
        name: obj.name.toString(),
        weight: obj.weight,
        color: obj.color.toString()
      };
    });
  }

  // newest data to parent rows
  getInfoData(logs: ShipmentStatusLog[]) {
    const infoArray = logs.map((obj) => {
      return {
        date: obj.date,
        statusId: obj.status.id,
        statusName: obj.status.name
      };
    });

    return infoArray;
  }

  // data for expandable row
  createExpandedData(shipmentId : number, boxes: Box[],  logs: ShipmentStatusLog[]) {
    let expandedData = <ExpandedData>{};
    expandedData.boxes = boxes;
    expandedData.shipmentStatusLogs = logs.filter(l => l.shipmentId == shipmentId);

    return expandedData;
  }

  getLatestDate(infoArray: any[]) {
    const latestDate = infoArray.sort((a : any, b : any) => b.date - a.date)[0]
    return new Date(latestDate.date).toDateString();
  }

  getLatestStatus(infoArray: any[]) {
    const latestStatus = infoArray.sort((a : any, b : any) => b.statusId - a.statusId)[0]
    return latestStatus.statusName;
  }

  getCombinedWeight(boxes: Box[]) {
    let sum = 0;

    boxes.forEach(box => {
      for (const property in box) {
        if(property === "weight"){
          sum += box[property];
        }
      }
    })
    return sum;
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
