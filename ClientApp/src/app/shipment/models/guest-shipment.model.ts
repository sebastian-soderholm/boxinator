import { BasicBox, HumbleBox, DeluxeBox, PremiumBox } from "./box.model";

//Guest is anonymous => no personal info
export interface GuestShipment {
  guestEmail: string;
  receiverName: string;
  boxes: [BasicBox | HumbleBox | DeluxeBox | PremiumBox];
  destinationCountryId: number;
}