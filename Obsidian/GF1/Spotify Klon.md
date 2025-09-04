https://github.com/Mercantech/GF1-Spotify


Vi skal i gang med at kode vores Spotify/mp3-afspiller klon! 

|Tidsplan|Opgave / Teori|
|---|---|
|12:00 - 13:15|Opsætning af udvikler miljø|
|13:15 - 13:45|Pause|
|13:45 - 14:30|Teste applikation og begynd på backend|
|14:30 - 15:00|Dokumentation og GF1-logbogsopgave|

I kan tage i mod opgaven her: [https://classroom.github.com/a/SI-02YMC](https://classroom.github.com/a/SI-02YMC)

Vi skal bruge 3 programmet under forløbet!

1. VSCode - Vi skal have et sted at redigere vores kode, her er den mest populære og bedste Visual Studio Code - [https://code.visualstudio.com/](https://code.visualstudio.com/)
2. Vi skal bruge GitHub Desktop, for at gemme vores projekt. Den kræver også en GitHub bruger, som vi laver her på første dagen! - [https://desktop.github.com/download/](https://desktop.github.com/download/)
3. Til sidste skal vi bruge Node, som kan køre vores program - [https://nodejs.org/en](https://nodejs.org/en)

Hvis NPM install, ikke virker, skal I køre følgende kommando!

```powershell
Set-ExecutionPolicy -Scope Process -ExecutionPolicy Bypass
```

Her er et eksempel på en strutkur af vores sang

![[Pasted image 20250904234705.png]]

```json
[
    {
        "id": 1,
        "title": "Allergy",
        "artist": "Pa Salieu",
        "coverPath": "/covers/allergy-image.jpg",
        "songPath": "/songs/Allergy.mp3"
    }
]
```

### Backend - del 1

## **Del 1 - Opsætning af Node.js server med JS** :js:

For at lave en online spotify-klon, skal vi bruge en back-end, altså en server! Her vælger vi at bygge den med node.js som kan kører JavaScript serverside. Vi har valgt det fordi vi også skal bruge det til vores frontend, så vi kun har brug for et programmeringssprog!

Vi starter med at hente node.js på vores udvikler computer og/eller vores server - [https://nodejs.org/en](https://nodejs.org/en)

[How to Install Node.js in 2025! (Mac & Windows)](https://www.youtube.com/watch?v=BCX6UVQba4U&pp=0gcJCdgAo7VqN5tD)

Herefter skal vi i vores fortrukne terminal navigere hen til den mappe, hvor vi vil have vores projekt liggende. Det kan fx være på Skrivebordet eller i en særlig mappe til skoleprojekter.

---

**1. Opret en ny mappe til projektet:**

```bash
mkdir spotify-klon
cd spotify-klon
```

> 💡 Opgave: Prøv selv at oprette mappen og gå ind i den via terminalen!

**2. Initialiser et nyt Node.js projekt:**

```bash
npm init -y
```

Dette opretter en `package.json` fil, som holder styr på projektets indstillinger og afhængigheder.

> 💡 Opgave: Kig i din mappe – kan du finde filen package.json?

**3. Installer Express (webserver):** Vi skal bruge Express til at lave vores server. Installer det med:

```bash
npm install express
```

> 💡 Opgave: Hvad sker der i din mappe, når du kører denne kommando?

**4. Opret en fil til din serverkode:** Lav en ny fil, fx `server.js`. Det kan du gøre i din editor eller med terminalen:

```bash
touch server.js
```

> 💡 Opgave: Åbn filen server.js og gør den klar til at skrive kode i!

**5. Skriv din første serverkode:** Kopier nedenstående kode ind i `server.js`:

```jsx
const express = require('express');
const app = express();
const port = 3000;

app.get('/', (req, res) => {
  res.send('Velkommen til din Spotify-klon!');
});

app.listen(port, () => {
  console.log(`Serveren kører på <http://localhost>:${port}`);
});
```

> 💡 Opgave: Prøv at forstå, hvad koden gør. Kan du forklare det med dine egne ord?

**6. Start serveren:** Kør følgende i terminalen:

```bash
node server.js
```

Gå derefter ind på [](http://localhost:3000/)[http://localhost:3000](http://localhost:3000) i din browser og se, hvad der sker!

> 💡 Opgave: Hvad ser du i browseren? Prøv at ændre teksten i res.send(...) og genstart serveren for at se ændringen.

---

## **Del 2 - Installation og opsætning af Swagger :bookmark_tabs:**

Swagger gør det nemt at dokumentere og teste din API direkte fra browseren. Vi bruger Swagger UI og Swagger JSDoc til at lave dokumentationen automatisk ud fra kommentarer i koden.

---

**1. Installer Swagger afhængigheder:**

```bash
npm install swagger-ui-express swagger-jsdo
```

> 💡 Opgave: Kør kommandoen og tjek at der nu er kommet nye pakker i din node_modules mappe og i din package.json.

**2. Tilføj Swagger til din server:** Åbn din `server.js` og indsæt følgende kode (gerne lige efter dine andre `require` statements):

```jsx
const swaggerUi = require('swagger-ui-express');
const swaggerJSDoc = require('swagger-jsdoc');

const swaggerOptions = {
  definition: {
    openapi: '3.0.0',
    info: {
      title: 'Spotify-klon API',
      version: '1.0.0',
      description: 'API dokumentation for din Spotify-klon',
    },
  },
  apis: ['./server.js'],// Her kan du tilføje flere filer senere
};

const swaggerSpec = swaggerJSDoc(swaggerOptions);
app.use('/api-docs', swaggerUi.serve, swaggerUi.setup(swaggerSpec));
```

**3. Tilføj din første Swagger kommentar til en endpoint:** Lige over din `app.get('/', ...)` kan du tilføje denne kommentar:

```jsx
/**
 * @swagger
 * /:
 *   get:
 *     summary: Forside på API'et
 *     description: Returnerer en velkomstbesked.
 *     responses:
 *       200:
 *         description: Succes!
 */
```

> 💡 Opgave: Læs kommentaren og se, hvordan den beskriver endpointet. Prøv evt. at ændre teksten og se, hvad der sker i Swagger UI.

**4. Genstart din server og tilgå Swagger UI:** Genstart din server med:

```bash
node server.js
```

Gå derefter ind på [http://localhost:3000/api-docs](http://localhost:3000/api-docs) i din browser.

> 💡 Opgave: Kan du se din API-dokumentation? Prøv at trykke på "GET /" og brug "Try it out" for at teste dit endpoint direkte fra Swagger!

---

## **Del 3 - Brug af Nodemon til automatisk genstart :repeat:**

Når du udvikler din server, kan det være irriterende hele tiden at skulle stoppe og starte serveren manuelt, hver gang du laver en ændring i koden. Her kommer nodemon til undsætning!

---

**1. Installer nodemon som udviklingsafhængighed:**

```bash
npm install --save-dev nodemon
```

> 💡 Opgave: Hvad betyder det, at vi bruger --save-dev? Hvorfor installerer vi ikke nodemon som en "almindelig" afhængighed?

**2. Tilføj en script-kommando til din `package.json`:** Åbn din `package.json` og find feltet `"scripts"`. Tilføj denne linje (eller ret evt. den eksisterende `start`-linje):

```json
"scripts": {
  "start": "node server.js",
  "dev": "nodemon server.js"
}
```

> 💡 Opgave: Hvad er forskellen på npm start og npm run dev nu?

**3. Brug nodemon til at starte din server:** Kør følgende i terminalen:

```bash
npm run dev
```

Nu vil din server automatisk genstarte, hver gang du gemmer ændringer i dine filer!

> 💡 Opgave: Prøv at lave en ændring i din server.js og se, om nodemon genstarter serveren automatisk.

---

## **Del 4 - CRUD mod en lokal JSON-fil :cd:**

Nu skal vi lave det, der gør vores Spotify-klon spændende: Vi skal kunne tilføje, læse, opdatere og slette musiknumre! Vi gemmer informationen i en lokal JSON-fil, så vi ikke behøver en database.

---

**1. Opret en mappe og en JSON-fil til dine sange:**

```bash
mkdir backend/data
```

Opret filen `songs.json` i mappen `backend/data` med følgende indhold:

```json
[]
```

> 💡 Opgave: Opret mappen og filen, og sørg for at filen starter som et tomt array.

**2. Tilføj endpoints til CRUD i din `server.js`:** Vi bruger Node.js' indbyggede `fs`-modul til at læse og skrive til JSON-filen.

Tilføj øverst i din `server.js`:

```jsx
const fs = require('fs');
const path = require('path');
const songsFile = path.join(__dirname, 'backend/data/songs.json');
```

---

### **a) Læs alle sange (GET)**

```jsx
app.get('/songs', (req, res) => {
  fs.readFile(songsFile, (err, data) => {
    if (err) return res.status(500).send('Fejl ved læsning af sange');
    const songs = JSON.parse(data);
    res.json(songs);
  });
});
```

> 💡 Opgave: Prøv at lave et GET request til /songs (fx via Swagger eller Postman) og se, hvad du får retur!

---

### **b) Tilføj en ny sang (POST)**

```jsx
app.use(express.json());// Husk denne linje, hvis du ikke allerede har den!

app.post('/songs', (req, res) => {
  fs.readFile(songsFile, (err, data) => {
    if (err) return res.status(500).send('Fejl ved læsning af sange');
    const songs = JSON.parse(data);
    const newSong = req.body;
    songs.push(newSong);
    fs.writeFile(songsFile, JSON.stringify(songs, null, 2), err => {
      if (err) return res.status(500).send('Fejl ved gemning af sang');
      res.status(201).json(newSong);
    });
  });
});
```

> 💡 Opgave: Prøv at lave et POST request til /songs med fx titel, kunstner og filnavn på MP3 og cover. Tjek at sangen bliver gemt i din JSON-fil!

---

### **c) Opdater en sang (PUT)**

```jsx
app.put('/songs/:id', (req, res) => {
  fs.readFile(songsFile, (err, data) => {
    if (err) return res.status(500).send('Fejl ved læsning af sange');
    let songs = JSON.parse(data);
    const songId = req.params.id;
    const songIndex = songs.findIndex(s => s.id == songId);
    if (songIndex === -1) return res.status(404).send('Sang ikke fundet');
    songs[songIndex] = { ...songs[songIndex], ...req.body };
    fs.writeFile(songsFile, JSON.stringify(songs, null, 2), err => {
      if (err) return res.status(500).send('Fejl ved opdatering');
      res.json(songs[songIndex]);
    });
  });
});
```

> 💡 Opgave: Prøv at opdatere en sang ved at sende et PUT request til /songs/:id med nye informationer!

---

### **d) Slet en sang (DELETE)**

```jsx
app.delete('/songs/:id', (req, res) => {
  fs.readFile(songsFile, (err, data) => {
    if (err) return res.status(500).send('Fejl ved læsning af sange');
    let songs = JSON.parse(data);
    const songId = req.params.id;
    const newSongs = songs.filter(s => s.id != songId);
    fs.writeFile(songsFile, JSON.stringify(newSongs, null, 2), err => {
      if (err) return res.status(500).send('Fejl ved sletning');
      res.sendStatus(204);
    });
  });
});
```

> 💡 Opgave: Prøv at slette en sang med DELETE og se, om den forsvinder fra din JSON-fil!

## **Samlet `server.js` efter del 3**

```jsx
const express = require('express');
const swaggerUi = require('swagger-ui-express');
const swaggerJSDoc = require('swagger-jsdoc');
const fs = require('fs');
const path = require('path');
const app = express();
const port = 3000;

const songsFile = path.join(__dirname, 'backend/data/songs.json');

class Song {
  constructor({ id, title, artist, mp3, cover }) {
    this.id = id || Date.now().toString();
    this.title = title;
    this.artist = artist;
    this.mp3 = mp3;
    this.cover = cover;
  }
}

const swaggerOptions = {
  definition: {
    openapi: '3.0.0',
    info: {
      title: 'Spotify-klon API',
      version: '1.0.0',
      description: 'API dokumentation for din Spotify-klon',
    },
  },
  apis: ['./server.js'],
};
const swaggerSpec = swaggerJSDoc(swaggerOptions);
app.use('/api-docs', swaggerUi.serve, swaggerUi.setup(swaggerSpec));

app.use(express.json());

/**
 * @swagger
 * /:
 *   get:
 *     summary: Forside på API'et
 *     description: Returnerer en velkomstbesked.
 *     responses:
 *       200:
 *         description: Succes!
 */
app.get('/', (req, res) => {
  res.send('Velkommen til din Spotify-klon!');
});

/**
 * @swagger
 * /songs:
 *   get:
 *     summary: Hent alle sange
 *     responses:
 *       200:
 *         description: En liste af sange
 */
app.get('/songs', (req, res) => {
  fs.readFile(songsFile, (err, data) => {
    if (err) return res.status(500).send('Fejl ved læsning af sange');
    const songs = JSON.parse(data);
    res.json(songs);
  });
});

/**
 * @swagger
 * /songs:
 *   post:
 *     summary: Tilføj en ny sang
 *     requestBody:
 *       required: true
 *       content:
 *         application/json:
 *           schema:
 *             type: object
 *             properties:
 *               title:
 *                 type: string
 *                 description: Sangens titel
 *               artist:
 *                 type: string
 *                 description: Kunstnerens navn
 *               mp3:
 *                 type: string
 *                 description: Filnavn på MP3-filen
 *               cover:
 *                 type: string
 *                 description: Filnavn på cover-billedet
 *             required:
 *               - title
 *               - artist
 *     responses:
 *       201:
 *         description: Sangen blev tilføjet
 *       400:
 *         description: Manglende påkrævede felter
 */
app.post('/songs', (req, res) => {
  const { title, artist, mp3, cover } = req.body;

// Validering af påkrævede felterif (!title || !artist) {
    return res.status(400).json({
      error: 'Manglende påkrævede felter',
      required: ['title', 'artist']
    });
  }

  fs.readFile(songsFile, (err, data) => {
    if (err) return res.status(500).send('Fejl ved læsning af sange');
    const songs = JSON.parse(data);
    const newSong = new Song({ title, artist, mp3, cover });
    songs.push(newSong);
    fs.writeFile(songsFile, JSON.stringify(songs, null, 2), err => {
      if (err) return res.status(500).send('Fejl ved gemning af sang');
      res.status(201).json(newSong);
    });
  });
});

/**
 * @swagger
 * /songs/{id}:
 *   put:
 *     summary: Opdater en sang
 *     parameters:
 *       - in: path
 *         name: id
 *         required: true
 *         schema:
 *           type: string
 *     requestBody:
 *       required: true
 *       content:
 *         application/json:
 *           schema:
 *             type: object
 *     responses:
 *       200:
 *         description: Sangen blev opdateret
 */
app.put('/songs/:id', (req, res) => {
  fs.readFile(songsFile, (err, data) => {
    if (err) return res.status(500).send('Fejl ved læsning af sange');
    let songs = JSON.parse(data);
    const songId = req.params.id;
    const songIndex = songs.findIndex(s => s.id == songId);
    if (songIndex === -1) return res.status(404).send('Sang ikke fundet');
    songs[songIndex] = { ...songs[songIndex], ...req.body };
    songs[songIndex] = new Song(songs[songIndex]);
    fs.writeFile(songsFile, JSON.stringify(songs, null, 2), err => {
      if (err) return res.status(500).send('Fejl ved opdatering');
      res.json(songs[songIndex]);
    });
  });
});

/**
 * @swagger
 * /songs/{id}:
 *   delete:
 *     summary: Slet en sang
 *     parameters:
 *       - in: path
 *         name: id
 *         required: true
 *         schema:
 *           type: string
 *     responses:
 *       204:
 *         description: Sangen blev slettet
 */
app.delete('/songs/:id', (req, res) => {
  fs.readFile(songsFile, (err, data) => {
    if (err) return res.status(500).send('Fejl ved læsning af sange');
    let songs = JSON.parse(data);
    const songId = req.params.id;
    const newSongs = songs.filter(s => s.id != songId);
    fs.writeFile(songsFile, JSON.stringify(newSongs, null, 2), err => {
      if (err) return res.status(500).send('Fejl ved sletning');
      res.sendStatus(204);
    });
  });
});

app.listen(port, () => {
  console.log(`Serveren kører på <http://localhost>:${port}`);
  console.log(`Tilgå Swagger siden på <http://localhost>:${port}/api-docs`);
});

```

---

## **Ekstra: Brug en model til dine sange**

For at gøre din kode mere struktureret, kan du lave en model (klasse) for dine sange. Det gør det nemmere at sikre, at alle sange har samme struktur, og det bliver lettere at udvide senere.

**1. Tilføj en Song-model øverst i din `server.js`:**

```jsx
class Song {
  constructor({ id, title, artist, mp3, cover }) {
    this.id = id || Date.now().toString();
    this.title = title;
    this.artist = artist;
    this.mp3 = mp3;
    this.cover = cover;
  }
}
```

**2. Brug modellen i dine endpoints:** Når du opretter eller opdaterer en sang, skal du bruge modellen:

```jsx
// POST - tilføj en ny sang
app.post('/songs', (req, res) => {
  const { title, artist, mp3, cover } = req.body;

// Validering af påkrævede felterif (!title || !artist) {
    return res.status(400).json({
      error: 'Manglende påkrævede felter',
      required: ['title', 'artist']
    });
  }

  fs.readFile(songsFile, (err, data) => {
    if (err) return res.status(500).send('Fejl ved læsning af sange');
    const songs = JSON.parse(data);
    const newSong = new Song({ title, artist, mp3, cover });
    songs.push(newSong);
    fs.writeFile(songsFile, JSON.stringify(songs, null, 2), err => {
      if (err) return res.status(500).send('Fejl ved gemning af sang');
      res.status(201).json(newSong);
    });
  });
});

// PUT - opdater en sang
app.put('/songs/:id', (req, res) => {
  fs.readFile(songsFile, (err, data) => {
    if (err) return res.status(500).send('Fejl ved læsning af sange');
    let songs = JSON.parse(data);
    const songId = req.params.id;
    const songIndex = songs.findIndex(s => s.id == songId);
    if (songIndex === -1) return res.status(404).send('Sang ikke fundet');
    songs[songIndex] = { ...songs[songIndex], ...req.body };
    songs[songIndex] = new Song(songs[songIndex]);
    fs.writeFile(songsFile, JSON.stringify(songs, null, 2), err => {
      if (err) return res.status(500).send('Fejl ved opdatering');
      res.json(songs[songIndex]);
    });
  });
});
```

> 💡 Fordel: Du sikrer, at alle sange har samme struktur og kan nemt udvide modellen senere, fx med flere felter eller metoder.

# Tirsdag d. 26/08

## 12:00 - 15:15 med MAGS

Vi skal i gang med at kode vores Spotify/mp3-afspiller klon! Vi følger guiden her til at bygge den [Spotify-Klon](https://www.notion.so/Spotify-Klon-225dab5ca237805dae41db54d8133773?pvs=21)

|Tidsplan|Opgave / Teori|
|---|---|
|12:00 - 12:30|Udvikling af backend|
|12:30 - 13:15|JS Spil - Træne JavaScript|
|13:15 - 13:45|Pause|
|13:45 - 14:30|Mere udvikling af backend + sidste JS|
|14:30 - 15:00|Dokumentation og GF1-logbogsopgave|

Lær JS - Vi starter med at se hvor langt vi kan komme i følgende spil! [https://warriorjs.com/](https://warriorjs.com/) Vi tager noget af det på klassen, men prøv så meget I kan selv først! Bliver man færdig eller synes det er for nemt kan man prøve den her som en udfordring! [https://play.elevatorsaga.com/](https://play.elevatorsaga.com/) - Husk at læse dokumentationen [https://play.elevatorsaga.com/documentation.html#docs](https://play.elevatorsaga.com/documentation.html#docs)

Bliver man færdig eller vil prøve et helt andet værktøj, kan jeg anbefale følgende, den har lidt et hacker vibe [https://bitburner-official.github.io/](https://bitburner-official.github.io/)

### Backend snippets

# Udviklingsguide: Fra Simpel Health Check til Fuldt Funktional Musik-API

Denne guide vil tage dig gennem processen med at udvikle din Express.js server fra en simpel health check til en komplet musik-API med Swagger dokumentation, CRUD operationer og fil upload funktionalitet.

## 🚀 Fase 1: Grundlæggende Setup

### Trin 1.1: Installer nødvendige dependencies

```bash
npm install express cors fs path
```

### Trin 1.2: Opret grundlæggende server struktur

```jsx
const express = require("express");
const cors = require("cors");
const fs = require("fs");
const path = require("path");

const app = express();
const PORT = 3001;

app.use(cors());
app.use(express.json());

// Healthcheck endpoint
app.get("/api/health", (req, res) => {
  const songsPath = path.join(__dirname, "data", "songs.json");

  try {
    const fileExists = fs.existsSync(songsPath);

    if (fileExists) {
      const data = fs.readFileSync(songsPath, "utf8");
      JSON.parse(data);

      res.json({
        status: "OK",
        message: "Server kører og songs.json er tilgængelig",
        timestamp: new Date().toISOString(),
        database: "connected"
      });
    } else {
      res.status(503).json({
        status: "ERROR",
        message: "songs.json filen findes ikke",
        timestamp: new Date().toISOString(),
        database: "disconnected"
      });
    }
  } catch (error) {
    res.status(503).json({
      status: "ERROR",
      message: "Fejl ved læsning af songs.json: " + error.message,
      timestamp: new Date().toISOString(),
      database: "error"
    });
  }
});

app.listen(PORT, () => {
  console.log(`Serveren kører på <http://localhost>:${PORT}`);
  console.log(`Health check tilgængelig på: <http://localhost>:${PORT}/api/health`);
});
```

### Trin 1.3: Test din server

```bash
npm run dev
```

Besøg `http://localhost:3001/api/health` for at verificere at serveren kører.

---

## 📚 Fase 2: Implementer Swagger Dokumentation

### Trin 2.1: Installer Swagger dependencies

```bash
npm install swagger-jsdoc swagger-ui-express
```

### Trin 2.2: Tilføj Swagger konfiguration

```jsx
const swaggerJsdoc = require("swagger-jsdoc");
const swaggerUi = require("swagger-ui-express");

// Swagger konfiguration
const swaggerOptions = {
  definition: {
    openapi: "3.0.0",
    info: {
      title: "Musik M-O API",
      version: "1.0.0",
      description: "API til musik-applikation med sange, covers og metadata",
      contact: {
        name: "API Support",
        email: "support@musik-mo.dk"
      }
    },
    servers: [
      {
        url: `http://localhost:${PORT}`,
        description: "Development server"
      }
    ],
    components: {
      schemas: {
        Song: {
          type: "object",
          properties: {
            id: { type: "integer", description: "Unikt ID for sangen" },
            title: { type: "string", description: "Sangens titel" },
            artist: { type: "string", description: "Kunstneren der har lavet sangen" },
            coverPath: { type: "string", description: "Sti til cover billedet" },
            songPath: { type: "string", description: "Sti til sangfilen" },
            createdAt: { type: "string", format: "date-time", description: "Tidspunkt for oprettelse" },
            updatedAt: { type: "string", format: "date-time", description: "Tidspunkt for sidste opdatering" }
          },
          required: ["id", "title", "artist", "coverPath", "songPath"]
        }
      }
    }
  },
  apis: ["./server.js"]
};

