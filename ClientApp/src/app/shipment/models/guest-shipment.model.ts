import { Country } from "src/app/login/models/country.model";
import { Box } from "./box.model";

//Guest is anonymous => no personal info, only email
export interface GuestShipment {
  email: string;
  firstName: string;
  lastName: string;
  countryId: number;
  zipCode: string;
  address: string;
  cost: number;
  boxes: Box[];
}

