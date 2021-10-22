import { Box } from "../../shared/box.model";

export interface EditShipment {
	id: number | undefined;
	receiverFirstName: string;
	receiverLastName: string;
	receiverZipCode: string;
	receiverAddress: string;
	country: Country;
	cost: number;
	boxes: Box[];
	sender: Sender;
	countries: Country[];
}

export interface Country {
	id: number | undefined;
	name: string;
}

export interface Sender {
	email: string;
}