# boxinator

* Sebastian Söderholm
* Peppi Mäkelä
* Jani Vihervuori

## Installation
1. Clone repo using `git clone https://github.com/sebastian-soderholm/boxinator.git`
2. Environmental variables (API): Add connection string to your database into appsettings.json in following format

```
{
  "AllowedHosts": "*",
  "ConnectionStrings": {
    "localSQLBoxinatorDB": ""
  }
}
```
3. Run ef core migrations by executing
`add-migration Initial` and then 
`update-database`

4. ClientApp folder: To install all required packages, run 
`npm install`

5. To run front end application in development environment execute
`ng serve`

6. Create account for firebase authentication

7. Environmental variables (ClientApp)
  - Update environments/environment.prod.ts and environment.ts files in ClientApp with your firebase settings
  - Update baseURL with your API:s address



