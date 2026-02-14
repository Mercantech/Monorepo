## Manim – matematik-animationer

[Manim](https://www.manim.community/) (Mathematical Animation Engine) er et Python-bibliotek til at lave flotte matematik-animationer (fx til undervisning eller videoer).

### Installation

```bash
cd MLWithPython/Manim
pip install -r requirements.txt
# eller: pip install manim
```

### Kør demo

Fra mappen `MLWithPython/Manim`:

```bash
# Medium kvalitet, åbn video efter render (anbefalet)
manim -pqm manim_demo.py IntroScene
manim -pqm manim_demo.py CircleEquation
manim -pqm manim_demo.py EulersIdentity
manim -pqm manim_demo.py Pythagoras
manim -pqm manim_demo.py GraphDemo
```

**Kvalitet** (vælg efter behov):
| Flag | Betydning | Opløsning / fps | Brug til |
|------|-----------|------------------|----------|
| `-ql` | Lav | 480p, 15 fps | Hurtig test |
| `-qm` | Medium | 720p, 30 fps | God balance |
| `-qh` | Høj | 1080p, 60 fps | Færdig video |
| `-qk` | 4K | 2160p, 60 fps | Meget høj kvalitet |

**Åbn video:** `-p` (preview). Uden `-p` gemmes filen i `media/videos/`.

Demoen bruger `Text()` med almindelig/unicode-tekst, så den kører **uden LaTeX**. For flottere matematik (fx med `MathTex`) kan du installere en TeX-distribution (fx [MiKTeX](https://miktex.org/) på Windows).

### Scener i demoen

| Scene           | Beskrivelse                          |
|-----------------|--------------------------------------|
| `IntroScene`    | Titel og undertekst                  |
| `CircleEquation`| Cirkel og ligningen \(x^2 + y^2 = r^2\) |
| `EulersIdentity`| Eulers identitet \(e^{i\pi} + 1 = 0\)   |
| `Pythagoras`    | Retvinklet trekant og \(a^2 + b^2 = c^2\) |
| `GraphDemo`     | Parabel \(y = x^2\)                  |