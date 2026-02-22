# Guide: Konverter submodules til almindelige filer

Når **Get-RepoStatus.ps1** viser "Submodule" for en mappe, betyder det at mappen har en `.git` eller er registreret som gitlink. Her er hvordan du får den over til almindelige filer (så den bliver "OK").

---

## Valg: Hvor skal indholdet komme fra?

- **Monorepo-mappen har allerede det rigtige indhold** → brug **Metode A** (fjern kun .git og re-registrér).
- **GitHub-repoet er den rigtige kilde** → brug **Metode B** (Pull først, derefter fjern .git og add).

---

## Metode A: Indholdet ligger allerede korrekt i mappen

Brug denne hvis du ikke skal hente noget nyt fra GitHub – du vil bare at Git skal tracke filerne i stedet for at se mappen som submodule.

**Kør fra monorepo-roden** (erstat `STI` med den faktiske sti, fx `Courses/H4-MAGS`):

```powershell
# 1. Fjern .git inde i mappen (så monorepo ikke ser den som repo)
Remove-Item -Recurse -Force "STI\.git" -ErrorAction SilentlyContinue

# 2. Fjern submodule/gitlink fra Git-index (slet ikke filer)
git rm --cached "STI"

# 3. Tilføj mappen som almindelige filer
git add "STI"

# 4. Tjek
git status
```

Derefter: `git commit -m "Konverter STI til almindelige filer"` og `git push`.

---

## Metode B: Hent indhold fra GitHub, derefter konverter

Brug denne hvis du vil have den seneste version fra GitHub ind i monorepo og derefter tracke den som almindelige filer.

**1. Pull (henter fra GitHub og overskriver mappen – scriptet kopierer ikke .git længere):**

```powershell
.\scripts\Sync-OneRepo.ps1 -FolderPath "STI" -Pull
```

Eller brug nummer fra `.\scripts\List-Repos.ps1`, fx:

```powershell
.\scripts\Sync-OneRepo.ps1 -Index 4 -Pull   # for Courses/H4-MAGS
```

**2. Hvis Get-RepoStatus stadig viser Submodule** (fx fordi der lå en .git før Pull), fjern den og re-add:

```powershell
Remove-Item -Recurse -Force "STI\.git" -ErrorAction SilentlyContinue
git rm --cached "STI"
git add "STI"
```

**3. Commit og push i monorepo:**

```powershell
git add "STI"
git commit -m "Konverter STI til almindelige filer (pull + fjern submodule)"
git push origin main
```

---

## Dine 9 submodules – hurtigreference

Kør fra **monorepo-roden**. Kopier **kun én blok ad gangen** – alle 4 linjer i blokken skal bruge **samme sti** (som i overskriften, fx kun "Projekter/aspire-exampels" i blok 6). Hvis `git rm --cached` siger "did not match any files", er det ok – kør bare `git add "sti"` og fortsæt. Derefter evt. `git commit` og `git push` når du har gjort flere.

**Rigtige index** (repos.json har Banko som nr. 6, så H5 og alt under er +1):

| Sti | Nr. |
|-----|-----|
| Courses/H4-MAGS | 3 |
| Courses/Templates/H1 | 5 |
| Courses/Templates/H5 | 10 |
| Projekter/videnstjek | 12 |
| Projekter/activeDirectoryTesting | 14 |
| Projekter/aspire-exampels | 15 |
| Projekter/ctf | 16 |
| GF2 | 19 |
| MachineLearning | 21 |

---

### 1. Courses/H4-MAGS (nr. 3)

```powershell
.\scripts\Sync-OneRepo.ps1 -Index 3 -Pull
Remove-Item -Recurse -Force "Courses/H4-MAGS\.git" -ErrorAction SilentlyContinue
git rm --cached "Courses/H4-MAGS"
git add "Courses/H4-MAGS"
```

### 2. Courses/Templates/H1 (nr. 5)

