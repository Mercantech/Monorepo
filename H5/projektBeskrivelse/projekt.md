# Projektbeskrivelse

Information om Projektbeskrivelse findes på [Notion](https://mercantec.notion.site/h5-projekt-beskrivelse)

Dette H5 forløb består af følgende hovedemner:

## 1. Linux
Det første emne er Linux, hvor vi arbejder med at opsætte en Linux server. Denne server skal bruges til at hoste vores message broker og database.

## 2. Embedded enheder og IOT 
Det andet emne fokuserer på embedded enheder og IOT. Her arbejder vi primært med Arduino OPLA med et IoT kit, som nogle måske kender fra H3. Alternative muligheder inkluderer Raspberry Pi eller BeagleBone Black, dog uden direkte undervisning i disse.

## 3. RabbitMQ
RabbitMQ er vores message broker system.

## 4. Database og databehandling
Det fjerde emne omhandler håndtering af data fra vores embedded enheder. Vi bruger:
- PostgreSQL database med TimescaleDB udvidelse, som er specialdesignet til time-series data
- Python med numpy og pandas til databehandling

## 5. Dashboard
Det afsluttende emne er dashboard, hvor vi laver grafiske fremstillinger af dataene fra vores embedded enheder. Der anbefales at bruge Python til dette formål.

## Samlet system
Systemet fungerer som en helhed hvor:
1. Embedded enheder indsamler data
2. Data sendes via RabbitMQ
3. Data behandles og gemmes i PostgreSQL/TimescaleDB
4. Data visualiseres i et dashboard

Dette giver et komplet system fra dataindsamling til visualisering.
