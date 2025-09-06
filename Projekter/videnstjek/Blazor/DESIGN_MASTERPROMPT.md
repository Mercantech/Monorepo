# Videnstjek Blazor App - Design Masterprompt

## Overordnet Design Filosofi
Videnstjek er en moderne, brugervenlig quiz-applikation bygget med Blazor Server, der kombinerer et professionelt uddannelsesdesign med interaktive elementer og responsivt layout. Applikationen fokuserer på at skabe en engagerende læringsoplevelse gennem visuelt tiltalende komponenter og intuitive brugerinteraktioner.

## Farvepalette og Design Tokens

### Primære Farver
```css
--primary-color: #6366f1;        /* Indigo - Hovedfarve for branding */
--primary-dark: #4f46e5;         /* Mørkere indigo for hover states */
--success-color: #10b981;        /* Grøn for korrekte svar */
--warning-color: #f59e0b;        /* Orange for advarsler */
--danger-color: #ef4444;         /* Rød for fejl/inkorrekte svar */
--info-color: #3b82f6;           /* Blå for information */
--light-bg: #f8fafc;             /* Lys baggrund */
--dark-text: #1e293b;            /* Mørk tekst */
```

### Design Tokens
```css
--border-radius: 12px;           /* Afrundede hjørner */
--shadow: 0 10px 25px -5px rgba(0, 0, 0, 0.1), 0 10px 10px -5px rgba(0, 0, 0, 0.04);
--shadow-lg: 0 25px 50px -12px rgba(0, 0, 0, 0.25);
--sidebar-width: 250px;          /* Sidebar bredde */
```

## Layout Struktur

### Sidebar Navigation
- **Position**: Fast positioneret til venstre (250px bredde)
- **Baggrund**: Gradient fra primær farve til mørkere variant
- **Navigation**: Vertikal liste med ikoner og tekst
- **Hover Effekter**: Subtile baggrundsændringer og border-left indikatorer
- **Responsiv**: Skjules på mobile enheder med slide-in animation

