# Boxinator
A mystery box web store / postal service

### Overview
This project consists of front end Angular application with Firebase authentication, .NET API and Entity Framework with database migrations. The application allows users to order mystery boxes to be shipped around the world.

## Functionalities
- Three types of accounts (guest/registered user/admin)
  - Guest access:
    - Able to create shipment without registering
    - Claim shipment by registering
  - Registered user access:
    - Dashboard: View current shipments
    - My shipments: View all shipments, order and filter by status and date range
    - Account: Edit their own account information
  - Admin access:
    - Dashboard: View all users' current shipments
    - My shipments:
        - View all users' shipments, order and filter by status and date range
        - Set next status
        - Edit, delete and cancel shipments
    - Account: 
        - Edit all users' account information
        - Edit countries and zones


### Group
* Sebastian Söderholm
* Peppi Mäkelä
* Jani Vihervuori

To start, clone repo using `git clone https://github.com/sebastian-soderholm/boxinator.git`

## Installation instructions for [API](https://github.com/sebastian-soderholm/boxinator/tree/master/API)

1. Add connection string to your database into appsettings.json in following format

```
{
  "AllowedHosts": "*",
  "ConnectionStrings": {
    "localSQLBoxinatorDB": ""
  }
}
```
2. Run ef core migrations by executing
`add-migration Initial` and then 
`update-database`

3. Continue with ClientApp setup [here](https://github.com/sebastian-soderholm/boxinator/blob/master/ClientApp/README.md)




