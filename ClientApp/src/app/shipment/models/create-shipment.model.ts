import { Box } from "./box.model";

export interface CreateShipment {
  id: number | undefined;
  firstName: string;
  lastName: string;
  countryId: number;
  zipCode: string;
  address: string;
  cost: number;
  boxes: Box[];
}
