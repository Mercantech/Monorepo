# Hovedforløb – Templates oversigt

Denne mappe indeholder **starterkode og skabeloner** til H1, H2, H3, H4 og H5. Her er en kort forklaring af **strukturen for hver template**, så du nemt kan finde rundt og vælge den rigtige til undervisning eller projekter.

---

## H1 – Blazor, domænemodeller, dokumentation og SQL

**Formål:** Første hovedforløbsprojekt med Blazor (ofte Blazor Server), database og dokumentation.

**Struktur i denne monorepo:**

| Mappe / indhold | Beskrivelse |
|-----------------|-------------|
| **Banko/** | Eksempelprojekt: Banko med Blazor + konsol-app (BankoCheater). Indeholder `Blazor/` (Blazor Web-app), `BankoCheater/` (konsol), `docker-compose.yml`, `appsettings.example.json`. |
| **Dokumentation/** | Tom/udfyldes af elever: UML, database-diagrammer (fx DrawSQL), Mermaid-filer. |
| **README.md** | Beskriver den klassiske H1-struktur: BlazorApp, Domain Models, Dokumentation, SQL-Scripts. |

**Den klassiske H1-struktur** (som beskrevet i H1’s egen README) er opdelt i fire dele: **BlazorApp** (UI + Blazor Server), **Domain Models** (klasser til app og DB), **Dokumentation** (UML, DB-diagram, SQL), **SQL-Scripts** (.sql-filer til queries). I dette repo er Banko et konkret eksempel med Blazor + BankoCheater.

**Teknologi:** .NET (Blazor), Supabase/Postgres, evt. Docker.  
**Notion:** [Projekt H1 – Webshop](https://www.notion.so/mercantec/Projekt-H1-Webshop-3eafa5e658f44a21a7edea55d419c3e8)

---

## H2 – Blazor WebAssembly, API, Aspire

**Formål:** H2-projekt med Blazor WebAssembly, ASP.NET Core API og .NET Aspire som hosting/orchestration.

**Struktur:**

| Mappe | Beskrivelse |
|-------|-------------|
| **Blazor/** | Blazor WebAssembly-frontend. Taler med API via APIService. |
| **API/** | ASP.NET Core Web API – database-adgang og data til Blazor. |
| **DomainModels/** | Class library med fælles domænemodeller til Blazor og API. |
| **H2-Projekt.AppHost/** | Aspire AppHost – samler API og Blazor, .NET 9. |
| **H2-Projekt.ServiceDefaults/** | Aspire ServiceDefaults (logging, health checks m.m.). |
| **Bruno/** | API-testfiler (Bruno). |
| **.github/ISSUE_TEMPLATE/** | Issue-skabeloner (aflevering, eksamen, ekstra opgaver). |

**Solution:** `H2-Projekt.sln` (DomainModels, API, Blazor, AppHost, ServiceDefaults).

**Teknologi:** .NET 9, Blazor WebAssembly, ASP.NET Core API, Aspire.  
**Notion:** [H2 Projekt forløb](https://mercantec.notion.site/h2f)

---

## H3 – Arduino / embedded (PlatformIO)

**Formål:** Template til embedded/IoT med Arduino (MKR WiFi 1010 + MKR IoT Carrier).

**Struktur:**

| Mappe | Beskrivelse |
|-------|-------------|
| **Arduino/** | PlatformIO-projekt. |
| **Arduino/src/** | Kildekode: `main.cpp`, `display_utils.cpp`, `led_utils.cpp`, `temp_color.cpp`, `secrets.h` (+ `secrets.h.example`). |
| **Arduino/include/** | Headers: `config.h`, `display_utils.h`, `led_utils.h`, `temp_color.h`. |
| **Arduino/lib/** | Valgfri projektspecifikke biblioteker (tom – dependencies via `platformio.ini`). |
| **Arduino/test/** | Unit tests (fx `test_temp_color.cpp`). |
| **H3-Noter/** | Obsidian/noter (fx Welcome.md). |

**Vigtigt:** `secrets.h` committes ikke; elever kopierer `secrets.h.example` til `secrets.h` og udfylder WiFi osv.

**Teknologi:** C++, PlatformIO, Arduino (MKR WiFi 1010, MKR IoT Carrier).

---

## H4 – Flutter, C# Backend, Docker

**Formål:** Mobil/krydsplatform med Flutter (eller React Native) og C# backend, med mulighed for Docker-setup.

**Struktur:**

| Mappe | Beskrivelse |
|-------|-------------|
| **Backend/** | C#-backend (Aspire-stil). Solution: API, Backend.AppHost, Backend.ServiceDefaults, DomainModels. |
| **flutter_app/** | Flutter-app (MVVM). `lib/` med `core/`, `data/`, `features/` (fx weather, infographic), `routing/`, `shared/`. |
| **Docker/** | Nginx reverse proxy + compose: `/flutter/` → Flutter Web, `/api/` → Backend API, `/react/` → React Native Web. |

**Backend:** API, Backend.AppHost, Backend.ServiceDefaults, DomainModels (.NET).  
**Flutter:** Følger MVVM under `lib/` (core, data/services, features med model/view/view_model/widgets).

**Teknologi:** Flutter, .NET (Aspire), Docker, Nginx.  
**Notion:** H4-forløb (se monorepo root README for links).

---

## H5 – Linux, embedded, message broker, dashboard

**Formål:** Template til H5 med materialer og noter – ikke ét enkelt kode-projekt, men mapper til pensum og projekter.

**Struktur (som beskrevet i H5’s README):**

| Mappe | Indhold |
|-------|---------|
| **projektBeskrivelse/** | Projektbeskrivelse og opgave. |
| **linux/** | Materialer om Linux. |
| **embedded/** | Embedded enheder og IoT. |
| **MessageBroker/** | Message brokers, RabbitMQ. |
| **databehandling/** | Database og databehandling. |
| **dashboard/** | Dashboard-materialer. |
| **ekstraEmner/** | Ekstra emner uden for kerne-pensum. |
| **H5-Noter/** | Obsidian-vault (fx Welcome.md). |

**Teknologi:** Blandet – Linux, embedded, RabbitMQ, databehandling, dashboard.  
**Notion:** [H5](https://mercantec.notion.site/h5?pvs=4), [Projektbeskrivelse](https://mercantec.notion.site/h5-projekt-beskrivelse), Linux, Embeded, Message Broker, Databehandling, Dashboard (links i H5/README.md).

---

## Hurtig oversigt

| Template | Kerne-teknologi | Typisk struktur |
|----------|----------------|------------------|
| **H1** | Blazor, DB, SQL, dokumentation | Banko (Blazor + konsol), Dokumentation |
| **H2** | Blazor WASM, API, Aspire | Blazor, API, DomainModels, AppHost, ServiceDefaults |
| **H3** | Arduino / PlatformIO | Arduino (src, include, lib, test), H3-Noter |
| **H4** | Flutter + C# backend | Backend (API, AppHost, ServiceDefaults, DomainModels), flutter_app (MVVM), Docker |
| **H5** | Materialer/noter | projektBeskrivelse, linux, embedded, MessageBroker, databehandling, dashboard, ekstraEmner, H5-Noter |

For mere detaljer og konfiguration (fx `appsettings.json`, Docker, Notion) se den enkelte templates **README.md** i hver undermappe (H1, H2, H3, H4, H5).
