# Pensum: Stolpediagram – fra data til graf

Kort pensum til undervisning i statistik om **stolpediagrammer** (søjlediagrammer) og hvordan man laver dem til en graf.

---

## 1. Hvad er et stolpediagram?

Et **stolpediagram** (eller **søjlediagram**) er en graf, der viser **kategoridata** ved hjælp af **søjler** (stolper). Hver søjles **højde** svarer til **værdien** (fx antal, procent) for den pågældende kategori.

- **Kategorier** står typisk langs den vandrette akse (x-aksen).
- **Værdier** (tal) aflæses på den lodrette akse (y-aksen).
- Søjlerne har **samme bredde** og **mellemrum** mellem sig, så sammenligning er nem.

**Eksempel:** Antal solgte frugter (æbler 4, pærer 7, bananer 3, appelsiner 5) kan vises som fire søjler med højder 4, 7, 3 og 5.

---

## 2. Hvornår bruger man stolpediagram?

- Når man har **kategorier** (fx frugttyper, måneder, hold) og et **tal per kategori** (antal, procent, gennemsnit).
- Når man vil **sammenligne** kategorierne visuelt.
- **Ikke** til tidsrækker med mange tidspunkter (der er linjediagram ofte bedre) eller til andele af en helhed (der kan lagkagediagram være passende).

---

## 3. Sådan laver du et stolpediagram (trin for trin)

### Trin 1: Har du data i rækkefølge

- **Kategorier** (navnene på x-aksen), fx: Æbler, Pærer, Bananer, Appelsiner.
- **Værdier** (tallene der skal vises som højde), fx: 4, 7, 3, 5.

### Trin 2: Tegn akser

- **Lodret akse (y-aksen):** Talakse med værdier fra 0 og op (evt. 0, 2, 4, 6, 8 …).
- **Vandret akse (x-aksen):** En linje hvor kategorierne kommer til at stå under hver søjle.
- Giv **y-aksen et navn** (fx "Antal") og sørg for, at **skalaen** passer til dine tal (fx 1 cm = 1 enhed).

### Trin 3: Tegn søjlerne

- For **hver kategori** tegner du en **søjle** (rektangel) op fra x-aksen.
- **Søjlens højde** svarer præcist til **værdien** for den kategori (aflæst på y-aksen).
- Brug **samme bredde** og **lige afstand** mellem søjlerne.
- Du kan bruge **farver** til at skelne kategorier (valgfrit).

### Trin 4: Gør grafen læsbar

- Skriv **kategorinavnene** under eller ved hver søjle på x-aksen.
- Tjek at **y-aksen** har tal og evt. enhed (fx "Antal" eller "Procent").
- Giv grafen evt. en **titel** (fx "Solgte frugter pr. uge").

---

## 4. Fra “rå data” til graf – kort opsummering

| Trin | Handling |
|------|----------|
| 1 | Skriv kategorier og værdier ned (evt. i en tabel). |
| 2 | Tegn to akser: lodret (tal) og vandret (kategorier). |
| 3 | Tegn en søjle per kategori med højde = værdi. |
| 4 | Tilføj navne på akser og evt. titel. |

Når du følger disse trin, har du lavet dataene **om til en graf** – et stolpediagram.

---

## 5. Videodemo

I denne mappe findes en Manim-animation, **StolpediagramDemo**, der viser netop denne proces: fra data (æbler, pærer, bananer, appelsiner med antal 4, 7, 3, 5) til akser og derefter søjler.

Kør videoen med:

```bash
manim -pqm manim_demo.py StolpediagramDemo
```

---

## 6. Ekstra: Tjekliste til elever

- [ ] Jeg har skrevet alle kategorier og værdier ned.
- [ ] Y-aksen starter ved 0 og har en passende skala.
- [ ] Hver søjles højde matcher værdien på y-aksen.
- [ ] Kategorierne står under/ved de rigtige søjler.
- [ ] Akser og evt. graf har navne/titel.

---

*Pensum til statistik: stolpediagram og hvordan man laver dem til en graf.*
