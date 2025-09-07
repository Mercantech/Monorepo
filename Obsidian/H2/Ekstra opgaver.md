# Ekstra Opgaver - H2

Hej alle! Her er jeres ekstra opgaver. Hver gruppe får en unik opgave, som I skal finde en løsning på. I skal både kode løsningen og fremlægge den for resten af klassen.

---

## Gæste Booking System

### Hvad er problemet?
Hotellet har et problem. Der kommer flere og flere folk ind fra gaden, der bare vil have et værelse med det samme. Lige nu er det lidt kaos, fordi receptionen ikke rigtig ved hvad de skal gøre:

- Nogle gange tvinger de kunderne til at lave en konto (det synes folk er træls)
- Andre gange laver receptionen bare kontoen for dem (det tager tid)
- Og så er der dem der bare markerer værelser som "optaget" og lejer dem ud udenom systemet (det er lidt rodet og ikke helt okay, ift brand og forsikring..)

### Hvad skal I lave?
I skal finde på en måde hvor gæster kan booke et værelse uden at skulle lave en konto først. Men det skal stadig passe ind i det system hotellet allerede har.

### Ting at tænke over
- Hvordan gemmer I gæsteinfo uden en konto?
- Hvordan sikrer I at intet går tabt?
- Hvordan gør I det nemt for både gæsten og receptionen?

---

## Udvidet Session Management

### Hvad er problemet?
Folk brokker sig over at de skal logge ind hele tiden. Det er irriterende! Konkurrenterne har login der holder i en hel måned, men jeres system kræver login hele tiden.

**Det der er træls lige nu:**
- Folk skal logge ind for ofte
- Det er irriterende for brugerne
- Konkurrenterne er bedre
- Det er svært at balancere sikkerhed og brugervenlighed

### Hvad skal I lave?
I skal finde på en måde at gøre login længere - mindst en uge, gerne en måned. Men det skal stadig være sikkert!

### Ting at tænke over
- Hvordan holder I det sikkert når login varer længere?
- Hvad gør I hvis nogen glemmer at logge ud?
- Hvordan håndterer I forskellige enheder (telefon, computer, tablet)?
- Hvordan giver I brugerne kontrol over deres sikkerhed?
- Hvordan fornyer I login automatisk?


## Stjålet konto 

### Hvad er problemet?
Nogen har fået stjålet deres konto! Det er ikke sjovt, og det sker desværre. Hotellet har brug for en måde at opdage når nogen bruger en konto der ikke er deres egen.

**Det der er træls:**
- Folk kan få stjålet deres login
- Nogen kan bruge andres konto uden at de ved det
- Hotellet ved ikke hvem der rent faktisk bruger kontoen
- Det kan skabe problemer med betalinger og bookinger

### Hvad skal I lave?
I skal finde på en måde at opdage når nogen bruger en konto der ikke er deres egen. Det skal være smart og ikke irriterende for de rigtige brugere.

### Ting at tænke over
- Hvordan kan I spotte mistænkelig adfærd?
- Hvad gør I når I tror en konto er stjålet?
- Hvordan gør I det nemt for rigtige brugere?
- Hvordan beskytter I folks privatliv?

---

## Offline view af bookinger i PWA

### Hvad er problemet?
Folk rejser til udlandet og vil gerne vise deres booking når de ankommer til hotellet. Men hvad nu hvis de ikke har internet? Eller hvis deres data er dyr i udlandet?

**Det der er træls:**
- Folk kan ikke vise deres booking uden internet
- Dyrt data i udlandet
- Hvad hvis telefonen dør?
- Folk vil gerne have deres booking info tilgængelig altid

### Hvad skal I lave?
I skal lave en PWA (Progressive Web App) der kan gemme booking informationer lokalt, så gæster kan vise deres booking selv uden internet.

### Ting at tænke over
- Hvordan gemmer I booking info lokalt?
- Hvordan synkroniserer I når der er internet igen?
- Hvad gør I hvis booking info ændres?
- Hvordan gør I det sikkert at gemme data lokalt?
- Hvordan sikre vi at gæster ikke snyder med deres data?
- Hvordan laver I en PWA der virker offline?

---

## Real-time notifikationer

### Hvad er problemet?
Hotellet vil gerne sende beskeder til gæster når der sker noget vigtigt. Måske er der problemer med deres booking, eller måske er der tilbud de skal vide om.

**Det der er træls:**
- Folk får ikke besked når der sker noget vigtigt
- Hotellet kan ikke kontakte gæster i tide
- Folk misser vigtige opdateringer
- Det er svært at koordinere med mange gæster

### Hvad skal I lave?
I skal lave et system der kan sende beskeder til gæster med det samme når der sker noget vigtigt. Det skal virke på både computer og telefon.

### Ting at tænke over
- Hvordan sender I beskeder i realtid?
- Hvordan sikrer I at folk får beskeden?
- Hvad gør I hvis nogen ikke vil have beskeder?
- Hvordan håndterer I forskellige enheder?
- Hvordan gemmer I beskeder hvis folk ikke er online?

---

## Automatisk prisjustering