const specs = swaggerJsdoc(swaggerOptions);

// Swagger UI
app.use("/api-docs", swaggerUi.serve, swaggerUi.setup(specs));
```

### Trin 2.3: Test Swagger

Besøg `http://localhost:3001/api-docs` for at se din API dokumentation.

---

## 📖 Fase 3: Implementer READ Operationer (GET)

### Trin 3.1: Tilføj hjælpefunktioner

```jsx
// Hjælpefunktioner
function loadSongsFromFile() {
  const songsPath = path.join(__dirname, "data", "songs.json");
  try {
    const data = fs.readFileSync(songsPath, "utf8");
    return JSON.parse(data);
  } catch (error) {
    return [];
  }
}
```

### Trin 3.2: Implementer GET alle sange

```jsx
/**
 * @swagger
 * /api/songs:
 *   get:
 *     summary: Hent alle sange
 *     description: Returnerer alle sange fra databasen med metadata
 *     tags: [Songs]
 *     responses:
 *       200:
 *         description: Liste over alle sange
 *         content:
 *           application/json:
 *             schema:
 *               type: object
 *               properties:
 *                 success: { type: boolean }
 *                 count: { type: integer }
 *                 songs:
 *                   type: array
 *                   items: { $ref: '#/components/schemas/Song' }
 */
app.get("/api/songs", (req, res) => {
  try {
    const songs = loadSongsFromFile();

    res.json({
      success: true,
      count: songs.length,
      songs: songs
    });
  } catch (error) {
    res.status(500).json({
      success: false,
      message: "Fejl ved læsning af sange: " + error.message
    });
  }
});
```

