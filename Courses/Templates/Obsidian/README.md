# Obsidian-notesbog – fælles skabelon til H1–H5

Denne mappe er den **fælles Obsidian-skabelon** til alle hovedforløb, baseret på **H4-New** (seneste opdaterede template). Alle forløb (H1, H2, H3, H4, H5) får samme plugins, layout og idéer – så du kan kopiere denne mappe ind i et givet forløb og erstatte den gamle notesbog.

## Indhold

| Indhold | Beskrivelse |
|--------|-------------|
| **`.obsidian/`** | Konfiguration: core + community plugins, workspace-layout, graph, vedhæftninger i `Noter/Assets`. |
| **`Noter/`** | Notesbogens rod: `Fællesnoter/`, `Plugins/`, `Assets/`. |

### Core plugins

File explorer, Search, Quick switcher, Graph, Backlink, Outgoing link, Tags, Page preview, Daily notes, Templates, Note composer, Bookmarks, Outline, Word count, Editor status, File recovery, Sync, Bases m.m. (Properties slået fra som i H4.)

### Community plugins (fra H4-New)

- **Mind Map** – vis noter som mind map
- **Excalidraw** – tegn skitser og diagrammer i noter
- **Iconize** (obsidian-icon-folder) – ikoner på filer, mapper og tekster
- **Banners** – bannerbilleder øverst i noter

### Noter-struktur

- **Noter/Fællesnoter/Velkommen.md** – velkomstnote med kort vejledning
- **Noter/Plugins/1. Plugins - Allerede installeret.md** – oversigt over plugins
- **Noter/Assets/** – standardmappe til vedhæftede filer (sættes i app.json)

---

## Sådan bruger du skabelonen i H1, H2, H3, H4 eller H5

### 1. Kopier ind i forløbet

Kopier **hele indholdet** af mappen `Obsidian` (inkl. `.obsidian` og `Noter`) ind i det forløb, du vil opdatere:

- **Eksempel til H2:** Kopiér alt fra `Templates/Obsidian/` ind i `Templates/H2/`. Er der allerede en `.obsidian`-mappe og en notes-mappe (fx `H2-Noter`), **erstat** dem med det, du lige har kopieret (eller slet den gamle først og læg derefter det nye ind).
- **Eksempel til H4:** Samme fremgangsmåde – kopiér ind i `Templates/H4/` (eller H4-New) og erstat evt. eksisterende Obsidian-opsætning.

### 2. Omdøb notes-mappen til forløbsnavn

Efter kopiering skal notes-mappen hedde det samme som i de andre forløb:

- Omdøb **`Noter`** til **`H1-Noter`** i H1  
- Omdøb **`Noter`** til **`H2-Noter`** i H2  
- Omdøb **`Noter`** til **`H3-Noter`** i H3  
- Omdøb **`Noter`** til **`H4-Noter`** i H4  
- Omdøb **`Noter`** til **`H5-Noter`** i H5  

Så matcher strukturen på tværs af forløb.

### 3. Opdater attachment-mappe i Obsidian (valgfrit)

`app.json` sætter `attachmentFolderPath` til `Noter/Assets`. Når du har omdøbt `Noter` til fx `H2-Noter`, kan du i Obsidian under **Indstillinger → Filer og links** evt. ændre “Standard placering for nye vedhæftninger” til `H2-Noter/Assets` – eller lade den stå og flytte mappen manuelt én gang.

### 4. Åbn vault i Obsidian

- Åbn Obsidian og vælg **Open folder as vault**.
- Vælg **forløbets rod-mappe** (fx `Templates/H2` eller `Templates/H4`).  
  Obsidian bruger den `.obsidian`-mappe, der ligger i den mappe du åbner.

**Bemærk:** Første gang efter omdøbning peger workspace måske stadig på `Noter/Fællesnoter/Velkommen.md`. Åbn i stedet `Hx-Noter/Fællesnoter/Velkommen.md` én gang – derefter husker Obsidian den rigtige fil.

---

## Opdatere skabelonen (Templates/Obsidian)

Når du ændrer i **H4-New** (eller en anden “master”-opsætning):

1. Kopiér de opdaterede filer fra forløbets `.obsidian` og `Hx-Noter` til **`Templates/Obsidian/`** (erstat med den nye version).
2. Sørg for at alle stier i konfigurationen bruger **`Noter`** (ikke `H4-Noter` osv.), så skabelonen forbliver generisk.
3. Derefter kan du kopiere den opdaterede `Obsidian`-mappe ind i de andre forløb som beskrevet ovenfor.

Så holder I samme Obsidian-opsætning og idéer på tværs af alle forløb.
