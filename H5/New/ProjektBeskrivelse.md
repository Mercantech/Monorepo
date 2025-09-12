

# Mandag 17/03 - Projekt Beskrivelse og Linux 🐧

## Kravspecifikation og Case beskrivelse

I skal i løbet af første dag (med mulighed for at gøre den færdig i løbet af 2. dagen) lave en case beskrivelse og en kravspecifikation for jeres selvvalgte projekt. Den skal ikke godkendes, men jeres underviser, kan løbende komme med feedback og vil læse den igennem, når den er skrevet færdig!

Jeg har et par overordnet krav til jeres projekt, hvor I har relativ stor frihed for at vælger teknologier og værktøjer under dem!

### Projektkrav

#### 1. Linux Miljø
Dele af jeres projekt skal køre i et Linux miljø! Vi giver adgang til en virtuel maskine, med nok hardware til jeres projekt.

#### 2. IoT/Embedded Enhed
Projektet skal inkludere en IoT/embedded enhed. Vi har Arduino enheder til rådighed, men også Beaglebone black eller Raspberry Pi. Det er op til jer at beslutte hvilken enhed som passer jeres case bedst! Der bliver som standard kun undervist i en af dem, hvilket er Arduino Opla som også er vores klare anbefaling til projektet.

#### 3. Dataopsamling og Time Series
Projektet skal omhandle en vinkel på at opsamle større mængder data, her anbefales det at tænke i Time Series data. I skal tænke jeres teknologier igennem for hvordan I vil håndtere større mængder data.

#### 4. Kommunikation udover HTTP
Projektet skal kunne kommunikere over andet end HTTP, her er det specielt fra jeres embedded enhed at vi har fokus. Protokoller og værktøjer som er anbefalet er MQTT og RabbitMQ som bruger AMQP.

#### 5. Hosting
Jeres projekt skal være hostet, vi anbefaler at gøre det på vores lokale datacenter, som I har til rådighed, men er åben for at I bruger cloud alternativer såsom Hetzner eller en lokal maskine I har. Vi har mulighed for at lave en cloudflare tunnel, for at få nogle af jeres lokale tjenester åbnet op til omverdenen over HTTP. Dette gælder altså ikke jeres Message broker.

#### 6. Oppe-tid og Pålidelighed
Projektet skal have fokus på oppe-tid og pålidelighed. Her kan systemovervågning, logning og struktureret fejlhåndtering være emner som I fokusere på. Derover gælder det også ved code-release, så tænk også over CI/CD opsætning.

## Arbejdsgang

Vi starter med at I for adgang til et Linux miljø, herfra har I resten af dagen til at beskrive jeres case (den skal ikke godkendes, men spørgsmål er altid velkomne). Hvis man har skrevet ens case ned, gerne med et case-beskrivelse og kravspecifikation format! Starter man derfra, hvor man føler det giver mening!

