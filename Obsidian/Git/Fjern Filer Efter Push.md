# Fjern Filer Efter Push - Git Guide

## Problem
Du har ved et uheld pushed følsomme filer som `appsettings.json`, `config.json` eller andre konfigurationsfiler til dit Git repository, og har efterfølgende tilføjet dem til `.gitignore`. Men filerne er stadig synlige i Git historikken.

## Løsning

### 1. Tilføj filen til .gitignore (hvis ikke allerede gjort)
```bash
echo "appsettings.json" >> .gitignore
echo "appsettings.Development.json" >> .gitignore
```

### 2. Fjern filen fra Git tracking (men behold lokalt)
```bash
git rm --cached appsettings.json
git rm --cached appsettings.Development.json
```

**Vigtigt:** `--cached` flag betyder at filen kun fjernes fra Git, men beholdes på dit lokale filsystem.

### 3. Commit ændringerne
```bash
git add .gitignore
git commit -m "Remove appsettings files from tracking and add to .gitignore"
```

### 4. Push til remote repository
```bash
git push origin main
```

## Hvis filen skal fjernes helt fra historikken

**⚠️ ADVARSEL:** Dette omskriver Git historik og kan være destruktivt. Koordiner med dit team først!

### Metode 1: git filter-branch (ældre metode)
```bash
git filter-branch --force --index-filter \
  'git rm --cached --ignore-unmatch appsettings.json' \
  --prune-empty --tag-name-filter cat -- --all
```

### Metode 2: BFG Repo-Cleaner (anbefalet)
1. Download BFG: https://rtyley.github.io/bfg-repo-cleaner/
2. Kør kommandoen:
```bash
java -jar bfg.jar --delete-files appsettings.json
git reflog expire --expire=now --all && git gc --prune=now --aggressive
```

### Metode 3: git filter-repo (moderne metode)
```bash
# Installer git-filter-repo først
pip install git-filter-repo

# Fjern filen fra hele historikken
git filter-repo --path appsettings.json --invert-paths
```

### 5. Force push (kun hvis du har omskrevet historik)
```bash
git push --force-with-lease origin main
```

## Best Practices

### Forebyggelse
- Opret `.gitignore` før du starter projektet
- Brug templates til almindelige projekttyper
- Tjek altid `git status` før `git add .`

### Almindelige filer at ignorere i .NET projekter
```gitignore
# Konfigurationsfiler
appsettings.json
appsettings.*.json
*.config

# Secrets
secrets.json
.env
.env.local

# Build output
bin/
obj/
*.dll
*.exe

# IDE filer
.vs/
.vscode/
*.user
*.suo
```

### Sikkerhedstips
- Skift alle passwords/API keys der var i den eksponerede fil
- Overvej at rotere secrets selvom filen er fjernet
- Brug secret management systemer som Azure Key Vault eller AWS Secrets Manager

## Eksempel Workflow

```bash
# 1. Tjek hvad der er tracked
git ls-files | grep appsettings

# 2. Fjern fra tracking
git rm --cached appsettings.json

# 3. Tilføj til .gitignore
echo "appsettings.json" >> .gitignore

# 4. Commit og push
git add .gitignore
git commit -m "Remove appsettings.json from tracking"
git push origin main
```

## Verifikation
Tjek at filen ikke længere er tracked:
```bash
git ls-files | grep appsettings
# Skulle ikke returnere noget
```

## Teamwork Overvejelser
- Informer teamet før du omskriver historik
- Alle skal re-clone repository efter force push
- Overvej at bruge `--force-with-lease` i stedet for `--force`