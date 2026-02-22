# 🏴‍☠️ CTF Hub

En samling af Capture The Flag udfordringer designet til at teste og forbedre cybersikkerhedsfærdigheder.

## 🎯 Om Projektet

CTF Hub er en interaktiv platform med forskellige typer af cybersikkerhedsudfordringer, der spænder fra begynder- til ekspertniveau. Hver udfordring er designet til at lære specifikke færdigheder inden for cybersikkerhed, kryptografi og problemløsning.

## 🚀 Tilgængelige Udfordringer

### 🏛️ [Pharos](Pharos/) - Metadata & Steganografi
- **Sværhedsgrad**: Let
- **Fokus**: Metadata-analyse, historie, geografi
- **Beskrivelse**: Udforsk metadata i et billede af Pharos-fyrtårnet og find skjulte informationer

### 🔐 [Crypto](Crypto/) - Kryptografi Kæde
- **Sværhedsgrad**: Medium
- **Fokus**: Kryptografi, matematik, algoritmer

### 🌐 Web Exploitation - *Kommende*
- **Sværhedsgrad**: Svær
- **Fokus**: Web-sikkerhed, SQL injection, XSS

### 🔍 Forensik Mysterie - *Kommende*
- **Sværhedsgrad**: Medium
- **Fokus**: Digital forensik, log-analyse

### ⚡ Reverse Engineering - *Kommende*
- **Sværhedsgrad**: Svær
- **Fokus**: Assembly, debugging, binær analyse

### 🎯 OSINT Opdagelse - *Kommende*
- **Sværhedsgrad**: Let
- **Fokus**: Open Source Intelligence, research

## 🛠️ Teknologi

- **Frontend**: HTML5, CSS3, JavaScript (ES6+)
- **Design**: Moderne, responsivt design med glassmorphism-effekter
- **Routing**: Client-side routing system
- **Kompatibilitet**: Moderne browsere (Chrome, Firefox, Safari, Edge)

## 🎮 Sådan Starter Du

### 🐳 Docker (Anbefalet)
```bash
# Start CTF Hub med Docker
./start-ctf.sh          # Linux/Mac
# eller
start-ctf.bat           # Windows

# Eller manuelt:
docker-compose up --build -d
```

### 🌐 Adgang
- **Hovedside:** http://localhost:8080
- **Pharos CTF:** http://localhost:8080/Pharos/
- **Crypto CTF:** http://localhost:8080/Crypto/

### 🔗 Tunnel Setup
```bash
# Med ngrok (installer først: https://ngrok.com/)
ngrok http 8080

# Med cloudflared
cloudflared tunnel --url http://localhost:8080
```

### 📱 Udfordringer
1. Vælg en udfordring fra hovedmenuen
2. Følg instruktionerne for hver udfordring
3. Find flaget i formatet `CTF{...}`

## 📚 Læringsmål

- **Metadata-analyse**: Lær at uddrage information fra billeder og filer
- **Kryptografi**: Forstå forskellige krypteringsmetoder
- **Web-sikkerhed**: Identificer og udnyt web-sårbarheder
- **Forensik**: Analyser digitale spor og beviser
- **Reverse Engineering**: Forstå binær kode og programmets funktioner
- **OSINT**: Saml information fra offentlige kilder

## 🤝 Bidrag

Dette projekt er åbent for bidrag! Hvis du har ideer til nye CTF-udfordringer eller forbedringer, er du velkommen til at bidrage.

## 📄 Licens

Dette projekt er udviklet til uddannelsesmæssige formål.

---

*"Det er ikke altid det, du ser, der er det vigtigste..."* 🕵️‍♂️