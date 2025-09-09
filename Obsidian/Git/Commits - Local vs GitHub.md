# Git Commits - Lokalt vs GitHub

## Hvad er et Commit?

Et commit er et snapshot af dine filer på et bestemt tidspunkt. Tænk på det som at tage et billede af din kode, som du altid kan vende tilbage til senere.

```
Working Directory → Staging Area → Local Repository → Remote Repository
     (add)            (commit)         (push)
```

## Commit Anatomi

Hvert commit indeholder:
- **Unique ID (SHA)**: f.eks. `a1b2c3d4e5f6...`
- **Author**: Hvem lavede ændringen
- **Timestamp**: Hvornår blev det lavet
- **Message**: Beskrivelse af ændringen
- **Parent commit(s)**: Hvilket commit kom før
- **File changes**: Præcis hvad blev ændret

## Lokale Commits vs Remote Commits

### Lokale Commits (På din computer)

```
Din Computer:
Working Dir → Staging → Local Repo
    |           |         |
  [filer]    [git add]  [git commit]
```

**Hvad sker der lokalt:**
1. Du ændrer filer i working directory
2. `git add` flytter ændringer til staging area
3. `git commit` gemmer snapshot i local repository
4. Commit eksisterer KUN på din computer

### Remote Commits (På GitHub)

```
GitHub Server:
Remote Repository
       |
   [git push]
       |
Din Computer:
Local Repository
```

**Hvad sker der ved push:**
1. `git push` sender commits til GitHub
2. Andre kan nu se dine ændringer
3. Commits bliver del af projektets historie
4. Backup af dit arbejde

## Praktisk Eksempel

### Scenario: Tilføj ny funktion

#### Step 1: Lokalt arbejde
```bash
# Ændre fil
echo "function newFeature() {}" >> app.js

# Se status
git status
# Output: app.js er modified

# Stage ændringer
git add app.js

# Commit lokalt
git commit -m "Add new feature function"
```

**På dette tidspunkt:**
```
Din Computer:     GitHub:
A---B---C         A---B
        |
    [ny commit]   [ingen ændring]
```

#### Step 2: Push til GitHub
```bash
# Send til GitHub
git push origin main
```

**Nu:**
```
Din Computer:     GitHub:
A---B---C         A---B---C
                      |
                  [synkroniseret]
```

## Detaljeret Commit Workflow

### 1. Working Directory
```
app.js (modified)
style.css (new file)
README.md (unchanged)
```

### 2. Staging Area (efter git add)
```bash
git add app.js style.css

# Staging area indeholder nu:
# - app.js changes
# - style.css (helt ny)
# README.md er IKKE staged
```

### 3. Local Repository (efter git commit)
```bash
git commit -m "Add styling and update app logic"

# Nyt commit oprettes med:
# - Unique SHA: e4f5a6b7c8d9...
# - Message: "Add styling and update app logic"
# - Changes: app.js + style.css
# - Parent: forrige commit
```

### 4. Remote Repository (efter git push)
```bash
git push origin main

# GitHub modtager:
# - Det nye commit
# - Alle file changes
# - Commit metadata
# - Opdaterer branch pointer
```

## Visualisering af Commit Historie

### Før Push
```
Local:    A---B---C---D (main)
                      ↑
                   HEAD

Remote:   A---B (origin/main)
```

### Efter Push
```
Local:    A---B---C---D (main)
                      ↑
                   HEAD

Remote:   A---B---C---D (origin/main)
```

## Commit States og Kommandoer

### Se Commit Status
```bash
# Se lokale commits der ikke er pushed
git log origin/main..HEAD

# Se forskel mellem local og remote
git status

# Se commit historie
git log --oneline --graph
```

