import { Country } from "src/app/login/models/country.model";
import { Zone } from "./zone.model";

export interface CountryAndZones {
  countries: Country,
  zones: Zone[]
}
