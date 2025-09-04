# Database og databehandling

Information om Databehandling findes på [Notion](https://mercantec.notion.site/h5-databehandling)

Vores 4. emne på H5 er Databehandling - her vil vi arbejde med at håndtere data fra vores embedded enheder og sende det videre til vores dashboard som vil lave en grafisk fremstilling af dataene.

Til database vil vi bruge PostgreSQL med en udvidelse kaldet TimescaleDB. TimescaleDB er en udvidelse til PostgreSQL som er specielt designet til time-series data, som er det jeres embedded enheder vil sende til vores database. Selve databehandlingen vil vi gøre i Python, med numpy og pandas. 