### Eksempel Output
```bash
$ git status
On branch main
Your branch is ahead of 'origin/main' by 2 commits.
  (use "git push" to publish your local commits)

$ git log origin/main..HEAD
commit d4e5f6a7b8c9 (HEAD -> main)
Author: Mathias <email@example.com>
Date: Tue Sep 9 14:30:00 2025
    Add user authentication

commit c3d4e5f6a7b8
Author: Mathias <email@example.com>  
Date: Tue Sep 9 14:15:00 2025
    Fix login validation
```

## Hvad Sker Der Under Motorhjelmen?

### Lokalt Commit
```
.git/objects/
├── a1/b2c3d4... (commit object)
├── e5/f6a7b8... (tree object - folder structure)  
├── c9/d0e1f2... (blob object - file content)
└── refs/heads/main (pointer til seneste commit)
```

### Efter Push
```
GitHub Repository:
├── Same objects som lokalt
├── refs/heads/main (opdateret pointer)
└── Synkroniseret med local repo
```

## Almindelige Situationer

### Situation 1: Flere Lokale Commits
```bash
# Lav flere commits
git commit -m "Fix bug A"
git commit -m "Add feature B" 
git commit -m "Update docs"

# Push alle på én gang
git push origin main
```

### Situation 2: Andre Har Pushed
```bash
# Forsøg push
git push origin main
# Error: Updates were rejected

# Løsning: Pull først
git pull origin main
git push origin main
```

### Situation 3: Konflikt Ved Pull
```bash
git pull origin main
# Auto-merging failed

# Løs konflikter manuelt
# Derefter:
git add resolved-files
git commit -m "Resolve merge conflicts"
git push origin main
```

## Commit Best Practices

### Gode Commit Messages
```bash
# ✅ Gode eksempler
git commit -m "Add user login validation"
git commit -m "Fix header alignment on mobile"
git commit -m "Update API documentation"

# ❌ Dårlige eksempler  
git commit -m "fix"
git commit -m "changes"
git commit -m "asdf"
```

### Commit Størrelse
```bash
# ✅ Små, fokuserede commits
git commit -m "Add email validation"
git commit -m "Add password strength check"

# ❌ Store, blandede commits
git commit -m "Add login, fix bugs, update styles, refactor code"
```

## Avancerede Commit Operationer

### Ændre Sidste Commit (Før Push)
```bash
# Tilføj mere til sidste commit
git add forgotten-file.js
git commit --amend -m "Updated commit message"
```

### Se Commit Detaljer
```bash
# Se specifikt commit
git show a1b2c3d4

# Se ændringer i commit
git diff HEAD~1 HEAD
```

### Sammenlign Local vs Remote
```bash
# Se commits der mangler på remote
git log origin/main..HEAD

# Se commits på remote som du mangler
git log HEAD..origin/main
```

## Troubleshooting

### Problem: "Your branch is ahead of origin/main"
```bash
# Løsning: Push dine commits
git push origin main
```

### Problem: "Updates were rejected"
```bash
# Løsning: Pull først, derefter push
git pull origin main
git push origin main
```

### Problem: Forkert commit message
```bash
# Hvis ikke pushed endnu:
git commit --amend -m "Correct message"

# Hvis allerede pushed (undgå hvis muligt):
git revert commit-hash
```

## Commit Workflow Diagram

```
1. Arbejd på filer
   ↓
2. git add (stage changes)
   ↓  
3. git commit (save locally)
   ↓
4. git push (send to GitHub)
   ↓
5. Andre kan pull dine ændringer
```

## Sammenfatning

**Lokale Commits:**
- Gemmes kun på din computer
- Hurtige og private
- Kan ændres før push
- Backup kun lokalt

**Remote Commits (GitHub):**
- Synlige for alle
- Permanent projekthistorie  
- Backup i skyen
- Grundlag for samarbejde

**Husk:** Commit ofte lokalt, push når du er klar til at dele!

---

*Denne guide forklarer forskellen mellem lokale og remote commits. Øv dig med små eksperimenter for at forstå flowet bedre.*