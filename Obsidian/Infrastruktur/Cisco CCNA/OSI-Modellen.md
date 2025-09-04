OSI-modellen er en netværksarkitekturmodel, der beskriver, hvordan forskellige netværksprotokoller arbejder sammen for at muliggøre kommunikation mellem computere. Modellen består af syv lag, der hver har deres eget ansvar og funktion.

1. Fysisk lag: Dette lag håndterer den fysiske forbindelse mellem enhederne og transmissionen af bitstrømme. Laget er kapitel 4 i vores CCNA kursus - [[04. Physical Layer]]
2. Datalink lag: Dette lag håndterer pålidelig overførsel af data mellem direkte forbundne enheder. Laget er kapitel 6 i vores CCNA kursus - [[06. Data Link Layer]]
3. Netværkslag: Dette lag er ansvarligt for routing af data gennem forskellige netværk og styring af trafikken. Laget er kapitel 8 i vores CCNA kursus - [[08. Network Layer]] her tilhøre kapitel 9 også som er [[09. Address Resolution]] også kaldet ARP.
4. Transportlag: Dette lag sikrer pålidelig dataoverførsel mellem applikationer på forskellige enheder. Laget er kapitel 14 i vores CCNA kursus - [[14. Transport Layer]]
5. Session lag: Dette lag opretter, vedligeholder og afslutter forbindelser mellem applikationer. Laget er kapitel 15 i vores CCNA kursus - [[15. Application Layer]]. Som programmører gør vi brug af dette lag, når vi koder software, der kræver kommunikation mellem forskellige enheder, fx ved at etablere en session mellem klienten og serveren i en webapplikation. Vi kan også bruge sessionslaget til at håndtere kommunikationssessioner (som logins eller logouts) i vores applikationer.
6. Præsentationslag: Dette lag håndterer datarepræsentation og kryptering for at sikre, at applikationer kan forstå hinandens data. Laget er kapitel 15 i vores CCNA kursus - [[15. Application Layer]]. Det er tæt forbundet med sikkerhed!
7. Applikationslag: Dette lag indeholder applikationer og tjenester, som brugerne interagerer med. Laget er kapitel 15 i vores CCNA kursus -  [[15. Application Layer]]. Det er essentielt dem vi bygger og det lag som vi bruger mest tid på som programmører!

- De sidste 3 er under samme kapitel, da man i TCP modellen har samlet de 3 lag under navnet “Application Layer”

Som programmør kan du bruge OSI-modellen som et reference tabel for at forstå, hvordan forskellige netværksprotokoller fungerer på forskellige lag. Det kan hjælpe dig med at fejlfinde netværksproblemer og designe software, der effektivt kan kommunikere over netværket. For eksempel kan du bruge Transportlaget til at vælge den passende protokol baseret på dine behov, eller bruge Session laget til at etablere og styre forbindelser mellem applikationer. Vi går i dybden med den under [[CCNA for programmør]]

Her er en hjemmeside som forklare hvert enkelt lag i modellen!

[The OSI-Model in a simple way](https://osi-model.com/)

Her er en video som gennemgår et praktisk eksempel ud fra [Youtube.com](http://Youtube.com)

[how the OSI model works on YouTube (Application and Transport Layers) // FREE CCNA // EP 5](https://www.youtube.com/watch?v=oIRkXulqJA4&t=777s&pp=ygUZT3NpIHlvdXR1YmUgbmV0d29yayBjaHVjaw==)

## OSI reference eksempler

Her er et eksempel, hvis vi har en chat app, hvordan en besked går fra den ene bruger til den anden.

Her ser vi hvordan hvert lag har et ansvar både for afsenders side og modtagers!

Den er stillet meget generelt op, men giver et overblik over hvem der har ansvaret for hvad.
![[Pasted image 20250904223533.png]]
## OSI VS TCP/IP

**OSI** (Open Systems Interconnection) modellen er en konceptuel model, der beskriver, hvordan forskellige netværksprotokoller kommunikerer og samarbejder for at give netværkstjenester. Modellen er delt op i syv lag, der hver især har deres specifikke funktioner og ansvarsområder i netværkskommunikationen.

På den anden side er **TCP/IP** (Transmission Control Protocol/Internet Protocol) en mere praktisk model og en standard for at oprette og opretholde forbindelser mellem computere på internettet. Denne model er mindre kompleks end OSI-modellen og kun er opdelt i fire lag.

![[Pasted image 20250904223538.png]]
Selvom begge modeller har til formål at beskrive netværksprotokoller og hvordan de interagerer, er der nogle forskelle mellem dem. OSI-modellen er mere detaljeret og specifik, mens TCP/IP-modellen er mere generel og anvendelig i praksis. For eksempel, i OSI-modellen er session, præsentation og applikationslag adskilt, mens de i TCP/IP-modellen er samlet i et enkelt applikationslag.