export interface ShipmentTableData {
	id: number;
	//statusReadDTO: Status;
	shipmentStatusLogs: ShipmentStatusLog[]
	receiverAddress: string;
	receiverFirstName: string;
	receiverLastName: string;
	date: string;
	cost: string;
	boxes: Box[]
	//country: Country;

}

export interface ShipmentStatusLog{
	shipmentId: number;
	status: Status;
	date: string;
}

export interface Status {
	id: number;
	name: string;
}

export interface Country {
	id: number;
	name: string;
	countryMultiplier: number;
	zoneId: number;
}

export interface Box {
	color: string;
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

  