### Hvad er problemet?
Hotellet vil gerne justere priserne automatisk baseret på hvad der sker. Måske skal priserne op når der er mange bookinger, eller ned når der er få.

**Det der er træls:**
- Nogen skal manuelt ændre priser hele tiden
- Priserne passer ikke altid med efterspørgslen
- Hotellet misser muligheder for at tjene mere
- Det er svært at være konkurrencedygtig

### Hvad skal I lave?
I skal lave et system der automatisk justerer priserne baseret på forskellige faktorer som antal bookinger, sæson, konkurrenter og andre ting.

### Ting at tænke over
- Hvad skal priserne baseres på?
- Hvor ofte skal priserne ændres?
- Hvordan sikrer I at priserne ikke bliver for høje?
- Hvordan fortæller I gæster om prisændringer?
- Hvordan tester I at systemet virker?

---

## Faktura og fraktura system

### Hvad er problemet?
Nogle kunder booker gennem deres firma og skal have en officiel faktura. Andre skal bare have en kvittering de kan bruge til deres regnskab. Lige nu er det lidt rodet at lave disse dokumenter.

**Det der er træls:**
- Folk skal have fakturaer til deres firma
- Nogen skal bare have en kvittering
- Det tager tid at lave disse dokumenter manuelt
- Folk glemmer at bede om det og bliver sure
- Det ser ikke professionelt ud

### Hvad skal I lave?
I skal lave et system der automatisk kan lave både fakturaer og kvitteringer når folk booker. Det skal se professionelt ud og være nemt at bruge.

### Ting at tænke over
- Hvordan laver I officielle dokumenter?
- Hvad skal der stå på en faktura?
- Hvordan gemmer I disse dokumenter?
- Hvordan sender I dem til kunderne?
- Hvordan sikrer I at det ser professionelt ud?
- Hvad gør I hvis folk vil have det på papir?


## Manglende data 

### Hvad er problemet?
Vores konkurrent har mistet ALT deres data! Det er ikke sjovt, og det kan faktisk ske for os også. Hvad hvis vi bliver hacket med ransomware? Eller hvad hvis serveren brænder? Eller hvad hvis nogen sletter alt ved et uheld?

![[Pasted image 20250907212052.png]]

**Det der er træls:**
- Alt data kan forsvinde på et sekund
- Ingen backup = ingen business
- Kunder bliver sure hvis deres data forsvinder
- Det koster kassen at komme tilbage efter et hack
- Konkurrenterne griner af os

### Hvad skal I lave?
I skal lave en nødplan der sikrer at vi aldrig mister vores data. Det skal være så godt at vi kan grine af konkurrenterne når de mister deres data.

### Ting at tænke over
- Hvor ofte skal I lave backup?
- Hvor gemmer I backup så det er sikkert?
- Hvordan tester I at backup virker?
- Hvad gør I hvis serveren brænder?
- Hvordan kommer I tilbage efter et hack?
- Hvordan sikrer I at backup ikke bliver hacket?

---

## Sensitivt brugerdata 🔒

### Hvad er problemet?
En af vores nabo hoteller har fået hacket deres database, og alt personligt data lå i plain text! Hvor dumt! Godt vi ikke har samme setup... vel? VEL?? 😅

**Det der er træls:**
- Folk kan læse alle passwords og personlige oplysninger
- Det er ikke lovligt at gemme data sådan
- Kunder bliver sure og sagsøger os
- Vores ry bliver ødelagt
- Vi får bøder for at bryde GDPR

### Hvad skal I lave?
I skal sikre at al personligt data er krypteret og sikkert. Ingen skal kunne læse det hvis de hacker os.

### Ting at tænke over
- Hvordan krypterer I passwords?
- Hvordan gemmer I personlige oplysninger sikkert?
- Hvad gør I hvis nogen alligevel hacker os?
- Hvordan sikrer I at kun rigtige brugere kan se deres data?
- Hvordan tester I at jeres sikkerhed virker?


## Hosting Lokalt 💰

### Hvad er problemet?
Deploy.Mercantec.Tech har rakt ud til os omkring deres hosting omkostninger - vores platform er blevet så populær, så de efterspørger 20.000 kr. pr måned for server omkostninger! Det synes vi er en stor omkostning og det må kunne gøres bedre on-prem.

**Det der er træls:**
- 20.000 kr. om måneden er mange penge
- Vi har allerede en server med AD
- Vi betaler for noget vi måske kan gøre selv
- Det er svært at kontrollere når det er hos andre
- Vi vil gerne have mere kontrol over vores system

### Hvad skal I lave?
I skal undersøge muligheden for at deploye lokalt på vores egen server. Host lokalt og test det - efterspørg eventuelt om hostnavnene og Cloudflare ved Deploy.mercantec.tech, det har de sagt vi kan få gratis ved dem!

### Ting at tænke over
- Hvordan deployer I på vores egen server?
- Hvordan sikrer I at det virker lige så godt?
- Hvad gør I hvis serveren går ned?
- Hvordan håndterer I trafik og brugere?
- Hvordan sikrer I at det er sikkert?
- Hvad koster det at køre lokalt vs. i skyen?
- Hvordan integrerer I med vores AD? 