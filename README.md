# boxinator

* Sebastian Söderholm
* Peppi Mäkelä
* Jani Vihervuori

## Installation
1. Clone repo using `git clone https://github.com/sebastian-soderholm/boxinator.git`
2. Add connection string to your database into appsettings.json in following format

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

4. Continue with ClientApp setup [here](https://github.com/sebastian-soderholm/boxinator/blob/master/ClientApp/README.md)




