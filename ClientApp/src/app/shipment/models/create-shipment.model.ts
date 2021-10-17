import { Box } from "./box.model";

export interface CreateShipment {
  senderId: number | undefined;
  receiverFirstName: string;
  receiverLastName: string;
  receiverZipCode: string;
  receiverAddress: string;
  countryId: number;
  cost: number;
  boxes: Box[];
}
