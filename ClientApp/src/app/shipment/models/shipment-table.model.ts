export interface ShipmentTableData {
	id: number;
	statusReadDTO: Status;
	shipmentReadDTO: Shipment;
	date: string;
}

export interface Status {
	id: number;
	name: string;
}

export interface Country {
	name: string;
}

export interface Shipment {
	id: number;
	receiverName: string;
	cost: string;
	country: Country;
	//boxes: [] // needs interface
	//statusLogs: [] // needs interface

}

export interface MappedData {
	id: number;
	cost: string;
	weight: number;
	status: string;
	receiverName: string;
	date: string;
}

