export interface RegisterUser {
  firstName: string;
  lastName: string;
  email: string;
  password: string;
  countryId: number | null;
  zipCode: string | null;
  dateOfBirth: string | null;
  phoneNumber: string | null;

}