### Trin 3.3: Implementer GET specifik sang

```jsx
/**
 * @swagger
 * /api/songs/{id}:
 *   get:
 *     summary: Hent en specifik sang
 *     description: Returnerer en sang baseret på det angivne ID
 *     tags: [Songs]
 *     parameters:
 *       - in: path
 *         name: id
 *         required: true
 *         description: ID på sangen der skal hentes
 *         schema:
 *           type: integer
 *           minimum: 1
 *     responses:
 *       200:
 *         description: Sang fundet
 *         content:
 *           application/json:
 *             schema:
 *               type: object
 *               properties:
 *                 success: { type: boolean }
 *                 song: { $ref: '#/components/schemas/Song' }
 *       404:
 *         description: Sang ikke fundet
 */
app.get("/api/songs/:id", (req, res) => {
  const songId = parseInt(req.params.id);

  try {
    const songs = loadSongsFromFile();
    const song = songs.find(s => s.id === songId);

    if (song) {
      res.json({
        success: true,
        song: song
      });
    } else {
      res.status(404).json({
        success: false,
        message: `Sang med ID ${songId} blev ikke fundet`
      });
    }
  } catch (error) {
    res.status(500).json({
      success: false,
      message: "Fejl ved læsning af sange: " + error.message
    });
  }
});
```

### Trin 3.4: Test READ operationer

```bash
# Test GET alle sange
curl <http://localhost:3001/api/songs>

# Test GET specifik sang
curl <http://localhost:3001/api/songs/1>
```

---

## ➕ Fase 4: Implementer CREATE Operation (POST)

### Trin 4.1: Installer Multer for fil uploads

```bash
npm install multer
```

### Trin 4.2: Konfigurer Multer

```jsx
const multer = require("multer");

// Multer konfiguration for fil uploads
const storage = multer.diskStorage({
  destination: function (req, file, cb) {
    let uploadPath;
    if (file.fieldname === 'cover') {
      uploadPath = path.join(__dirname, 'covers');
    } else if (file.fieldname === 'song') {
      uploadPath = path.join(__dirname, 'songs');
    }

    // Opret mappe hvis den ikke findes
    if (!fs.existsSync(uploadPath)) {
      fs.mkdirSync(uploadPath, { recursive: true });
    }
    cb(null, uploadPath);
  },
  filename: function (req, file, cb) {
    // Generer unikt filnavn
    const uniqueSuffix = Date.now() + '-' + Math.round(Math.random() * 1E9);
    const ext = path.extname(file.originalname);
    cb(null, file.fieldname + '-' + uniqueSuffix + ext);
  }
});

const upload = multer({
  storage: storage,
  fileFilter: function (req, file, cb) {
    if (file.fieldname === 'cover') {
      if (file.mimetype.startsWith('image/')) {
        cb(null, true);
      } else {
        cb(new Error('Kun billedfiler er tilladt for covers'), false);
      }
    } else if (file.fieldname === 'song') {
      if (file.mimetype.startsWith('audio/')) {
        cb(null, true);
      } else {
        cb(new Error('Kun lydfiler er tilladt for sange'), false);
      }
    } else {
      cb(null, true);
    }
  }
});
```

