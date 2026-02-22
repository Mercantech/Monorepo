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

| Sti | List-Repos nr. | Forslag |
|-----|----------------|--------|
| Courses/H4-MAGS | 3 | Metode B: `Sync-OneRepo -Index 3 -Pull`, derefter fjern .git + git rm --cached + git add |
| Courses/Templates/H1 | 5 | Samme |
| Courses/Templates/H5 | 9 | Samme |
| Projekter/videnstjek | 10 | Samme |
| Projekter/activeDirectoryTesting | 11 | Samme |
| Projekter/aspire-exampels | 12 | Samme |
| Projekter/ctf | 13 | Samme |
| GF2 | 16 | Samme |
| MachineLearning | 18 | Samme |

**Tip:** Kør `.\scripts\List-Repos.ps1` for at se de præcise numre (de kan ændres hvis du redigerer repos.json).

---

## Efter én mappe: tjek igen

```powershell
.\scripts\Get-RepoStatus.ps1
```

Når den mappe viser "OK", er du færdig med den. Gentag for næste submodule.

---

## Hvis git rm --cached siger "did not match any file"

Så er stien ikke i index som gitlink. Så er det nok kun .git inde i mappen der gør den "Submodule". Fjern kun .git og kør git add:

```powershell
Remove-Item -Recurse -Force "STI\.git" -ErrorAction SilentlyContinue
git add "STI"
git status
```

---

## Kort checkliste per repo

1. `Sync-OneRepo.ps1 -Index <nr> -Pull` (eller -FolderPath "sti" -Pull)
2. `Remove-Item -Recurse -Force "sti\.git" -ErrorAction SilentlyContinue`
3. `git rm --cached "sti"` (hvis det fejler, spring over)
4. `git add "sti"`
5. `Get-RepoStatus.ps1` → mappen skal stå som OK
6. Når du har gjort det for flere: `git commit -m "Konverter X, Y, Z til almindelige filer"` og `git push`
