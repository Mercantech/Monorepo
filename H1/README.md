# H1-Projekt {Indsæt gruppenavn}
Her skal alt omkring jeres projekt være. Alle jeres OOP klasser, Diagrammer (UML, Database, mm.), Blazorkode og SQL scripts. 
Projektet han findes her - [Notion](https://www.notion.so/mercantec/Projekt-H1-Webshop-3eafa5e658f44a21a7edea55d419c3e8)

Det er delt op i 4 mapper 

## [BlazorApp](https://github.com/MAGS-Template/H1-Projekt/tree/master/BlazorApp)
Her er størstedelen af jeres projekt, her har vi alt UI. Det er også vores Blazor Server som skal håndtere vores forbindelse til databasen!

## [Domain Models](https://github.com/MAGS-Template/H1-Projekt/tree/master/Domain%20Models)
Her er alle jeres klasser, som skal bruges inde i jeres BlazorApp. 

## [Dokumentation](https://github.com/MAGS-Template/H1-Projekt/tree/master/Dokumentation)
Mappen her er stortset tom, fordi I selv skal udfylde den med jeres dokumentation fra jeres projekt! Der skal være jeres UML diagram, enten bare det nyeste eller alle versioner. 
Jeres Database diagram som i har lavet med [DrawSQL.app](drawsql.app)

## [SQL-Scripts](https://github.com/MAGS-Template/H1-Projekt/tree/master/SQL-Scripts)
Vi skal skrive scripts som kan queries mod vores database som enten er hostet lokalt eller på en cloudplatform! Det er vigtigt at gemme dem, så vi bruger mappen her og gemmer dem som .SQL filer. De kan eksekveres med mange GUI's - personligt anbefaler jeg [SQLTools](https://www.notion.so/mercantec/VSCode-Extensions-f4e03a6568ee483f85d9fc018ba6baa7?pvs=4#e439f568d1fe4749afa04ee204f37ac9) som er en udvidelse til VSCode. [TablePlus](https://tableplus.com/) og [HeidiSQL](https://www.heidisql.com/) er også gode bud!

## Projekt Opsætning

### appsettings.json Konfiguration
For at køre projektet lokalt skal du oprette en `appsettings.json` fil i Blazor-mappen. Denne fil er ikke inkluderet i repository'et af sikkerhedsmæssige årsager.

Jeres Database Connection kommer fra Supabase - Hvor I for udleveret en konto af jeres underviser.

#### Placering
/Blazor/appsettings.json

```json
{
  "Logging": {
    "LogLevel": {
      "Default": "Information",
      "Microsoft.AspNetCore": "Warning"
    }
  },
  "ConnectionStrings": {
    "DefaultConnection": "User Id=******;Password=***;Server=***;Port=5432;Database=postgres"
  },

  "JWT": {
    "SecretKey": "JegErEnH1Elev-HerErMinSuperSikreSecretKey",
    "Issuer": "DinPlatform",
    "Audience": "DinPlatformUsers",
    "ExpirationDays": "7"
  },

  "AllowedHosts": "*"
}
```

#### Konfigurationstrin
1. Opret en ny fil med navnet `appsettings.json`
2. Kopier skabelonen ovenfor
3. Erstat følgende værdier:
   - `Din_Database_Connection_String`: Din faktiske database forbindelsesstreng
   - `Din_Hemmelige_Nøgle_Minimum_32_Tegn`: En sikker nøgle til JWT token generering (minimum 32 tegn)
4. Gem filen

> **VIGTIGT**: Denne fil indeholder følsomme oplysninger og må aldrig committes til Git. Den er allerede tilføjet til `.gitignore`.



### Hosting
Vi kigger på Hosting under H1 forløbet, men vores applikation her burde gerne være live på Deploy.mercantec.tech