### Trin 4.3: Implementer POST endpoint

```jsx
/**
 * @swagger
 * /api/songs:
 *   post:
 *     summary: Opret en ny sang
 *     description: Opretter en ny sang med cover og lydfil
 *     tags: [Songs]
 *     requestBody:
 *       required: true
 *       content:
 *         multipart/form-data:
 *           schema:
 *             type: object
 *             properties:
 *               title: { type: string, description: "Sangens titel" }
 *               artist: { type: string, description: "Kunstneren der har lavet sangen" }
 *               cover: { type: string, format: binary, description: "Cover billede" }
 *               song: { type: string, format: binary, description: "Lydfil" }
 *     responses:
 *       201:
 *         description: Sang oprettet succesfuldt
 */
app.post("/api/songs", upload.fields([
  { name: 'cover', maxCount: 1 },
  { name: 'song', maxCount: 1 }
]), (req, res) => {
  try {
    const { title, artist } = req.body;

    if (!title || !artist) {
      return res.status(400).json({
        success: false,
        message: "Titel og kunstner er påkrævet"
      });
    }

    if (!req.files || !req.files.cover || !req.files.song) {
      return res.status(400).json({
        success: false,
        message: "Både cover og lydfil er påkrævet"
      });
    }

    const songs = loadSongsFromFile();
    const newId = songs.length > 0 ? Math.max(...songs.map(s => s.id)) + 1 : 1;
    const now = new Date().toISOString();

    const newSong = {
      id: newId,
      title: title,
      artist: artist,
      coverPath: `/covers/${req.files.cover[0].filename}`,
      songPath: `/songs/${req.files.song[0].filename}`,
      createdAt: now,
      updatedAt: now
    };

    songs.push(newSong);

    // Gem til fil
    const songsPath = path.join(__dirname, "data", "songs.json");
    fs.writeFileSync(songsPath, JSON.stringify(songs, null, 2));

    res.status(201).json({
      success: true,
      song: newSong
    });
  } catch (error) {
    res.status(500).json({
      success: false,
      message: "Fejl ved oprettelse af sang: " + error.message
    });
  }
});
```

### Trin 4.4: Test CREATE operation

```bash
# Test med curl (erstatt filstier med dine egne)
curl -X POST <http://localhost:3001/api/songs> \\\\
  -F "title=Test Sang" \\\\
  -F "artist=Test Kunstner" \\\\
  -F "cover=@/path/to/cover.jpg" \\\\
  -F "song=@/path/to/song.mp3"
```

---

## ✏️ Fase 5: Implementer UPDATE Operation (PUT)

### Trin 5.1: Implementer PUT endpoint

```jsx
/**
 * @swagger
 * /api/songs/{id}:
 *   put:
 *     summary: Opdater en eksisterende sang
 *     description: Opdaterer en sangs metadata og/eller filer
 *     tags: [Songs]
 *     parameters:
 *       - in: path
 *         name: id
 *         required: true
 *         description: ID på sangen der skal opdateres
 *         schema:
 *           type: integer
 *           minimum: 1
 *     requestBody:
 *       required: true
 *       content:
 *         multipart/form-data:
 *           schema:
 *             type: object
 *             properties:
 *               title: { type: string, description: "Sangens titel" }
 *               artist: { type: string, description: "Kunstneren der har lavet sangen" }
 *               cover: { type: string, format: binary, description: "Cover billede (valgfrit)" }
 *               song: { type: string, format: binary, description: "Lydfil (valgfrit)" }
 *     responses:
 *       200:
 *         description: Sang opdateret succesfuldt
 */
app.put("/api/songs/:id", upload.fields([
  { name: 'cover', maxCount: 1 },
  { name: 'song', maxCount: 1 }
]), (req, res) => {
  try {
    const songId = parseInt(req.params.id);
    const { title, artist } = req.body;

    const songs = loadSongsFromFile();
    const songIndex = songs.findIndex(s => s.id === songId);

    if (songIndex === -1) {
      return res.status(404).json({
        success: false,
        message: `Sang med ID ${songId} blev ikke fundet`
      });
    }

    const updatedSong = { ...songs[songIndex] };

    if (title) updatedSong.title = title;
    if (artist) updatedSong.artist = artist;

    // Hvis nye filer er uploadet, opdater stierne
    if (req.files && req.files.cover) {
      // Slet gammel cover fil hvis den findes
      const oldCoverPath = path.join(__dirname, songs[songIndex].coverPath.replace('/covers/', ''));
      if (fs.existsSync(oldCoverPath)) {
        fs.unlinkSync(oldCoverPath);
      }
      updatedSong.coverPath = `/covers/${req.files.cover[0].filename}`;
    }

    if (req.files && req.files.song) {
      // Slet gammel lydfil hvis den findes
      const oldSongPath = path.join(__dirname, songs[songIndex].songPath.replace('/songs/', ''));
      if (fs.existsSync(oldSongPath)) {
        fs.unlinkSync(oldSongPath);
      }
      updatedSong.songPath = `/songs/${req.files.song[0].filename}`;
    }

    updatedSong.updatedAt = new Date().toISOString();

    songs[songIndex] = updatedSong;

    // Gem til fil
    const songsPath = path.join(__dirname, "data", "songs.json");
    fs.writeFileSync(songsPath, JSON.stringify(songs, null, 2));

    res.json({
      success: true,
      song: updatedSong
    });
  } catch (error) {
    res.status(500).json({
      success: false,
      message: "Fejl ved opdatering af sang: " + error.message
    });
  }
});

```

### Trin 5.2: Test UPDATE operation

```bash
# Test med curl
curl -X PUT <http://localhost:3001/api/songs/1> \\\\
  -F "title=Opdateret Titel" \\\\
  -F "artist=Opdateret Kunstner"

```

---

## 🗑️ Fase 6: Implementer DELETE Operation

### Trin 6.1: Implementer DELETE endpoint

```jsx
/**
 * @swagger
 * /api/songs/{id}:
 *   delete:
 *     summary: Slet en sang
 *     description: Sletter en sang og alle tilhørende filer
 *     tags: [Songs]
 *     parameters:
 *       - in: path
 *         name: id
 *         required: true
 *         description: ID på sangen der skal slettes
 *         schema:
 *           type: integer
 *           minimum: 1
 *     responses:
 *       200:
 *         description: Sang slettet succesfuldt
 */
app.delete("/api/songs/:id", (req, res) => {
  try {
    const songId = parseInt(req.params.id);
    const songs = loadSongsFromFile();
    const songIndex = songs.findIndex(s => s.id === songId);

    if (songIndex === -1) {
      return res.status(404).json({
        success: false,
        message: `Sang med ID ${songId} blev ikke fundet`
      });
    }

    const songToDelete = songs[songIndex];

    // Slet filer
    try {
      if (songToDelete.coverPath) {
        const coverPath = path.join(__dirname, songToDelete.coverPath.replace('/covers/', ''));
        if (fs.existsSync(coverPath)) {
          fs.unlinkSync(coverPath);
        }
      }

      if (songToDelete.songPath) {
        const songPath = path.join(__dirname, songToDelete.songPath.replace('/songs/', ''));
        if (fs.existsSync(songPath)) {
          fs.unlinkSync(songPath);
        }
      }
    } catch (fileError) {
      console.warn(`Kunne ikke slette filer for sang ${songId}:`, fileError.message);
    }

    // Fjern sang fra array
    songs.splice(songIndex, 1);

    // Gem til fil
    const songsPath = path.join(__dirname, "data", "songs.json");
    fs.writeFileSync(songsPath, JSON.stringify(songs, null, 2));

    res.json({
      success: true,
      message: `Sang "${songToDelete.title}" blev slettet succesfuldt`
    });
  } catch (error) {
    res.status(500).json({
      success: false,
      message: "Fejl ved sletning af sang: " + error.message
    });
  }
});

```

### Trin 6.2: Test DELETE operation

```bash
# Test med curl
curl -X DELETE <http://localhost:3001/api/songs/1>

```

---

## 🎯 Fase 7: Tilføj Statiske Filer og Error Handling

### Trin 7.1: Tilføj statiske filer

```jsx
// Statiske filer til frontend
app.use('/covers', express.static(path.join(__dirname, 'covers')));
app.use('/songs', express.static(path.join(__dirname, 'songs')));
app.use('/', express.static(path.join(__dirname, '../frontend')));
```

### Trin 7.2: Implementer error handling middleware

```jsx
// Error handling middleware
app.use((error, req, res, next) => {
  if (error instanceof multer.MulterError) {
    if (error.code === 'LIMIT_FILE_SIZE') {
      return res.status(400).json({
        success: false,
        message: 'Filen er for stor'
      });
    }
  }

  if (error.message.includes('Kun billedfiler er tilladt')) {
    return res.status(400).json({
      success: false,
      message: 'Kun billedfiler er tilladt for covers'
    });
  }

  if (error.message.includes('Kun lydfiler er tilladt')) {
    return res.status(400).json({
      success: false,
      message: 'Kun lydfiler er tilladt for sange'
    });
  }

  console.error('Server fejl:', error);
  res.status(500).json({
    success: false,
    message: 'Intern server fejl'
  });
});

```

### Trin 7.3: Opdater server startup besked

