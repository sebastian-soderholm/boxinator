export interface Box {
  name: string;
  weight: number;
  color: string;
}

export const BoxTypes = [
  {
    name: "Basic",
    weight: 1,
  },
  {
    name: "Humble",
    weight: 2,
  },
  {
    name: "Deluxe",
    weight: 5,
  },
  {
    name: "Premium",
    weight: 8,
  }
];
