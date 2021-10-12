import { BasicBox, HumbleBox, DeluxeBox, PremiumBox } from "./box.model";

export interface CreateShipment {
  receiverName: string;
  boxes: [BasicBox | HumbleBox | DeluxeBox | PremiumBox];
  destinationCountryId: number;
}