```jsx
app.listen(PORT, () => {
  console.log(`Serveren kører på <http://localhost>:${PORT}`);
  console.log(`Health check tilgængelig på: <http://localhost>:${PORT}/api/health`);
  console.log(`Alle sange tilgængelige på: <http://localhost>:${PORT}/api/songs`);
  console.log(`Specifik sang tilgængelig på: <http://localhost>:${PORT}/api/songs/:id`);
  console.log(`Swagger dokumentation tilgængelig på: <http://localhost>:${PORT}/api-docs`);
  console.log(`Frontend tilgængelig på: <http://localhost>:${PORT}`);
});

```

---

## 🧪 Fase 8: Test Din Komplette API

### Trin 8.1: Test alle endpoints

```bash
# 1. Health check
curl <http://localhost:3001/api/health>

# 2. Opret en sang
curl -X POST <http://localhost:3001/api/songs> \\\\
  -F "title=Min Første Sang" \\\\
  -F "artist=Min Kunstner" \\\\
  -F "cover=@/path/to/cover.jpg" \\\\
  -F "song=@/path/to/song.mp3"

# 3. Hent alle sange
curl <http://localhost:3001/api/songs>

# 4. Hent specifik sang
curl <http://localhost:3001/api/songs/1>

# 5. Opdater sang
curl -X PUT <http://localhost:3001/api/songs/1> \\\\
  -F "title=Opdateret Titel"

# 6. Slet sang
curl -X DELETE <http://localhost:3001/api/songs/1>
```

### Trin 8.2: Verificer Swagger dokumentation

Besøg `http://localhost:3001/api-docs` og test alle endpoints direkte fra Swagger UI.

---

## 🎉 Fase 9: Afslutning og Næste Skridt

### Trin 9.1: Verificer din komplette API

Du har nu en fuldt funktionel musik-API med:

- ✅ Health check endpoint
- ✅ Swagger dokumentation
- ✅ CRUD operationer (Create, Read, Update, Delete)
- ✅ Fil upload funktionalitet
- ✅ Error handling
- ✅ Statiske filer

### Trin 9.2: Næste skridt for at forbedre API'en

1. **Validering**: Tilføj mere robust input validering
2. **Authentication**: Implementer bruger authentication
3. **Rate limiting**: Tilføj rate limiting for API calls
4. **Logging**: Implementer omfattende logging
5. **Testing**: Skriv unit tests og integration tests
6. **Database**: Migrer fra fil-baseret til en rigtig database
7. **Caching**: Implementer caching for bedre performance

### Trin 9.3: Kør din komplette server

```bash
npm install
npm start
```

---

## 🔧 Fejlfinding

### Almindelige problemer og løsninger:

1. **Port allerede i brug**: Skift PORT til 3002 eller brug `npx kill-port 3001`
2. **Filer kan ikke uploades**: Tjek at mapperne `covers/` og `songs/` eksisterer
3. **CORS fejl**: Verificer at `cors()` middleware er aktiveret
4. **Swagger viser ikke endpoints**: Tjek at alle JSDoc kommentarer er korrekt formateret

---

## 📚 Yderligere Ressourcer

