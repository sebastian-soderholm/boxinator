export interface User {
  id: number;
  firstName: string;
  lastName: string;
  email: string;
  password: string;
  dateOfBirth: Date | null;
  country: string | null;
  zip: string | null;
  contactNumber: string | null;
}