```powershell
.\scripts\Sync-OneRepo.ps1 -Index 5 -Pull
Remove-Item -Recurse -Force "Courses/Templates/H1\.git" -ErrorAction SilentlyContinue
git rm --cached "Courses/Templates/H1"
git add "Courses/Templates/H1"
```

### 3. Courses/Templates/H5 (nr. 10)

```powershell
.\scripts\Sync-OneRepo.ps1 -Index 10 -Pull
Remove-Item -Recurse -Force "Courses/Templates/H5\.git" -ErrorAction SilentlyContinue
git rm --cached "Courses/Templates/H5"
git add "Courses/Templates/H5"
```

### 4. Projekter/videnstjek (nr. 12)

```powershell
.\scripts\Sync-OneRepo.ps1 -Index 12 -Pull
Remove-Item -Recurse -Force "Projekter/videnstjek\.git" -ErrorAction SilentlyContinue
git rm --cached "Projekter/videnstjek"
git add "Projekter/videnstjek"
```

### 5. Projekter/activeDirectoryTesting (nr. 14)

```powershell
.\scripts\Sync-OneRepo.ps1 -Index 14 -Pull
Remove-Item -Recurse -Force "Projekter/activeDirectoryTesting\.git" -ErrorAction SilentlyContinue
git rm --cached "Projekter/activeDirectoryTesting"
git add "Projekter/activeDirectoryTesting"
```

### 6. Projekter/aspire-exampels (nr. 15)

```powershell
.\scripts\Sync-OneRepo.ps1 -Index 15 -Pull
Remove-Item -Recurse -Force "Projekter/aspire-exampels\.git" -ErrorAction SilentlyContinue
git rm --cached "Projekter/aspire-exampels"
git add "Projekter/aspire-exampels"
```

### 7. Projekter/ctf (nr. 16)

```powershell
.\scripts\Sync-OneRepo.ps1 -Index 16 -Pull
Remove-Item -Recurse -Force "Projekter/ctf\.git" -ErrorAction SilentlyContinue
git rm --cached "Projekter/ctf"
git add "Projekter/ctf"
```

### 8. GF2 (nr. 19)

```powershell
.\scripts\Sync-OneRepo.ps1 -Index 19 -Pull
Remove-Item -Recurse -Force "GF2\.git" -ErrorAction SilentlyContinue
git rm --cached "GF2"
git add "GF2"
```

### 9. MachineLearning (nr. 21)

```powershell
.\scripts\Sync-OneRepo.ps1 -Index 21 -Pull
Remove-Item -Recurse -Force "MachineLearning\.git" -ErrorAction SilentlyContinue
git rm --cached "MachineLearning"
git add "MachineLearning"
```

**Tip:** Kør `.\scripts\List-Repos.ps1` for at se aktuelle numre. Hvis `git rm --cached` siger "did not match any file", spring den linje over.

---

### Copy-paste (rigtige index – brug denne liste)

