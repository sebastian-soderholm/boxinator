import { Box } from "../../shared/box.model";

//Guest is anonymous => no personal info, only email
export interface GuestShipment {
  email: string;
  receiverFirstName: string;
  receiverLastName: string;
  receiverZipCode: string;
  receiverAddress: string;
  cost: number;
  countryId: number;
  boxes: Box[];
}

