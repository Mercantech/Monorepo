# MicroServices

I kan læse mere om MicroServices på [Notion](https://mercantec.notion.site/microservices)

## Hvad er Microservices?

Microservices er en arkitekturel tilgang til softwareudvikling, hvor en applikation opbygges som en samling af små, uafhængige tjenester. Hver tjeneste:

- Kører i sin egen proces
- Kommunikerer via veldefinerede API'er
- Har ansvar for en specifik forretningsfunktion
- Kan udvikles og deployes uafhængigt

## Fordele ved Microservices i IoT og Embedded systemer

1. **Fleksibel teknologivalg**
   - Forskellige services kan udvikles i forskellige programmeringssprog
   - Hvert team kan vælge den bedste teknologi til deres specifikke opgave
   - Nemmere integration af forskellige IoT-enheder og protokoller

2. **Skalerbarhed**
   - Services kan skaleres individuelt efter behov
   - Effektiv ressourceudnyttelse
   - Bedre håndtering af varierende belastning fra IoT-enheder

3. **Integration med Message Brokers**
   - Message brokers som RabbitMQ fungerer perfekt som kommunikationsled
   - Asynkron kommunikation mellem services
   - Pålidelig datahåndtering fra embedded enheder

4. **Fejlisolering**
   - Problemer i én service påvirker ikke andre
   - Lettere fejlfinding og debugging
   - Øget stabilitet i det samlede system

5. **Vedligeholdelse og Udvikling**
   - Teams kan arbejde uafhængigt på forskellige services
   - Hurtigere udviklingscyklus
   - Nemmere at opdatere og udskifte enkelte komponenter

6. **IoT-specifike fordele**
   - Bedre håndtering af forskellige dataformater
   - Fleksibel databehandling og -lagring
   - Effektiv integration af nye enheder og sensorer
