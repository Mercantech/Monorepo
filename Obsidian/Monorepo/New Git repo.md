# Guide: Tilføj ny repo til Monorepo

Denne guide viser hvordan man tilføjer et eksisterende GitHub repo til vores monorepo struktur.

## Forudsætninger
- PowerShell terminal
- Git installeret
- Adgang til det originale repo

## Trin-for-trin proces

### 1. Clone det originale repo direkte til monorepo
```powershell
cd C:\Users\MAGS\Documents\GitHub\Monorepo\Projekter
git clone https://github.com/[ORGANISATION]/[REPO-NAVN] [Repo-Navn]
```

### 2. Slet .git mappe (unbind fra originalt repo)
```powershell
cd [Repo-Navn]
Remove-Item -Recurse -Force .git
```

### 3. Initialiser ny git forbindelse
```powershell
git init
git remote add origin https://github.com/[ORGANISATION]/[REPO-NAVN].git
```

### 4. Commit og push
```powershell
git add .
git commit -m "Move project to monorepo structure"
git branch -M main
git pull origin main --allow-unrelated-histories
git push -u origin main
```

## Eksempel: Videnstjek repo
```powershell
# Clone direkte til monorepo
cd C:\Users\MAGS\Documents\GitHub\Monorepo\Projekter
git clone https://github.com/Mercantech/videnstjek videnstjek

# Unbind
cd videnstjek
Remove-Item -Recurse -Force .git

# Rebind
git init
git remote add origin https://github.com/Mercantech/videnstjek.git
git add .
git commit -m "Move project to monorepo structure"
git branch -M main
git pull origin main --allow-unrelated-histories
git push -u origin main
```

## Vigtige noter
- `--allow-unrelated-histories` er nødvendig når man merger to separate git historier
- Hvis der opstår merge conflicts, løs dem manuelt før push
- Brug `git push --force` kun hvis du er sikker på at overskrive det originale repo

## Fejlfinding
- **"remote origin already exists"**: Remote er allerede tilføjet, fortsæt med næste trin
- **"Updates were rejected"**: Brug `git pull origin main --allow-unrelated-histories` først
- **Merge conflicts**: Løs konflikterne manuelt og commit derefter
