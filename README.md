# boxinator

* Sebastian Söderholm
* Peppi Mäkelä
* Jani Vihervuori

## Installation
1. Clone repo using `git clone https://github.com/sebastian-soderholm/boxinator.git`
2. API folder: Add connection string to your local database into appsettings.json in following format

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




