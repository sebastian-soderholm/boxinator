<br />
<div align="center">
  <img src="https://github.com/sebastian-soderholm/boxinator/blob/master/ClientApp/src/assets/images/boxinator-cat-logo.jpg" alt="Logo" width="80" height="80">

  <h3 align="center">Boxinator</h3>

  <p align="center">
    A mystery box web store / postal service
    <br />
    <a href="https://sebastian-soderholm.github.io/boxinator/#/login"><strong>View app</strong></a>
    ·
    <a href="https://github.com/sebastian-soderholm/boxinator/blob/master/Documentation/swagger.json">Swagger</a>
    ·
    <a href="https://github.com/sebastian-soderholm/boxinator/blob/master/Documentation/Boxinator-Prototype.pdf">Prototype</a>
    ·
    <a href="https://github.com/sebastian-soderholm/boxinator/blob/master/Documentation/boxinator-database-diagram.PNG">Database diagram</a>
  </p>
</div>

<!-- TABLE OF CONTENTS -->
<details>
  <summary>Table of Contents</summary>
  <ol>
    <li>
      <a href="#group">Group</a>
    </li>      
    <li>
      <a href="#overview">Overview</a>
      <ul>
        <li><a href="#built-with">Built With</a></li>
        <li><a href="#functionalities">Functionalities</a></li>
      </ul>
    </li>
    <li>
      <a href="#getting-started">Getting Started</a>
      <ul>
        <li><a href="#api">Api instructions</a></li>
        <li><a href="#client">Client instructions</a></li>
      </ul>
    </li>
  </ol>
</details>

## Group
* Sebastian Söderholm
* Peppi Mäkelä
* Jani Vihervuori

## Overview
This project consists of front end Angular application with Firebase authentication, .NET API and Entity Framework with database migrations. The application allows users to order mystery boxes to be shipped around the world.

## Built with
* [Angular](https://angular.io/)
* [Angular Material](https://material.angular.io/)
* [.Net](https://docs.microsoft.com/en-us/dotnet/)
* [Firebase](https://firebase.google.com/)

## Functionalities
### Pages:  
#### Dashboard  
  - View current shipments  
#### New shipment  
  - Select multiple boxes for shipping from multiple tiers with user defined colours  
#### My shipments  
  - View all shipments, order and filter by status and date range    
  - Shipment management (admin)  
#### Account  
  - Edit own account data 
#### Settings (admin)  
  - Edit users' accounts  
  - Edit countries and zones  

### Login using Google account as one of the following:
#### Guest
- Able to create shipment without registering
- Claim shipment by registering
#### Registered user
  - Dashboard: View current shipments
  - New shipment: Create new shipments
  - My shipments: View all shipments, order and filter by status and date range
  - Account: Edit own account information
#### Admin
  - Dashboard: View all users' current shipments
  - New shipment: Create new shipments
  - My shipments:
    - View all users' shipments, order and filter by status and date range
    - Set next status
    - Edit, delete and cancel shipments
  - Account: 
    - Edit all users' account information
    - Edit countries and zones  

## Getting started
To start, clone repo using `git clone https://github.com/sebastian-soderholm/boxinator.git`

### Installation instructions for [API](https://github.com/sebastian-soderholm/boxinator/tree/master/API)

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


### Installation instructions for [ClientApp](https://github.com/sebastian-soderholm/boxinator/tree/master/ClientApp)

1. To install all required packages, run 
`npm install`

2. To run this front end application in development environment execute
`ng serve` and navigate to `http://localhost:4200/`

3. Create firebase account for authentication

4. Environmental variables (environments/environment.ts)
    - Update your firebase settings
    - Update baseURL with your API's address

```
export const environment = {
  production: false,
  firebaseConfig : {
    apiKey: "",
    authDomain: "",
    projectId: "",
    storageBucket: "",
    messagingSenderId: "",
    appId: "",
    measurementId: ""
  },
  baseURL: "",
};
```


