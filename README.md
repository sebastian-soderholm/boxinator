# boxinator

* Sebastian Söderholm
* Peppi Mäkelä
* Jani Vihervuori

To start clone repo using `git clone https://github.com/sebastian-soderholm/boxinator.git`

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




