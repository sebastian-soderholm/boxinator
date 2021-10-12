export interface User {
  id: number;
  firstName: string;
  lastName: string;
  email: string;
  password: string;
  dateOfBirth: Date | null;
  country: string;
  zip: string;
  contactNumber: string;
}
