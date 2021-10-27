# Boxinator
A mystery box web store / postal service

## Overview
This project consists of front end Angular application with Firebase authentication, .NET API and Entity Framework with database migrations. The application allows users to order mystery boxes to be shipped around the world.

## Functionalities
###Pages:  
Dashboard  
  - View current shipments  
New shipment  
  - Select multiple boxes for shipping from multiple tiers with user defined colours  
  - Shipment status management (admin)  
My shipments  
  - View all shipments, order and filter by status and date range  
Account  
  - Edit own account data 
Settings (admin)  
  - Edit users' accounts  
  - Edit countries and zones  

Login using Google account as one of the following
  - Guest
    - Able to create shipment without registering
    - Claim shipment by registering
  - Registered user
    - Dashboard: View current shipments
    - New shipment: Create new shipments
    - My shipments: View all shipments, order and filter by status and date range
    - Account: Edit their own account information
  - Admin
    - Dashboard: View all users' current shipments
    - New shipment: Create new shipments
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




