Vi bruger følgende API med benzinpriser ved CirkleK - der er data fra 2015 og fremad.

Der er som standard åbent [cors](https://developer.mozilla.org/en-US/docs/Web/HTTP/CORS) adgang fra 127.0.0.1/5500 - andre kan også bruges, men er til tider lukket.

[Miles95 - Benzin Data fra CirkleK](https://opgaver.mercantec.tech/Opgaver/Miles95)

Generelt prøver vi at lave et produkt til en kunde - så tænkt hvad vi kunne bruge dataet til. Hvad kunden helt præcis efterspørger er lidt op til jer.

### Graf over priser med [[Blazor]]

Vi skal bygge en lille hjemmeside som kan vise os dataet fra API’en - Jeres underviser skal nok specificere om det er med JavaScript eller [[CSharp]] og [[Blazor]]

Der skal laves en visualisering, enten i form af en graf eller anden passende data repræsentation
![[Pasted image 20250904232439.png]]
Der finde et C# Library med Razor her: [https://demos.blazorbootstrap.com/charts/line-chart](https://demos.blazorbootstrap.com/charts/line-chart)

Når I har lavet det for Benzin, altså miles95, kan i tilføje Diesel og sammenligne dataet.

[Diesel - Diesel Data fra CirkleK](https://opgaver.mercantec.tech/Opgaver/diesel)

Er det den samme udvikling som er sket med begge produkter?

Er der problemer med vores datasæt, er det komplet?

### Derudover kan man lave følgende

Denne sektion er mest tiltænkt, hvis man bliver hurtig færdig med ens graf som er hovedopgaven

Det er vigtigt at vores funktioner skalere, så hvis datasættet bliver større følger funktionen med. Dataet er beregnet til at blive opdateret dagligt.

1. En boks som viser den seneste pris i datasættet - som om det var en live visning.
2. Der er en del data, så man kan give sig i kast med lidt simpelt dataanalyse:
    - Finde gennemsnit, median, varians (Som i har gjort før, på grundforløbet!)
    - Lave en **Lineær regressionsanalyse** på jeres data og se hvordan priserne har udviklet sig over tid. Eventuelt prøve at forudsige prisen derfra.
3. Hente ekstra data ind og se sammenhænget, fx benzin priser og fragtpriser.
4. Analyse ud fra hvilken dag på måneden eller ugen man skal tanke. Det kan være dataet viser at benzin altid er billigst om lørdagen.
5. En oversigt med 52/u high and low. Altså et overblik over den laveste pris det seneste år og den højeste.
6. En inflationsberegner, hvor man kan få nogle procent ud og se udviklingen år for år.