- [Express.js Dokumentation](https://expressjs.com/)
- [Multer Dokumentation](https://github.com/expressjs/multer)
- [Swagger/OpenAPI Specifikation](https://swagger.io/specification/)
- [Node.js File System](https://nodejs.org/api/fs.html)

---

**Tillykke!** Du har nu bygget en komplet musik-API fra bunden. 🎵

# Onsdag d. 27/08

# Mandag d. 01/09

## 12:00 - 15:15 med MAGS

Vi starter med at tage et kig på [[CSS Spil]], som vi bruger noget tid på i dag og imorgen!

Derudover skal vi etablere koden som er herunder!

### Frontend - del 1

### Trin-for-trin guide: Hent, afspil og upload sange via jeres API

Nedenfor bygger vi ovenpå den eksisterende backend og frontend. Vi:

- henter og viser sange fra API’et
- afspiller valgte sange (play/pause, tid, scrub/seek)
- uploader nye sange med cover (multipart/form-data)
- filtrerer via søgning

Bemærk: Backend returnerer relative stier (`/covers/...`, `/songs/...`). Frontend præfixer med `API_URL`.

---

## Forberedelse

- Start backend:

```powershell
# fra projektrod
backend/ npm run dev
# Swagger: <http://localhost:3001/api-docs>
# Health:  <http://localhost:3001/api/health>
```

- Åbn `frontend/index.html` i din browser (eller servér den via en simpel static server).

---

### Trin 1 · **OBLIGATORISK**: Gør backend klar til at serve filer (covers og sange)

**Dette trin er allerede implementeret i koden**, men hvis du starter fra en ren backend, skal du tilføje statiske ruter i `backend/server.js` (lige efter `app.use(express.json());`):

```jsx
// Servér statiske filer (covers og sange)
app.use('/covers', express.static(path.join(__dirname, 'covers')));
app.use('/songs', express.static(path.join(__dirname, 'songs')));

```

**Test**: Åbn `http://localhost:3001/covers/howitsdone.png` i browseren - den skal vise billedet (ikke 404).

---

### Trin 2 · Ret upload-feltets navn i HTML

Backend forventer feltet `song` (ikke `file`). Opdater i `frontend/index.html`:

```html
<form id="upload-form" enctype="multipart/form-data">
  <h2>Tilføj ny sang</h2>
  <input type="text" name="youtube" id="youtube-link" placeholder="YouTube-link (valgfrit)" />
  <input type="hidden" name="thumbnail_url" id="youtube-thumbnail-url" />
  <div id="youtube-preview" class="hidden" style="margin-bottom: 1em;"></div>

  <input type="text" name="title" placeholder="Titel" required />
  <input type="text" name="artist" placeholder="Kunstner" required />

  <!-- VIGTIGT: name="song" -->
  <input type="file" name="song" accept="audio/*" required />
  <input type="file" name="cover" accept="image/*" required />

  <button type="submit">Upload sang</button>
  </form>

```

---

### Trin 3 · Hent sange fra API og vis dem i UI

I `frontend/script.js` har I allerede `API_URL` og loader/error-håndtering.

1. Tilføj referencer og state øverst i filen (under de eksisterende `const`):

```jsx
const audio = document.getElementById("audio");
const playPauseBtn = document.getElementById("play-pause");
const playIcon = document.getElementById("play-icon");
const pauseIcon = document.getElementById("pause-icon");
const seekBar = document.getElementById("seek-bar");
const currentTimeEl = document.getElementById("current-time");
const durationEl = document.getElementById("duration");
const playerCover = document.getElementById("player-cover");
const playerInfo = document.getElementById("player-info");
const searchInput = document.getElementById("search");
const uploadForm = document.getElementById("upload-form");

let songs = [];
let currentIndex = -1;

```

1. Tilføj funktioner til at hente/rendere:

```jsx
async function loadSongs() {
  setLoader(true);
  setError(null);
  try {
    const res = await fetch(`${API_URL}/api/songs`);
    if (!res.ok) throw new Error("Kunne ikke hente sange");
    const data = await res.json();
    songs = data.songs || [];
    renderSongs(songs);
  } catch (e) {
    setError(e.message);
  } finally {
    setLoader(false);
  }
}

function renderSongs(list) {
  if (!Array.isArray(list) || list.length === 0) {
    songListElem.innerHTML = `<p style="padding:1rem;color:#b3b3b3;">Ingen sange endnu. Prøv at uploade en.</p>`;
    return;
  }

  songListElem.innerHTML = list
    .map((s, i) => `
      <article class="song-card">
        <button class="play" data-index="${i}" title="Afspil">
          <img src="${API_URL}${s.coverPath}" alt="Cover: ${s.title}" />
        </button>
        <h3>${s.title}</h3>
        <p>${s.artist}</p>
      </article>
    `)
    .join("");

  songListElem.querySelectorAll(".play").forEach(btn => {
    btn.addEventListener("click", (e) => {
      const idx = Number(e.currentTarget.dataset.index);
      playSong(idx);
    });
  });
}

```

1. Kobl `loadSongs()` ind efter vellykket health-check.

---

### Trin 4 · Afspiller: play/pause, tid, seekbar

```jsx
function playSong(index) {
  currentIndex = index;
  const s = songs[index];
  if (!s) return;

  audio.src = `${API_URL}${s.songPath}`;
  playerCover.src = `${API_URL}${s.coverPath}`;
  playerCover.style.display = "block";
  playerInfo.textContent = `${s.title} — ${s.artist}`;

  audio.play();
}

function togglePlay() {
  if (audio.paused) {
    audio.play();
  } else {
    audio.pause();
  }
}

playPauseBtn.addEventListener("click", togglePlay);

audio.addEventListener("play", () => {
  playIcon.style.display = "none";
  pauseIcon.style.display = "block";
});
audio.addEventListener("pause", () => {
  playIcon.style.display = "block";
  pauseIcon.style.display = "none";
});

audio.addEventListener("loadedmetadata", () => {
  durationEl.textContent = formatTime(audio.duration);
  seekBar.max = Math.floor(audio.duration || 0);
});

audio.addEventListener("timeupdate", () => {
  currentTimeEl.textContent = formatTime(audio.currentTime);
  if (!seekBar.matches(":active")) {
    seekBar.value = Math.floor(audio.currentTime || 0);
  }
});

audio.addEventListener("ended", () => {
  if (currentIndex < songs.length - 1) {
    playSong(currentIndex + 1);
  }
});

seekBar.addEventListener("input", () => {
  audio.currentTime = Number(seekBar.value || 0);
});

function formatTime(sec) {
  const s = Math.floor(sec || 0);
  const m = Math.floor(s / 60);
  const r = s % 60;
  return `${m}:${r.toString().padStart(2, "0")}`;
}

```

---

# Tirsdag d. 02/09

## 12:00 - 15:15 med MAGS

Vi starter med at tage et kig på [[CSS Spil]], som vi bruger noget tid på i dag og imorgen!

Derudover skal vi etablere koden som er herunder!

### Frontend- del 2

### Trin 5 · Upload ny sang (multipart/form-data)

```jsx
if (uploadForm) {
  uploadForm.addEventListener("submit", async (e) => {
    e.preventDefault();

    const fd = new FormData(uploadForm);
    // Robust mod ældre HTML: konverter "file" → "song"
    if (fd.has("file")) {
      const file = fd.get("file");
      fd.delete("file");
      fd.append("song", file);
    }

    try {
      setLoader(true);
      setError(null);

      const res = await fetch(`${API_URL}/api/songs`, {
        method: "POST",
        body: fd,
      });

      if (!res.ok) {
        const errText = await res.text().catch(() => "");
        throw new Error(`Upload fejlede: ${errText || res.status}`);
      }

      const data = await res.json();
      if (!data?.success || !data?.song) throw new Error("Uventet svar fra server");

      songs = [data.song, ...songs];
      renderSongs(songs);

      uploadForm.reset();
      uploadModal.classList.add("hidden");
    } catch (err) {
      setError(err.message);
    } finally {
      setLoader(false);
    }
  });
}

```

---

### Trin 6 · Søgning

```jsx
if (searchInput) {
  searchInput.addEventListener("input", () => {
    const q = searchInput.value.trim().toLowerCase();
    const filtered = songs.filter(s =>
      s.title.toLowerCase().includes(q) || s.artist.toLowerCase().includes(q)
    );
    renderSongs(filtered);
  });
}

```

---

### Trin 7 · Hook op efter health-check

Når `checkHealth()` melder OK, kaldes `loadSongs()` for at hente listen.

---

### Testcheckliste

### **Backend Test:**

- ✅ Åbn `http://localhost:3001/api/health` → status OK
- ✅ Åbn `http://localhost:3001/api-docs` → Swagger UI vises
- ✅ Test `http://localhost:3001/covers/howitsdone.png` → billede vises (ikke 404)

### **Frontend Design Test:**

- ✅ Frontend loader sange → **Spotify-lignende kort** vises i responsive grid
- ✅ Hover over kort → **løfter sig, får glow, play-knap vises**
- ✅ Covers viser korrekt med **gradient baggrund** og **afrundede hjørner**

### **Funktionalitet Test:**

- ✅ Klik på kort → afspilning starter, **kort får grøn aktiv-kant**
- ✅ Play/pause virker, tider opdateres, slider kan søge
- ✅ Upload ny sang + cover → vises som **nyt kort i grid**, kan afspilles
- ✅ Søg efter titel/kunstner → listen filtreres med **smooth animationer**

### **Responsive Test:**

- ✅ Desktop: Store kort i grid (200px+)
- ✅ Tablet: Mellemstore kort (160px+)
- ✅ Mobil: Kompakte kort (140px+)

---

### Trin 8 · **IMPLEMENTERET**: Spotify-lignende Card Design

**Dette er allerede implementeret i CSS'en**, men her er hvad der er tilføjet:

### **Visuelle Features:**

- **Grid Layout**: Responsive grid der tilpasser sig skærmstørrelse
- **Gradient Baggrund**: Flotte gradienter på `.song-card` elementer
- **Hover Effekter**: Kort løfter sig (`translateY(-8px)`) og får grøn glow
- **Play Button Overlay**: Grøn play-knap (▶) vises på hover over covers
- **Aktiv Status**: Det kort der spiller fremhæves med `.active` klasse

### **CSS Klasser:**

```css
.song-card              /* Hovedkort med gradient baggrund */
.song-card:hover        /* Hover effekter med løft og glow */
.song-card.active       /* Aktiv sang med grøn kant */
.song-card .play        /* Cover billede som knap */
.song-card .play::after /* Play-knap overlay */

```

### **JavaScript Integration:**

- `playSong()` funktionen tilføjer automatisk `.active` klasse
- Fjerner `.active` fra andre kort når ny sang starter
- Responsive grid: 200px → 160px → 140px på mindre skærme

---

### Bonus (valgfrit)

- Næste/Forrige-knapper i afspilleren
    
- ✅ **Implementeret**: Highlight aktiv sang i listen (`.active` klasse)
    
- Gem sidste afspillede sang i `localStorage`
    
- Upload-progress (XHR onprogress eller Fetch stream)
    
- Volume kontrol slider
    
- Shuffle og repeat funktioner
    
- Komplette filer for frontend!
    
    CSS filen - Style.css
    
    ```css
    body {
      margin: 0;
      font-family: "Segoe UI", Arial, sans-serif;
      background: #181818;
      color: #fff;
      scrollbar-width: none;
      -ms-overflow-style: none;
    }
    header {
      background: #121212;
      padding: 1rem 2rem;
      text-align: center;
      border-bottom: 1px solid #222;
      display: flex;
      flex-direction: column;
      align-items: center;
      gap: 1rem;
    }
    #search {
      width: 100%;
      max-width: 350px;
      padding: 0.6rem 1rem;
      border-radius: 20px;
      border: none;
      background: #232323;
      color: #fff;
      font-size: 1rem;
      outline: none;
      margin-top: 0.5rem;
      transition: box-shadow 0.2s;
    }
    #search:focus {
      box-shadow: 0 0 0 2px #1db95455;
    }
    main {
      padding: 2rem;
      min-height: 60vh;
      padding-bottom: 100px;
      scrollbar-width: none;
      -ms-overflow-style: none;
    }
    #loader {
      border: 4px solid #232323;
      border-top: 4px solid #1db954;
      border-radius: 50%;
      width: 40px;
      height: 40px;
      margin: 2rem auto;
      animation: spin 1s linear infinite;
    }
    @keyframes spin {
      0% {
        transform: rotate(0deg);
      }
      100% {
        transform: rotate(360deg);
      }
    }
    .hidden {
      display: none !important;
    }
    #error {
      color: #ff4c4c;
      background: #2a1818;
      border: 1px solid #ff4c4c;
      border-radius: 8px;
      padding: 1rem;
      margin: 1rem auto;
      max-width: 400px;
      text-align: center;
    }
    #song-list {
      display: grid;
      grid-template-columns: repeat(auto-fill, minmax(200px, 1fr));
      gap: 1.5rem;
      padding: 0 1rem;
      max-width: 1400px;
      margin: 0 auto;
    }
    
    /* Spotify-lignende kort design */
    .song-card {
      background: linear-gradient(145deg, #1a1a1a, #2a2a2a);
      border-radius: 12px;
      padding: 1.25rem;
      transition: all 0.3s cubic-bezier(0.25, 0.46, 0.45, 0.94);
      cursor: pointer;
      position: relative;
      overflow: hidden;
      border: 1px solid transparent;
      box-shadow: 
        0 4px 12px rgba(0, 0, 0, 0.4),
        0 2px 4px rgba(0, 0, 0, 0.3);
    }
    
    .song-card::before {
      content: '';
      position: absolute;
      top: 0;
      left: 0;
      right: 0;
      bottom: 0;
      background: linear-gradient(145deg, rgba(29, 185, 84, 0.1), transparent);
      opacity: 0;
      transition: opacity 0.3s ease;
      pointer-events: none;
    }
    
    .song-card:hover {
      transform: translateY(-8px);
      background: linear-gradient(145deg, #252525, #353535);
      box-shadow: 
        0 12px 24px rgba(0, 0, 0, 0.6),
        0 6px 12px rgba(0, 0, 0, 0.4),
        0 0 0 1px rgba(29, 185, 84, 0.3);
    }
    
    .song-card:hover::before {
      opacity: 1;
    }
    
    .song-card.active {
      border: 1px solid #1db954;
      background: linear-gradient(145deg, #1a2b1a, #2a3b2a);
      box-shadow: 
        0 8px 20px rgba(29, 185, 84, 0.4),
        0 4px 8px rgba(0, 0, 0, 0.3);
    }
    
    .song-card .play {
      background: none;
      border: none;
      padding: 0;
      width: 100%;
      cursor: pointer;
      position: relative;
      display: block;
      overflow: hidden;
      border-radius: 8px;
      margin-bottom: 1rem;
      transition: all 0.3s ease;
    }
    
    .song-card .play::after {
      content: '▶';
      position: absolute;
      top: 50%;
      left: 50%;
      transform: translate(-50%, -50%);
      background: rgba(29, 185, 84, 0.9);
      color: white;
      width: 50px;
      height: 50px;
      border-radius: 50%;
      display: flex;
      align-items: center;
      justify-content: center;
      font-size: 16px;
      opacity: 0;
      transition: all 0.3s ease;
      backdrop-filter: blur(4px);
      box-shadow: 0 4px 12px rgba(0, 0, 0, 0.5);
    }
    
    .song-card:hover .play::after {
      opacity: 1;
      transform: translate(-50%, -50%) scale(1.1);
    }
    
    .song-card .play img {
      width: 100%;
      height: 180px;
      object-fit: cover;
      border-radius: 8px;
      transition: all 0.3s ease;
      box-shadow: 0 4px 12px rgba(0, 0, 0, 0.3);
    }
    
    .song-card:hover .play img {
      transform: scale(1.05);
      filter: brightness(0.7);
    }
    
    .song-card h3 {
      font-size: 1.1rem;
      font-weight: 600;
      margin: 0 0 0.5rem 0;
      color: #fff;
      line-height: 1.3;
      overflow: hidden;
      text-overflow: ellipsis;
      white-space: nowrap;
      letter-spacing: 0.01em;
    }
    
    .song-card p {
      color: #b3b3b3;
      margin: 0;
      font-size: 0.9rem;
      font-weight: 400;
      overflow: hidden;
      text-overflow: ellipsis;
      white-space: nowrap;
      transition: color 0.3s ease;
    }
    
    .song-card:hover p {
      color: #fff;
    }
    
    /* Responsive grid */
    @media (max-width: 768px) {
      #song-list {
        grid-template-columns: repeat(auto-fill, minmax(160px, 1fr));
        gap: 1rem;
        padding: 0 0.5rem;
      }
      
      .song-card {
        padding: 1rem;
      }
      
      .song-card .play img {
        height: 140px;
      }
      
      .song-card h3 {
        font-size: 1rem;
      }
      
      .song-card p {
        font-size: 0.85rem;
      }
    }
    
    @media (max-width: 480px) {
      #song-list {
        grid-template-columns: repeat(auto-fill, minmax(140px, 1fr));
        gap: 0.75rem;
      }
      
      .song-card .play img {
        height: 120px;
      }
    }
    #player {
      position: fixed;
      left: 0;
      right: 0;
      bottom: 0;
      background: #181818;
      border-top: 1px solid #222;
      display: flex;
      align-items: center;
      padding: 0.7rem 2rem;
      gap: 1rem;
      min-height: 70px;
      z-index: 10;
      box-shadow: 0 -2px 12px #0007;
    }
    #player-cover {
      width: 50px;
      height: 50px;
      object-fit: cover;
      border-radius: 6px;
      display: block;
      box-shadow: 0 2px 8px #0005;
    }
    #player-info {
      flex: 1;
      font-size: 1.1rem;
      font-weight: 500;
      color: #fff;
      text-shadow: 0 1px 2px #000a;
    }
    #play-pause {
      background: #1db954;
      color: #fff;
      border: none;
      border-radius: 50%;
      width: 48px;
      height: 48px;
      font-size: 1.2rem;
      cursor: pointer;
      transition: background 0.2s, box-shadow 0.2s;
      display: flex;
      align-items: center;
      justify-content: center;
      box-shadow: 0 2px 8px #0005;
    }
    #play-pause:hover {
      background: #1ed760;
      box-shadow: 0 4px 16px #0007;
    }
    #play-icon,
    #pause-icon {
      pointer-events: none;
    }
    #seek-bar {
      flex: 2;
      margin: 0 1rem;
      accent-color: #1db954;
      height: 4px;
      background: linear-gradient(90deg, #1db954 0%, #1ed760 100%);
      border-radius: 2px;
    }
    #current-time,
    #duration {
      min-width: 48px;
      text-align: center;
      font-variant-numeric: tabular-nums;
      color: #b3b3b3;
    }
    @media (max-width: 700px) {
      main {
        padding: 1rem 0.2rem;
      }
      #player {
        flex-direction: column;
        align-items: stretch;
        gap: 0.5rem;
        padding: 0.7rem 0.5rem;
      }
      #player-cover {
        margin: 0 auto;
      }
      #player-info {
        text-align: center;
      }
      #seek-bar {
        margin: 0 0.2rem;
      }
    }
    #upload-form {
      background: #232323;
      border-radius: 12px;
      box-shadow: 0 2px 12px #0005;
      padding: 1.5rem 2rem;
      max-width: 400px;
      margin: 2rem auto 2.5rem auto;
      display: flex;
      flex-direction: column;
      gap: 1rem;
      align-items: stretch;
    }
    #upload-form h2 {
      margin: 0 0 0.5rem 0;
      color: #1db954;
      font-size: 1.3rem;
      text-align: center;
    }
    #upload-form input[type="text"],
    #upload-form input[type="number"] {
      padding: 0.7rem 1rem;
      border-radius: 20px;
      border: none;
      background: #181818;
      color: #fff;
      font-size: 1rem;
      outline: none;
    }
    #upload-form input[type="file"] {
      background: #181818;
      color: #b3b3b3;
      border-radius: 20px;
      padding: 0.5rem 0.5rem;
      font-size: 1rem;
    }
    #upload-form button[type="submit"] {
      background: #1db954;
      color: #fff;
      border: none;
      border-radius: 20px;
      padding: 0.8rem 0;
      font-size: 1.1rem;
      font-weight: bold;
      cursor: pointer;
      margin-top: 0.5rem;
      transition: background 0.2s;
    }
    #upload-form button[type="submit"]:hover {
      background: #1ed760;
    }
    #open-upload-modal {
      background: #1db954;
      color: #fff;
      border: none;
      border-radius: 20px;
      padding: 0.7rem 1.5rem;
      font-size: 1rem;
      font-weight: bold;
      cursor: pointer;
      margin-top: 0.5rem;
      margin-left: 0.5rem;
      transition: background 0.2s, box-shadow 0.2s, transform 0.1s;
      box-shadow: 0 2px 8px #0005;
      display: flex;
      align-items: center;
      gap: 0.5rem;
    }
    #open-upload-modal:hover {
      background: #1ed760;
      box-shadow: 0 4px 16px #0007;
      transform: scale(1.04);
    }
    #open-upload-modal svg {
      width: 20px;
      height: 20px;
      fill: currentColor;
    }
    @media (max-width: 700px) {
      #open-upload-modal {
        width: 100%;
        justify-content: center;
        margin-left: 0;
      }
    }
    .modal {
      display: flex;
      position: fixed;
      z-index: 1000;
      left: 0;
      top: 0;
      right: 0;
      bottom: 0;
      background: rgba(0, 0, 0, 0.7);
      align-items: center;
      justify-content: center;
      transition: opacity 0.2s;
    }
    .modal.hidden {
      display: none !important;
    }
    .modal-content {
      background: #232323;
      border-radius: 16px;
      box-shadow: 0 8px 32px #000a;
      padding: 2rem 2.5rem 2rem 2.5rem;
      position: relative;
      min-width: 320px;
      max-width: 95vw;
      max-height: 90vh;
      overflow-y: auto;
      animation: modalPopIn 0.2s;
    }
    @keyframes modalPopIn {
      from {
        transform: scale(0.95);
        opacity: 0;
      }
      to {
        transform: scale(1);
        opacity: 1;
      }
    }
    .close {
      position: absolute;
      right: 1rem;
      top: 1rem;
      font-size: 2rem;
      color: #fff;
      cursor: pointer;
      user-select: none;
      transition: color 0.2s;
    }
    .close:hover {
      color: #1db954;
    }
    /* Skjul scrollbar i Chrome, Edge og Safari */
    body::-webkit-scrollbar,
    main::-webkit-scrollbar {
      width: 0px;
      background: transparent;
      display: none;
    }
    
    /* Skjul scrollbar i Firefox */
    body, main {
      scrollbar-width: none; /* Firefox */
      -ms-overflow-style: none; /* IE og Edge */
    }
    
    html {
      overflow: -moz-scrollbars-none;
      scrollbar-width: none;
    }
    html::-webkit-scrollbar {
      width: 0px;
      display: none;
    }
    
    ```
    
    HTML filen - Index.html
    
    ```html
    <!DOCTYPE html>
    <html lang="da">
      <head>
        <meta charset="UTF-8" />
        <meta name="viewport" content="width=device-width, initial-scale=1.0" />
        <title>Spotify-klon</title>
        <link rel="stylesheet" href="style.css" />
      </head>
      <body>
        <header>
          <h1>Spotify-klon</h1>
          <input
            type="text"
            id="search"
            placeholder="Søg efter sang eller kunstner..."
          />
          <button id="open-upload-modal" type="button">
            <svg
              viewBox="0 0 24 24"
              width="20"
              height="20"
              style="vertical-align: middle; margin-right: 0.5em"
            >
              <path
                d="M12 5v14m7-7H5"
                stroke="currentColor"
                stroke-width="2"
                fill="none"
                stroke-linecap="round"
              />
            </svg>
            Tilføj sang
          </button>
        </header>
        <main>
          <div id="loader" class="hidden"></div>
          <div id="error" class="hidden"></div>
          <section id="song-list"></section>
        </main>
        <footer id="player">
          <img id="player-cover" src="" alt="Cover" style="display: none" />
          <div id="player-info"></div>
          <span id="current-time">0:00</span>
          <input type="range" id="seek-bar" value="0" min="0" max="100" step="1" />
          <span id="duration">0:00</span>
          <button id="play-pause" aria-label="Afspil/Pause">
            <svg id="play-icon" viewBox="0 0 24 24" width="28" height="28">
              <polygon points="6,4 20,12 6,20" fill="currentColor" />
            </svg>
            <svg
              id="pause-icon"
              viewBox="0 0 24 24"
              width="28"
              height="28"
              style="display: none"
            >
              <rect x="6" y="4" width="4" height="16" fill="currentColor" />
              <rect x="14" y="4" width="4" height="16" fill="currentColor" />
            </svg>
          </button>
          <audio id="audio" src="" preload="metadata"></audio>
        </footer>
        <div id="upload-modal" class="modal hidden">
          <div class="modal-content">
            <span id="close-upload-modal" class="close">&times;</span>
            <form id="upload-form" enctype="multipart/form-data">
              <h2>Tilføj ny sang</h2>
              <input type="text" name="youtube" id="youtube-link" placeholder="YouTube-link (valgfrit)" style="" />
              <input type="hidden" name="thumbnail_url" id="youtube-thumbnail-url" />
              <div id="youtube-preview" class="hidden" style="margin-bottom: 1em;"></div>
              <input type="text" name="title" placeholder="Titel" required />
              <input type="text" name="artist" placeholder="Kunstner" required />
              <input
                type="file"
                name="song"
                accept="audio/mp3,audio/mpeg"
                required
              />
              <input type="file" name="cover" accept="image/*" required />
              <button type="submit">Upload sang</button>
            </form>
          </div>
        </div>
        <script src="script.js"></script>
      </body>
    </html>
    
    ```
    
    Javascript filen - script.js
    
    ```jsx
    const API_URL = "<http://localhost:3001>";
    
    const songListElem = document.getElementById("song-list");
    const loader = document.getElementById("loader");
    const errorElem = document.getElementById("error");
    const openUploadModalBtn = document.getElementById("open-upload-modal");
    const uploadModal = document.getElementById("upload-modal");
    const closeUploadModalBtn = document.getElementById("close-upload-modal");
    const audio = document.getElementById("audio");
    const playPauseBtn = document.getElementById("play-pause");
    const playIcon = document.getElementById("play-icon");
    const pauseIcon = document.getElementById("pause-icon");
    const seekBar = document.getElementById("seek-bar");
    const currentTimeEl = document.getElementById("current-time");
    const durationEl = document.getElementById("duration");
    const playerCover = document.getElementById("player-cover");
    const playerInfo = document.getElementById("player-info");
    const searchInput = document.getElementById("search");
    const uploadForm = document.getElementById("upload-form");
    
    let songs = [];
    let currentIndex = -1;
    
    function setLoader(visible) {
      loader.classList.toggle("hidden", !visible);
    }
    
    function setError(msg) {
      if (msg) {
        errorElem.textContent = msg;
        errorElem.classList.remove("hidden");
      } else {
        errorElem.classList.add("hidden");
      }
    }
    
    // Funktion til at tjekke server status
    function checkHealth() {
      setLoader(true);
      setError(null);
      
      fetch(`${API_URL}/api/health`)
        .then((res) => {
          if (!res.ok) throw new Error("Server er ikke tilgængelig");
          return res.json();
        })
        .then((data) => {
          if (data.status === "OK") {
            songListElem.innerHTML = `
              <div style="text-align: center; padding: 2rem; color: #1db954;">
                <h2>✅ Server Status: ${data.status}</h2>
                <p>${data.message}</p>
                <p><strong>Database:</strong> ${data.database}</p>
                <p><small>Sidste tjek: ${new Date(data.timestamp).toLocaleString('da-DK')}</small></p>
                <p style="margin-top: 2rem; color: #b3b3b3;">
                  Din Spotify-klon starter template er klar til udvikling!
                </p>
              </div>
            `;
            // Efter OK health → hent sange
            loadSongs();
          } else {
            setError(`Server fejl: ${data.message}`);
          }
        })
        .catch((err) => {
          setError("Kunne ikke forbinde til serveren: " + err.message);
          songListElem.innerHTML = `
            <div style="text-align: center; padding: 2rem; color: #ff4c4c;">
              <h2>❌ Server ikke tilgængelig</h2>
              <p>Sørg for at backend serveren kører på <http://localhost:3001></p>
            </div>
          `;
        })
        .finally(() => setLoader(false));
    }
    
    // Kør health check når siden loader
    checkHealth();
    
    // Modal funktionalitet (behold designet)
    if (openUploadModalBtn && uploadModal && closeUploadModalBtn) {
      openUploadModalBtn.onclick = () => {
        uploadModal.classList.remove("hidden");
      };
      closeUploadModalBtn.onclick = () => {
        uploadModal.classList.add("hidden");
      };
      // Luk modal hvis man klikker udenfor modal-content
      uploadModal.onclick = (e) => {
        if (e.target === uploadModal) {
          uploadModal.classList.add("hidden");
        }
      };
    }
    
    // Hent og render sange
    async function loadSongs() {
      setLoader(true);
      setError(null);
      try {
        const res = await fetch(`${API_URL}/api/songs`);
        if (!res.ok) throw new Error("Kunne ikke hente sange");
        const data = await res.json();
        songs = data.songs || [];
        renderSongs(songs);
      } catch (e) {
        setError(e.message);
      } finally {
        setLoader(false);
      }
    }
    
    function renderSongs(list) {
      if (!Array.isArray(list) || list.length === 0) {
        songListElem.innerHTML = `<p style="padding:1rem;color:#b3b3b3;">Ingen sange endnu. Prøv at uploade en.</p>`;
        return;
      }
    
      songListElem.innerHTML = list
        .map((s, i) => `
          <article class="song-card">
            <button class="play" data-index="${i}" title="Afspil">
              <img src="${API_URL}${s.coverPath}" alt="Cover: ${s.title}" />
            </button>
            <h3>${s.title}</h3>
            <p>${s.artist}</p>
          </article>
        `)
        .join("");
    
      songListElem.querySelectorAll(".play").forEach(btn => {
        btn.addEventListener("click", (e) => {
          const idx = Number(e.currentTarget.dataset.index);
          playSong(idx);
        });
      });
    }
    
    // Afspiller-kontrol
    function playSong(index) {
      currentIndex = index;
      const s = songs[index];
      if (!s) return;
    
      // Fjern aktiv klasse fra alle kort
      document.querySelectorAll('.song-card').forEach(card => {
        card.classList.remove('active');
      });
    
      // Tilføj aktiv klasse til det aktuelle kort
      const currentCard = document.querySelector(`[data-index="${index}"]`)?.closest('.song-card');
      if (currentCard) {
        currentCard.classList.add('active');
      }
    
      audio.src = `${API_URL}${s.songPath}`;
      playerCover.src = `${API_URL}${s.coverPath}`;
      playerCover.style.display = "block";
      playerInfo.textContent = `${s.title} — ${s.artist}`;
    
      audio.play();
    }
    
    function togglePlay() {
      if (!audio) return;
      if (audio.paused) {
        audio.play();
      } else {
        audio.pause();
      }
    }
    
    if (playPauseBtn) {
      playPauseBtn.addEventListener("click", togglePlay);
    }
    
    if (audio) {
      audio.addEventListener("play", () => {
        if (playIcon) playIcon.style.display = "none";
        if (pauseIcon) pauseIcon.style.display = "block";
      });
      audio.addEventListener("pause", () => {
        if (playIcon) playIcon.style.display = "block";
        if (pauseIcon) pauseIcon.style.display = "none";
      });
      audio.addEventListener("loadedmetadata", () => {
        if (durationEl) durationEl.textContent = formatTime(audio.duration);
        if (seekBar) seekBar.max = Math.floor(audio.duration || 0);
      });
      audio.addEventListener("timeupdate", () => {
        if (currentTimeEl) currentTimeEl.textContent = formatTime(audio.currentTime);
        if (seekBar && !seekBar.matches(":active")) {
          seekBar.value = Math.floor(audio.currentTime || 0);
        }
      });
      audio.addEventListener("ended", () => {
        if (currentIndex < songs.length - 1) {
          playSong(currentIndex + 1);
        }
      });
    }
    
    if (seekBar) {
      seekBar.addEventListener("input", () => {
        if (!audio) return;
        audio.currentTime = Number(seekBar.value || 0);
      });
    }
    
    function formatTime(sec) {
      const s = Math.floor(sec || 0);
      const m = Math.floor(s / 60);
      const r = s % 60;
      return `${m}:${r.toString().padStart(2, "0")}`;
    }
    
    // Upload ny sang
    if (uploadForm) {
      uploadForm.addEventListener("submit", async (e) => {
        e.preventDefault();
    
        const fd = new FormData(uploadForm);
        if (fd.has("file")) {
          const file = fd.get("file");
          fd.delete("file");
          fd.append("song", file);
        }
    
        try {
          setLoader(true);
          setError(null);
    
          const res = await fetch(`${API_URL}/api/songs`, {
            method: "POST",
            body: fd,
          });
    
          if (!res.ok) {
            const errText = await res.text().catch(() => "");
            throw new Error(`Upload fejlede: ${errText || res.status}`);
          }
    
          const data = await res.json();
          if (!data?.success || !data?.song) throw new Error("Uventet svar fra server");
    
          songs = [data.song, ...songs];
          renderSongs(songs);
    
          uploadForm.reset();
          if (uploadModal) uploadModal.classList.add("hidden");
        } catch (err) {
          setError(err.message);
        } finally {
          setLoader(false);
        }
      });
    }
    
    // Søgning
    if (searchInput) {
      searchInput.addEventListener("input", () => {
        const q = searchInput.value.trim().toLowerCase();
        const filtered = songs.filter(s =>
          s.title.toLowerCase().includes(q) || s.artist.toLowerCase().includes(q)
        );
        renderSongs(filtered);
      });
    }
    
    ```