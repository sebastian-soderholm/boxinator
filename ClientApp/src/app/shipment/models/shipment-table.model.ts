export interface ShipmentTableData {
	id: number;
	shipmentStatusLogs: ShipmentStatusLog[]
	receiverAddress: string;
	receiverFirstName: string;
	receiverLastName: string;
	date: string;
	cost: string;
	boxes: Box[];
	expandedData: ExpandedData []
	country: Country;

}

export interface ExpandedData {
	boxes: Box [];
	shipmentStatusLogs: ShipmentStatusLog[];
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
	weight: number;
	name: string;
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

  
