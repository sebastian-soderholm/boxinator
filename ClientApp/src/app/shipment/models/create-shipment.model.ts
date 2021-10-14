import { BasicBox, HumbleBox, DeluxeBox, PremiumBox } from "./box.model";

export interface CreateShipment {
  receiverName: string;
  cost: number;
  boxes: [BasicBox | HumbleBox | DeluxeBox | PremiumBox];
  destinationCountryId: number;
}