1. Courses/H4-MAGS (nr. 3)
```
.\scripts\Sync-OneRepo.ps1 -Index 3 -Pull
Remove-Item -Recurse -Force "Courses/H4-MAGS\.git" -ErrorAction SilentlyContinue
git rm --cached "Courses/H4-MAGS"
git add "Courses/H4-MAGS"
```
2. Courses/Templates/H1 (nr. 5)
```
.\scripts\Sync-OneRepo.ps1 -Index 5 -Pull
Remove-Item -Recurse -Force "Courses/Templates/H1\.git" -ErrorAction SilentlyContinue
git rm --cached "Courses/Templates/H1"
git add "Courses/Templates/H1"
```
3. Courses/Templates/H5 (nr. 10)
```
.\scripts\Sync-OneRepo.ps1 -Index 10 -Pull
Remove-Item -Recurse -Force "Courses/Templates/H5\.git" -ErrorAction SilentlyContinue
git rm --cached "Courses/Templates/H5"
git add "Courses/Templates/H5"
```
4. Projekter/videnstjek (nr. 12)
```
.\scripts\Sync-OneRepo.ps1 -Index 12 -Pull
Remove-Item -Recurse -Force "Projekter/videnstjek\.git" -ErrorAction SilentlyContinue
git rm --cached "Projekter/videnstjek"
git add "Projekter/videnstjek"
```
5. Projekter/activeDirectoryTesting (nr. 14)
```
.\scripts\Sync-OneRepo.ps1 -Index 14 -Pull
Remove-Item -Recurse -Force "Projekter/activeDirectoryTesting\.git" -ErrorAction SilentlyContinue
git rm --cached "Projekter/activeDirectoryTesting"
git add "Projekter/activeDirectoryTesting"
```
6. Projekter/aspire-exampels (nr. 15)
```
.\scripts\Sync-OneRepo.ps1 -Index 15 -Pull
Remove-Item -Recurse -Force "Projekter/aspire-exampels\.git" -ErrorAction SilentlyContinue
git rm --cached "Projekter/aspire-exampels"
git add "Projekter/aspire-exampels"
```
7. Projekter/ctf (nr. 16)
```
.\scripts\Sync-OneRepo.ps1 -Index 16 -Pull
Remove-Item -Recurse -Force "Projekter/ctf\.git" -ErrorAction SilentlyContinue
git rm --cached "Projekter/ctf"
git add "Projekter/ctf"
```
8. GF2 (nr. 19)
```
.\scripts\Sync-OneRepo.ps1 -Index 19 -Pull
Remove-Item -Recurse -Force "GF2\.git" -ErrorAction SilentlyContinue
git rm --cached "GF2"
git add "GF2"
```
9. MachineLearning (nr. 21)
```
.\scripts\Sync-OneRepo.ps1 -Index 21 -Pull
Remove-Item -Recurse -Force "MachineLearning\.git" -ErrorAction SilentlyContinue
git rm --cached "MachineLearning"
git add "MachineLearning"
```

---

## Efter én mappe: tjek igen

```powershell
.\scripts\Get-RepoStatus.ps1
```

Når den mappe viser "OK", er du færdig med den. Gentag for næste submodule.

---

## Hvis git rm --cached siger "did not match any file"

**A)** Stien er ikke i index som gitlink → fjern kun .git og kør git add:

```powershell
Remove-Item -Recurse -Force "STI\.git" -ErrorAction SilentlyContinue
git add "STI"
git status
```

**B)** Index har stien med **anden case** (fx `Projekter/CTF` i stedet for `Projekter/ctf`). Find den præcise sti og brug den:

```powershell
git ls-files -s | findstr /i "del af sti"
# Find linjen med 160000 – den tredje kolonne er stien. Brug den sti nøjagtigt:
git rm --cached "Projekter/CTF"
git add "Projekter/ctf"
```

## Hvis Get-RepoStatus stadig viser Submodule efter Pull + git add

Prøv at fjerne .git med **fuld sti** (brug backslash på Windows) og tilføj mappen igen:

```powershell
# Erstat med din monorepo-rod og den konkrete mappe, fx:
Remove-Item -Recurse -Force "C:\Users\mathi\Documents\GitHub\Monorepo\Projekter\ctf\.git" -ErrorAction SilentlyContinue
Remove-Item -Force "C:\Users\mathi\Documents\GitHub\Monorepo\Projekter\ctf\.git" -ErrorAction SilentlyContinue
git add "Projekter/ctf"
.\scripts\Get-RepoStatus.ps1
```

Første linje fjerner .git-mappe, anden fjerner .git-fil. Derefter `git add` og tjek status.

---

## Kort checkliste per repo

1. `Sync-OneRepo.ps1 -Index <nr> -Pull` (eller -FolderPath "sti" -Pull)
2. `Remove-Item -Recurse -Force "sti\.git" -ErrorAction SilentlyContinue`
3. `git rm --cached "sti"` (hvis det fejler, spring over)
4. `git add "sti"`
5. `Get-RepoStatus.ps1` → mappen skal stå som OK
6. Når du har gjort det for flere: `git commit -m "Konverter X, Y, Z til almindelige filer"` og `git push`
