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

export interface Box {
	color: string;
}

export interface Shipment {
	id: number;
	address: string;
	firstName: string;
	lastName: string;
	cost: string;
	country: Country;
	boxes: Box[]

}

export interface MappedData {
	id: number;
	cost: string;
	weight: number;
	status: string;
	address: string;
	receiverName: string;
	date: string;
}

  
