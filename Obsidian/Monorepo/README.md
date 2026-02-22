# Monorepo – oversigt over aktive repos og projekter

Denne mappe indeholder noter og guides til Monorepo. Herunder en oversigt over **aktive repos/projekter** i monorepo-strukturen, så du nemt kan finde dem til undervisning, forking til elever eller bare holde styr på det store GitHub-repo.

*Sidst opdateret: februar 2025*

---

## Alle GitHub-repos (hurtigliste)

| Repo | URL |
|------|-----|
| GF1 | [mercantech/gf1](https://github.com/mercantech/gf1) |
| GF2 | [mercantech/gf2](https://github.com/mercantech/gf2) |
| H1 | [mercantech/h1](https://github.com/mercantech/h1) |
| H2 | [mercantech/h2](https://github.com/mercantech/h2) |
| H3 | [mercantech/h3](https://github.com/mercantech/h3) |
| H4 | [mercantech/h4](https://github.com/mercantech/h4) |
| H5 | [mercantech/h5](https://github.com/mercantech/h5) |
| H6 | [mercantech/h6](https://github.com/mercantech/h6) |
| H1 (MAGS template) | [MAGS-Template/H1-Projekt](https://github.com/MAGS-Template/H1-Projekt) |
| Machine Learning | [Mercantech/MachineLearning](https://github.com/Mercantech/MachineLearning) |
| Proxi (DOD) | [mercantech/Proxi](https://github.com/mercantech/Proxi) |
| eval | [Mercantech/eval](https://github.com/Mercantech/eval) |
| videnstjek | [Mercantech/videnstjek](https://github.com/Mercantech/videnstjek) |

*Projekter uden link (ActiveDirectoryTesting, Aspire-Exampels, CTF, Learning-Rust): tilføj gerne repo-URL i denne fil.*

---

## Struktur på topniveau

| Mappe | Beskrivelse | GitHub-repo |
|-------|-------------|-------------|
| **Projekter** | Standalone projekter (ofte med eget GitHub-repo) | — |
| **Hovedforløb** | H1/H2/H4 templates + MAGS-elever (Blazor, API, Aspire) | [mercantech/h1](https://github.com/mercantech/h1), [h2](https://github.com/mercantech/h2), [h4](https://github.com/mercantech/h4) |
| **DOD** | DevOps-dag: backend, frontend, Proxi, CICD, DNS, Nginx, Sikkerhed m.m. | [Proxi](https://github.com/mercantech/Proxi) |
| **GF2** | Grundlæggende programmering 2: C#, WPF, MSTest, opgaver, konsol/Blazor | [mercantech/gf2](https://github.com/mercantech/gf2) |
| **GF2-MAGS** | GF2 for MAGS: WPF, UnitTest, Blazor, Konsol, Teori, Opgaver | (del af gf2 / monorepo) |
| **Learning-Languages** | Sprog/tekst (fx Learning-Rust) | — |
| **MachineLearning** | ML-forløb: Dataanalyse, Regression, Classification, Cluster, Fit, TimeSeries, NeuralNetwork, Reinforcement | [Mercantech/MachineLearning](https://github.com/Mercantech/MachineLearning) |
| **Obsidian** | Obsidian-vault/noter (denne mappe) | — |

---

## Projekter (standalone – gode kandidater til forking)

Disse ligger under `Monorepo/Projekter/` og er typisk projekter du også har som separate GitHub-repos.

| Repo/projekt | Teknologi / formål | GitHub |
|--------------|--------------------|--------|
| **ActiveDirectoryTesting** | .NET (C#) | — |
| **Aspire-Exampels** | .NET Aspire (AppHost, API, Web, ServiceDefaults, Tests) | — |
| **CTF** | Capture The Flag / sikkerhed | — |
| **eval** | Blazor + Aspire (eval.AppHost, Blazor, ServiceDefaults) | [Mercantech/eval](https://github.com/Mercantech/eval) |
| **videnstjek** | Blazor (.NET 9) | [Mercantech/videnstjek](https://github.com/Mercantech/videnstjek) |

*Tip: Brug [New Git repo.md](New%20Git%20repo.md) til at tilføje nye repos til monorepo eller til at genoprette forbindelse til det oprindelige repo.*

---

## Hovedforløb

- **Templates**  
  - **H1** – fx Banko (Blazor + BankoCheater), kan have egen `.git` → [mercantech/h1](https://github.com/mercantech/h1) / [MAGS-Template/H1-Projekt](https://github.com/MAGS-Template/H1-Projekt)  
  - **H2** – Blazor/API/DomainModels/AppHost/ServiceDefaults-template → [mercantech/h2](https://github.com/mercantech/h2)  
  - **H4** – Backend-template (API, AppHost, ServiceDefaults, DomainModels) → [mercantech/h4](https://github.com/mercantech/h4)
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

- **GF2**: CSharp-Bogen, MSTest-Opgaver, Opgaver, WPF, Projekter (Konsol, Blazor), Notes → [mercantech/gf2](https://github.com/mercantech/gf2)
- **GF2-MAGS**: WPF, UnitTest, Teori, Opgaver, Projekter (Konsol, Blazor)

---

## Learning-Languages

- **Learning-Rust**

---

## MachineLearning

Jupyter/Notion-forløb med mapper:  
`0.Dataanalyse`, `1.Regression`, `2.Classification`, `3.Cluster`, `4.Fit`, `5.TimeSeries`, `6.NeuralNetwork`, `7.Reinforcement`, `MLWithPython`.

- **GitHub:** [Mercantech/MachineLearning](https://github.com/Mercantech/MachineLearning)  
- Se `MachineLearning/README.md` i monorepo-roden for pensum og opgaver.

---

## Hurtig reference: hvor er hvad?

- **Undervisning / fork til elever** → `Projekter/`, `Hovedforløb/` (Templates + MAGS), `GF2/`, `GF2-MAGS/`
- **DevOps/Infra** → `DOD/`
- **Machine Learning** → `MachineLearning/`
- **Tilføj nyt repo til monorepo** → [New Git repo.md](New%20Git%20repo.md)
