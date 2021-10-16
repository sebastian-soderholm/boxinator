import { Country } from "src/app/login/models/country.model";
import { Box } from "./box.model";

//Guest is anonymous => no personal info, only email
export interface GuestShipment {
  senderEmail: string;
  receiverFirstName: string;
  receiverLastName: string;
  destinationCountryId: number;
  destinationZipCode: string;
  cost: number;
  boxes: Box[];
}

