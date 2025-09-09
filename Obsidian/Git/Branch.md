# Git Branches - Komplet Guide

## Hvad er en Branch?

En branch i Git er en let, bevægelig pointer til et specifikt commit. Tænk på det som en parallel udviklingslinje, hvor du kan arbejde på nye features eller eksperimenter uden at påvirke hovedkoden.

```
main:    A---B---C-------F---G
              \         /
feature:       D---E---/

A: Initial commit
B: Add login  
C: Bug fix
D: Start new feature
E: Complete feature
F: Merge feature
G: Release v1.0
```

## Hvorfor Bruge Branches?

- **Isolation**: Arbejd på features uden at påvirke main branch
- **Eksperimentering**: Test nye ideer sikkert
- **Samarbejde**: Flere udviklere kan arbejde parallelt
- **Organisering**: Hold forskellige features adskilt

## Branch Kommandoer

### Opret og Skift Branch
```bash
# Opret ny branch
git branch feature-login

# Skift til branch
git checkout feature-login

# Opret og skift i én kommando
git checkout -b feature-login
```

### Se Branches
```bash
# Vis lokale branches
git branch

# Vis alle branches (lokale og remote)
git branch -a

# Vis branches med sidste commit
git branch -v
```

## Branch Workflow Visualisering

### Før Branch Creation
```
main: A---B---C
```

### Efter Branch Creation
```
main:    A---B---C
              \
feature:       D---E
```

### Efter Merge
```
main: A---B---C-------F
           \         /
feature:    D---E---/
```

## Merge vs Rebase

### Merge (Bevarer Historie)
```
main:    A---B---C-------M
              \         /
feature:       D---E---/

M = Merge commit (bevarer branch historie)
```

### Rebase (Lineær Historie)
```
Før rebase:
main:    A---B---C
              \
feature:       D---E

Efter rebase:
main:    A---B---C---D'---E'

D' og E' = Rebasede commits (ny historie)
```

## Branch Strategier

### Git Flow
```
main:     A-----------M-------R
           \         /       /
develop:    B---C---D---E---/
                 \     /
feature:          F---G

A: Initial commit
B: Start develop
C: Dev work  
F-G: Feature work
D: Merge feature
E: Prepare release
M: Merge to main
R: Release v1.0
```

### GitHub Flow (Simplere)
```
main:     A---B-------D---E
               \     /
feature:        C---/

A: Initial
B: Stable
C: New feature + tests
D: Merge feature  
E: Deploy
```

## Praktiske Eksempler

### Scenario 1: Ny Feature
```bash
# Start fra main
git checkout main
git pull origin main

# Opret feature branch
git checkout -b feature/user-authentication

# Arbejd på feature
git add .
git commit -m "Add login form"
git commit -m "Add password validation"

# Push til remote
git push origin feature/user-authentication

# Merge tilbage til main
git checkout main
git merge feature/user-authentication
git push origin main

# Slet branch
git branch -d feature/user-authentication
```

### Scenario 2: Hotfix
```bash
# Opret hotfix branch fra main
git checkout main
git checkout -b hotfix/critical-bug

# Fix bug
git add .
git commit -m "Fix critical security issue"

# Merge til main
git checkout main
git merge hotfix/critical-bug
git push origin main

# Merge til develop også (hvis du bruger Git Flow)
git checkout develop
git merge hotfix/critical-bug
git push origin develop

# Slet hotfix branch
git branch -d hotfix/critical-bug
```

## Branch Navngivning

### Gode Konventioner
```
feature/user-login
feature/payment-integration
bugfix/header-alignment
hotfix/security-patch
release/v2.1.0
```

### Undgå
```
test
temp
my-branch
branch1
```

## Konflikt Håndtering

### Når Merge Konflikter Opstår
```bash
# Forsøg merge
git merge feature-branch

# Hvis konflikter opstår
# 1. Åbn konflikt filer
# 2. Løs konflikter manuelt
# 3. Stage løste filer
git add resolved-file.js

# 4. Fuldfør merge
git commit
```

### Konflikt Eksempel
```javascript
<<<<<<< HEAD
function login(username, password) {
    // Main branch version
    return authenticate(username, password);
}
=======
function login(user, pass) {
    // Feature branch version
    return validateAndAuthenticate(user, pass);
}
>>>>>>> feature-branch
```

## Remote Branches

### Arbejde med Remote Branches
```bash
# Hent remote branches
git fetch origin

# Checkout remote branch
git checkout -b local-branch origin/remote-branch

# Push ny branch til remote
git push -u origin new-branch

# Slet remote branch
git push origin --delete old-branch
```

## Branch Visualisering Tools

### Kommandolinje
```bash
# Simpel log
git log --oneline --graph

# Detaljeret branch graf
git log --graph --pretty=format:'%Cred%h%Creset -%C(yellow)%d%Creset %s %Cgreen(%cr) %C(bold blue)<%an>%Creset' --abbrev-commit --all
```

### GUI Tools
- GitKraken
- SourceTree
- GitHub Desktop
- VS Code Git Graph extension

## Best Practices

### Do's ✅
- Brug beskrivende branch navne
- Hold branches små og fokuserede
- Merge eller rebase regelmæssigt fra main
- Slet branches efter merge
- Test før merge

### Don'ts ❌
- Arbejd ikke direkte på main branch
- Lad ikke branches blive for gamle
- Commit ikke ufærdigt arbejde til shared branches
- Glem ikke at pull før du starter ny branch

## Avancerede Branch Teknikker

### Cherry-pick
```bash
# Kopier specifikt commit til current branch
git cherry-pick abc123
```

### Stash før Branch Switch
```bash
# Gem ændringer midlertidigt
git stash

# Skift branch
git checkout other-branch

# Hent ændringer tilbage
git stash pop
```

### Branch fra Specifikt Commit
```bash
# Opret branch fra specifikt commit
git checkout -b new-branch abc123
```

## Troubleshooting

### Almindelige Problemer

**Problem**: Kan ikke skifte branch pga. uncommitted changes
```bash
# Løsning 1: Commit ændringer
git add .
git commit -m "WIP: Save progress"

# Løsning 2: Stash ændringer
git stash
git checkout other-branch
git stash pop
```

**Problem**: Branch er ikke synkroniseret med remote
```bash
# Hent seneste ændringer
git fetch origin

# Merge remote ændringer
git merge origin/branch-name
```

**Problem**: Forkert branch navn
```bash
# Omdøb branch
git branch -m old-name new-name

# Hvis branch er pushed til remote
git push origin :old-name new-name
git push origin -u new-name
```

---

*Denne guide dækker de vigtigste aspekter af Git branches. Øv dig med små eksperimenter i test repositories for at blive fortrolig med koncepterne.*