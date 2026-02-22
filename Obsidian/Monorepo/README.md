# Monorepo – oversigt over aktive repos og projekter

Denne mappe indeholder noter og guides til Monorepo. Herunder en oversigt over **aktive repos/projekter** i monorepo-strukturen, så du nemt kan finde dem til undervisning, forking til elever eller bare holde styr på det store GitHub-repo.

*Sidst opdateret: februar 2025*

---

## Struktur på topniveau

| Mappe | Beskrivelse |
|-------|-------------|
| **Projekter** | Standalone projekter (ofte med eget GitHub-repo) |
| **Hovedforløb** | H1/H2/H4 templates + MAGS-elever (Blazor, API, Aspire) |
| **DOD** | DevOps-dag: backend, frontend, Proxi, CICD, DNS, Nginx, Sikkerhed m.m. |
| **GF2** | Grundlæggende programmering 2: C#, WPF, MSTest, opgaver, konsol/Blazor |
| **GF2-MAGS** | GF2 for MAGS: WPF, UnitTest, Blazor, Konsol, Teori, Opgaver |
| **Learning-Languages** | Sprog/tekst (fx Learning-Rust) |
| **MachineLearning** | ML-forløb: Dataanalyse, Regression, Classification, Cluster, Fit, TimeSeries, NeuralNetwork, Reinforcement |
| **Obsidian** | Obsidian-vault/noter (denne mappe) |

---

## Projekter (standalone – gode kandidater til forking)

Disse ligger under `Monorepo/Projekter/` og er typisk projekter du også har som separate GitHub-repos.

| Repo/projekt | Teknologi / formål |
|--------------|--------------------|
| **ActiveDirectoryTesting** | .NET (C#) |
| **Aspire-Exampels** | .NET Aspire (AppHost, API, Web, ServiceDefaults, Tests) |
| **CTF** | Capture The Flag / sikkerhed |
| **eval** | Blazor + Aspire (eval.AppHost, Blazor, ServiceDefaults) |
| **videnstjek** | Blazor (.NET 9) |

*Tip: Brug [New Git repo.md](New%20Git%20repo.md) til at tilføje nye repos til monorepo eller til at genoprette forbindelse til det oprindelige repo.*

---

## Hovedforløb

- **Templates**  
  - **H1** – fx Banko (Blazor + BankoCheater), kan have egen `.git`  
  - **H2** – Blazor/API/DomainModels/AppHost/ServiceDefaults-template  
  - **H4** – Backend-template (API, AppHost, ServiceDefaults, DomainModels)
- **H2-MAGS** – H2-projekt med Blazor, API, admin-dashboard (Svelte), AppHost, DomainModels
- **H4-MAGS** – H4 Backend (API, AppHost, ServiceDefaults, Tests)

---

## DOD (DevOps-dag)

| Underprojekt | Beskrivelse |
|--------------|-------------|
| backend, frontend | App-kode |
| Proxi | Proxy (har `package.json` i app) |
| CICD, docker, infra | Pipeline og infrastruktur |
| DNS-og-Firewall, Nginx-og-HTTPS, Sikkerhed | Netværk og sikkerhed |
| Noter | Dokumentation |

---

## GF2 og GF2-MAGS

- **GF2**: CSharp-Bogen, MSTest-Opgaver, Opgaver, WPF, Projekter (Konsol, Blazor), Notes
- **GF2-MAGS**: WPF, UnitTest, Teori, Opgaver, Projekter (Konsol, Blazor)

---

## Learning-Languages

- **Learning-Rust**

---

## MachineLearning

Jupyter/Notion-forløb med mapper:  
`0.Dataanalyse`, `1.Regression`, `2.Classification`, `3.Cluster`, `4.Fit`, `5.TimeSeries`, `6.NeuralNetwork`, `7.Reinforcement`, `MLWithPython`.

Se `MachineLearning/README.md` i monorepo-roden for pensum og opgaver.

---

## Hurtig reference: hvor er hvad?

- **Undervisning / fork til elever** → `Projekter/`, `Hovedforløb/` (Templates + MAGS), `GF2/`, `GF2-MAGS/`
- **DevOps/Infra** → `DOD/`
- **Machine Learning** → `MachineLearning/`
- **Tilføj nyt repo til monorepo** → [New Git repo.md](New%20Git%20repo.md)