### Hovedindhold
- **Layout**: Flexbox med margin-left for sidebar
- **Baggrund**: Lys grå (#f8fafc)
- **Container**: Bootstrap container med responsiv padding
- **Z-index**: Properly layered for overlays og modals

## Komponent Design

### Hero Section
- **Baggrund**: Gradient fra #667eea til #764ba2
- **Tekst**: Hvid med gradient tekst effekt på titel
- **Animationer**: Floating cards med CSS keyframes
- **Responsiv**: Tilpasser sig forskellige skærmstørrelser

### Quiz Kort
- **Design**: Hvid baggrund med subtile skygger
- **Border**: Ingen border, kun box-shadow
- **Hover**: Transform translateY(-5px) og forøget skygge
- **Padding**: Generøs padding (2rem) for luftigt design

### Quiz Spørgsmål
- **Layout**: Centreret i 8-kolonne grid
- **Progress Bar**: Gradient progress bar med afrundede hjørner
- **Svar Muligheder**: 
  - Hvid baggrund med grå border
  - Hover: Primær farve border og baggrund
  - Selected: Primær farve baggrund og hvid tekst
  - Correct: Grøn baggrund og hvid tekst
  - Incorrect: Rød baggrund og hvid tekst

### Code Blokke
- **Design**: VSCode Dark Mode inspireret
- **Baggrund**: #1e1e1e (mørk)
- **Header**: #2d2d30 med sprogindikator
- **Syntax Highlighting**: Omfattende C# syntax highlighting
- **Copy Button**: Subtile hover effekter

## Typografi

### Font Stack
```css
font-family: 'Segoe UI', Tahoma, Geneva, Verdana, sans-serif;
```

### Hierarki
- **Display**: 4-5rem for hovedtitler
- **H1-H6**: Bootstrap standard med custom farver
- **Body**: 1rem med 1.6 line-height
- **Small**: 0.8-0.9rem for metadata

## Animationer og Transitions

### Hover Effekter
- **Buttons**: translateY(-2px) + box-shadow
- **Cards**: translateY(-5px) + forøget skygge
- **Navigation**: Baggrundsændring + border-left

### Keyframe Animationer
- **Float**: 6s ease-in-out infinite for floating cards
- **FadeIn**: 0.5s ease-in for nye elementer
- **Pulse**: 2s infinite for score cirkler
- **SlideInUp**: 0.8s ease-out for modals

### Transition Timing
```css
transition: all 0.3s ease;  /* Standard transition */
```

## Responsiv Design

### Breakpoints
- **Mobile**: < 768px
- **Tablet**: 768px - 1024px
- **Desktop**: > 1024px

### Mobile Anpassinger
- Sidebar skjules med transform: translateX(-100%)
- Hero section reduceret højde
- Button padding reduceret
- Font størrelser justeret
- Card padding reduceret

## Interaktive Elementer

### Buttons
- **Primær**: Indigo baggrund med hvid tekst
- **Outline**: Transparent baggrund med indigo border
- **Success**: Grøn baggrund
- **Hover**: Transform + skygge effekter
- **Disabled**: Grå baggrund med reduceret opacity

### Form Elementer
- **Input Fields**: Subtile borders med focus states
- **Progress Bars**: Gradient fyld med afrundede hjørner
- **Badges**: Afrundede med passende farver

### Modals og Overlays
- **Backdrop**: Semi-transparent sort med blur effekt
- **Content**: Hvid baggrund med store skygger
- **Animation**: Fade in + scale effekter

## Accessibility

### Farve Kontrast
- Alle tekst har tilstrækkelig kontrast
- Fokus states er tydelige
- Color coding suppleres med ikoner

### Keyboard Navigation
- Tab navigation understøttet
- Keyboard shortcuts for quiz svar (A, B, C, D)
- Enter key for bekræftelse

### Screen Reader Support
- Proper ARIA labels
- Semantic HTML struktur
- Alt tekst for ikoner

## Performance Optimering

### CSS
- CSS custom properties for konsistent styling
- Efficient selectors
- Minimal repaints med transform/opacity

### JavaScript
- Lazy loading af komponenter
- Efficient state management
- Debounced user interactions

## Browser Support

### Moderne Browsers
- Chrome 90+
- Firefox 88+
- Safari 14+
- Edge 90+

### Fallbacks
- Graceful degradation for ældre browsere
- CSS Grid fallbacks
- Custom properties fallbacks

## Design System Komponenter

### Cards
- `.card`: Basis kort styling
- `.quiz-card`: Specialiseret quiz kort
- `.feature-card`: Feature highlight kort

### Buttons
- `.btn`: Basis button styling
- `.btn-primary`: Primær action button
- `.btn-outline-*`: Outline variant buttons

### Navigation
- `.nav-link`: Navigation links
- `.nav-brand`: Brand/logo styling
- `.sidebar`: Sidebar container

### Utilities
- `.text-gradient`: Gradient tekst effekt
- `.fade-in`: Fade in animation
- `.floating-card`: Floating animation element

## Implementerings Guidelines

### CSS Struktur
1. CSS Custom Properties (variabler) øverst
2. Reset/normalize styles
3. Layout komponenter
4. Komponent specifikke styles
5. Utility classes
6. Responsive media queries
7. Animation keyframes

### Naming Convention
- BEM-lignende struktur
- Beskrivende klassenavne
- Konsistent spacing i navngivning

### File Organization
- `app.css`: Hovedstilark
- `MainLayout.razor.css`: Layout specifikke styles
- `NavMenu.razor.css`: Navigation styles
- Komponent specifikke CSS filer

## Fremtidige Forbedringer

### Design System
- Token-baseret design system
- Komponent dokumentation
- Design system storybook

### Animationer
- Micro-interactions
- Page transitions
- Loading states

### Accessibility
- WCAG 2.1 AA compliance
- High contrast mode
- Reduced motion support

---

*Denne masterprompt beskriver det komplette design system for Videnstjek Blazor applikationen og kan bruges som reference for konsistent design implementering.*
