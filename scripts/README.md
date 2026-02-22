# Monorepo-scripts (fuld kopi – ingen submodules)

Disse scripts bruger **almindelige filer** i monorepo: hver mappe er en fuld kopi af et GitHub-repo. Når du cloner monorepo, får du alt. GitHub-repoet opdateres ikke automatisk – du syncer med scriptene.

## Krav

- Kør scriptene fra **Monorepo-roden** (eller sørg for at `scripts/` ligger i monorepo og `repos.json` i `scripts/`).
- PowerShell, Git.

## repos.json

Listen over mapper der skal synces: `path` = relativ sti i monorepo, `url` = GitHub-repo-URL.  
Nye tilføjelser sker enten manuelt eller via `Add-RepoAsCopy.ps1`.

---

## 1. Add-RepoAsCopy.ps1

Tilføjer et **nyt** GitHub-repo til monorepo som almindelige filer.

- Clone repo til angivet mappe, slet `.git`, opdater `repos.json`.
- Mappen må ikke findes med indhold i forvejen.

```powershell
cd C:\Users\mathi\Documents\GitHub\Monorepo
.\scripts\Add-RepoAsCopy.ps1 -RepoUrl "https://github.com/Mercantech/h3.git" -FolderPath "Courses/Templates/H3"
```

Derefter: `git add <mappe> scripts/repos.json`, commit, push (i monorepo).

---

## 2. Push-MonorepoFolderToRepo.ps1

Pusher indholdet af **én** mappe i monorepo til det tilhørende GitHub-repo.

- Bruges når du har ændret filer i monorepo og vil have dem op på fx H3 på GitHub.

```powershell
.\scripts\Push-MonorepoFolderToRepo.ps1 -FolderPath "Courses/Templates/H3"
.\scripts\Push-MonorepoFolderToRepo.ps1 -FolderPath "Courses/Templates/H3" -CommitMessage "Opdater README"
.\scripts\Push-MonorepoFolderToRepo.ps1 -FolderPath "Courses/Templates/H3" -WhatIf
```

---

## 3. Pull-RepoIntoMonorepo.ps1

Henter indhold fra **ét** GitHub-repo ind i den tilhørende mappe i monorepo (overskriver lokalt).

- Bruges når ændringer er lavet direkte i repoet på GitHub og du vil have dem ind i monorepo.

```powershell
.\scripts\Pull-RepoIntoMonorepo.ps1 -FolderPath "Courses/Templates/H3"
.\scripts\Pull-RepoIntoMonorepo.ps1 -FolderPath "Courses/Templates/H3" -WhatIf
```

---

## 4. Sync-AllRepos.ps1

Kører push og/eller pull for **alle** mapper i `repos.json`.

```powershell
.\scripts\Sync-AllRepos.ps1 -Push
.\scripts\Sync-AllRepos.ps1 -Pull
.\scripts\Sync-AllRepos.ps1 -Push -Pull
.\scripts\Sync-AllRepos.ps1 -Push -WhatIf
```

---

## 5. Gennemgå repos enkeltvis (List-Repos + Sync-OneRepo)

Når du vil gå **én repo ad gangen** (fx for at løse merge-konflikter):

**1. Se listen med numre:**
```powershell
.\scripts\List-Repos.ps1
```
Du får fx: `  3.   Courses/Templates/H3  ->  https://github.com/Mercantech/h3.git`

**2. Kør Push eller Pull for den repo (brug nummeret):**
```powershell
.\scripts\Sync-OneRepo.ps1 -Index 3 -Push
.\scripts\Sync-OneRepo.ps1 -Index 3 -Pull
```
Eller med sti direkte:
```powershell
.\scripts\Sync-OneRepo.ps1 -FolderPath "Courses/Templates/H3" -Push
.\scripts\Sync-OneRepo.ps1 -FolderPath "Courses/Templates/H3" -Pull -WhatIf
```

---

## Merge-konflikter / push afvist

- **Push afvist (remote har nye commits):**  
  Vælg én kilde til sandhed:  
  - **Monorepo er sandheden:** Kør push igen med **-Force** (overskriver GitHub).  
    `.\scripts\Sync-OneRepo.ps1 -Index 3 -Push -Force`  
  - **GitHub har også vigtige ændringer:** Kør først **Pull** for den repo, så monorepo får GitHub-versionen. Rediger evt. i monorepo og push igen (uden -Force).

- **Pull:** Overskriver alt i monorepo-mappen med indhold fra GitHub. Lokale ændringer i den mappe går tabt – commit i monorepo først hvis du vil bevare dem.

- **Git viser "modified (untracked content)" eller submodule:** Mappen har en `.git` inde i sig, så monorepo tracker den som reference i stedet for filer. Fjern `.git` i mappen og fortæl Git at tracke mappen som almindelige filer:
  ```powershell
  Remove-Item -Recurse -Force "Courses/Templates/H3\.git" -ErrorAction SilentlyContinue
  git rm --cached "Courses/Templates/H3"
  git add "Courses/Templates/H3"
  ```
  (Erstat sti efter behov.) Derefter `git status` – du skal se enkelte filer, ikke "submodule".

---

## Typisk flow

1. **Tilføj nyt repo:** `Add-RepoAsCopy.ps1` → commit + push i monorepo.
2. **Arbejd i monorepo:** Rediger filer, commit og push i monorepo som sædvanligt.
3. **Opdater GitHub-repoet:** `Push-MonorepoFolderToRepo.ps1 -FolderPath "..."` (eller `Sync-AllRepos.ps1 -Push`).
4. **Hent ændringer fra GitHub ind i monorepo:** `Pull-RepoIntoMonorepo.ps1 -FolderPath "..."` (eller `Sync-AllRepos.ps1 -Pull`).
