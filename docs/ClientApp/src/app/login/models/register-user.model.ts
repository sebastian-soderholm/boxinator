export interface RegisterUser {
  id: number | null;
  firstName: string;
  lastName: string;
  email: string;
  countryId: number | null;
  zipCode: string | null;
  address: string;
  dateOfBirth: string | null;
  phoneNumber: string | null;
}
