CORS (Cross Origin Resource Sharing) er en vigtig sikkerhedsmekanisme i webudvikling. Lad mig forklare hvorfor det er vigtigt for jeres hotelbooking projekt:

Hvis I implementerer en løs CORS-politik i jeres hotel-API, kan det medføre flere sikkerhedsrisici:

- Uautoriseret adgang: Ondsindede websites kunne potentielt få adgang til jeres kunders personlige data og bookinger.

- Misbrug af brugerdata: Andre websites kunne potentielt udføre handlinger på vegne af jeres brugere, såsom at:

- Lave uautoriserede bookinger

- Få adgang til personlige oplysninger

- Ændre eller slette eksisterende reservationer

Dette er særligt kritisk for et hotelbookingsystem, hvor I håndterer:

- Brugerlogin og authentication

- Personlige brugerdata

- Bookingoplysninger

For at beskytte jeres system bør I implementere en strikt CORS-politik, der kun tillader adgang fra jeres godkendte frontend-domæner. Dette er særligt vigtigt når I arbejder med funktioner som brugerlogin og bookinghandlinger.

#### Hvorfor har vi CORS?

The cross-domain vulnerability

> Let’s say you browse to a malicious website https://evilunicorns.com while logged into https://examplebank.com. Without same-origin policy, that hacker website could make authenticated malicious AJAX calls to https://examplebank.com/api to 
> 
> POST /withdraw
> 
>  even though the hacker website doesn’t have direct access to the bank’s cookies. [Source](https://www.moesif.com/blog/technical/cors/Authoritative-Guide-to-CORS-Cross-Origin-Resource-Sharing-for-REST-APIs/)

Her ser vi at uden CORS, ville en side kunne få adgang til blandt andet cookies som indeholder vores personlige data og i nogle eksempler få adgang til agere på vores vegne.

Her er det blandt andet vigtigt at [examplebank.com](http://examplebank.com/) har CORS sikkerhed, så den ikke tillader at lave kommandoer fra vores computer og på vores vegne.

Videon her forklare CORS i simple termer med gode eksempler

![[Pasted image 20250904222553.png]]

Her ser vi på vores postnumre-api, at de har en * i deres control-allow-origin header - det betyder at den er åben for alle hosts. Standarten er at den kun ville acceptere kald fra adressen “data.forsningen.dk”