export interface RegisterUser {
  id: number | null;
  firstName: string;
  lastName: string;
  email: string;
  countryId: number | null;
  zipCode: string | null;
  dateOfBirth: string | null;
  phoneNumber: string | null;
}
