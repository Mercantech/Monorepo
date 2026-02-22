# 🔐 Kryptografi CTF

En interaktiv Capture The Flag udfordring fokuseret på kryptografi og dekryptering.

## 📺 Introduktion

**VIGTIGT:** Se denne video før du starter: [Kryptografi Introduktion](https://www.youtube.com/watch?v=NuyzuNBFWxQ&t=414s)

Videoen dækker grundlæggende kryptografi-koncepter som er nødvendige for at løse udfordringerne.

## 🎯 Udfordringer

### Flag 1 (Let)
- **Tekst:** `Wkh qdph ri wkh jdph lv "Fdswxuh wkh Iodj"`
- **Hint:** Caesar cipher flytter hver bogstav med et fast antal positioner i alfabetet
- **Flag:** `CTF{caesar_cipher}`

### Flag 2 (Let)
- **Tekst:** `Q1RGe2Jhc2U2NF9lbmNvZGluZ19pbl9hY3Rpb259`
- **Hint:** Base64 er en måde at konvertere binære data til tekst
- **Flag:** `CTF{base64_decoded}`

### Flag 3 (Let)
- **Tekst:** `Gur pbqr vf "ebg13_rapbqrq"`
- **Hint:** ROT13 er en Caesar cipher med shift 13
- **Flag:** `CTF{rot13_text}`

### Flag 4 (Medium)
- **Tekst:** `48657861646563696d616c5f6465636f64696e67`
- **Hint:** Hex bruger 0-9 og A-F til at repræsentere binære data
- **Flag:** `CTF{hex_converted}`

### Flag 5 (Svær)
- **Tekst:** `Lwjv qfqj lwjv qfqj lwjv qfqj`
- **Nøgle:** `SECRET`
- **Hint:** Vigenère bruger en nøgle-ord til at kryptere hver bogstav
- **Flag:** `CTF{vigenere_solved}`

## 📋 Flag Format

Alle flag følger standard formatet:
```
CTF{flag_indhold}
```

### Eksempler på korrekte flag:
- `CTF{caesar_cipher}` (16 tegn)
- `CTF{base64_decoded}` (18 tegn)
- `CTF{rot13_text}` (14 tegn)
- `CTF{hex_converted}` (16 tegn)
- `CTF{vigenere_solved}` (18 tegn)

### Regler:
- Alt i små bogstaver
- Brug underscore (_) i stedet for mellemrum
- Ingen specialtegn udover underscore
- Altid start med `CTF{` og slut med `}`
- Længden varierer afhængigt af indholdet

## 🛠️ Værktøjer

### Online Værktøjer:
- [Base64 Decoder](https://www.base64decode.org/)
- [Caesar Cipher Decoder](https://cryptii.com/pipes/caesar-cipher)
- [ROT13 Decoder](https://rot13.com/)
- [Hex to Text Converter](https://www.rapidtables.com/convert/number/hex-to-ascii.html)
- [Vigenère Cipher Decoder](https://cryptii.com/pipes/vigenere-cipher)

### Kommandolinje Værktøjer:
```bash
# Base64
echo "Q1RGe2Jhc2U2NF9lbmNvZGluZ19pbl9hY3Rpb259" | base64 -d

# Caesar Cipher (Python)
python3 -c "
text = 'Wkh qdph ri wkh jdph lv \"Fdswxuh wkh Iodj\"'
shift = 3
result = ''
for char in text:
    if char.isalpha():
        ascii_offset = 65 if char.isupper() else 97
        result += chr((ord(char) - ascii_offset - shift) % 26 + ascii_offset)
    else:
        result += char
print(result)
"

# ROT13 (Python)
import codecs
print(codecs.decode('Gur pbqr vf "ebg13_rapbqrq"', 'rot13'))

# Hex to Text (Python)
hex_string = '48657861646563696d616c5f6465636f64696e67'
print(bytes.fromhex(hex_string).decode('ascii'))
```

## 🎮 Funktioner

- **Progress Tracking:** Følg din fremgang gennem alle udfordringer
- **Flexibel Rækkefølge:** Løs flag i vilkårlig rækkefølge
- **Local Storage:** Din fremgang gemmes automatisk
- **Responsivt Design:** Fungerer på alle enheder
- **Hints:** Få hjælp til hver udfordring

## 🔧 Teknisk Information

- **Frontend:** HTML5, CSS3, JavaScript (ES6+)
- **Styling:** Moderne glassmorphism design
- **State Management:** Local Storage for progress
- **Validation:** Client-side flag validation
- **Accessibility:** Keyboard navigation support

## 🎓 Læringsmål

Efter at have gennemført denne CTF vil du have lært:

- **Caesar Cipher:** Grundlæggende substitutionskryptering
- **Base64:** Binær-til-tekst encoding
- **ROT13:** Speciel case af Caesar cipher
- **Hexadecimal:** Hex-til-ASCII konvertering
- **Vigenère Cipher:** Polyalfabetisk substitutionskryptering

## 🚀 Kom i Gang

1. Åbn `index.html` i din browser
2. Se introduktionsvideoen
3. Læs flag format guiden
4. Start med at løse udfordringerne
5. Følg din fremgang i progress baren

## 🏆 Completion

Når du har løst alle 5 flag, vil du få en completion-besked og kunne navigere tilbage til hovedmenuen for at prøve andre CTF-udfordringer.

---

*"Kryptografi er kun så sikker som den svageste nøgle..."* 🔐
