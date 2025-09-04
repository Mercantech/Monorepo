## Dokumentation af jeres API med XML-kommentarer

Når I bygger en API, er det vigtigt, at både den som har lavet kontrolleren og andre udviklere hurtigt kan forstå, hvad de forskellige endpoints gør. Derfor skal I **dokumentere jeres controllere og endpoints**, og det gør vi i .NET med **XML-kommentarer**.

### Hvad er XML-kommentarer?

XML-kommentarer er specialformat, som .NET og værktøjer som Swagger kan læse og vise som **automatisk API-dokumentation**.

De skrives som `///` lige over metoder, klasser og parametre. Eksempel:

```csharp
/// <summary>
/// Henter alle brugere.
/// </summary>
/// <returns>En liste af brugere.</returns>
[HttpGet]
public async Task<ActionResult<IEnumerable<User>>> GetUsers()
{
    return await _context.Users.ToListAsync();
}
```

### De vigtigste tags

Her er de mest brugte tags du skal kende:

|Tag|Funktion|
|---|---|
|`<summary>`|Kort beskrivelse af hvad metoden gør|
|`<param name="...">`|Beskriver en indparameter|
|`<returns>`|Forklarer hvad metoden returnerer|
|`<response code="...">`|(Til Web API) Hvilke HTTP-statuskoder der kan returneres og hvorfor|

### Best Practice - Hvad I burde gøre, jo mindre I har en god grund til at gøre noget andet!

1. **Start altid med `<summary>`** – Tænk: "Hvad gør denne metode?"
2. **Forklar alle parametre** – Det gør det tydeligt, hvad der forventes.
3. **Vis mulige fejl** – Brug `<response code="...">` til at vise f.eks. `404` eller `400`.
4. **Vær kortfattet men præcis** – Forestil dig, at en anden skal bruge din API uden at kende koden.

---

### Eksempel: Dokumenteret endpoint

```csharp
/// <summary>
/// Henter en specifik bruger ud fra ID.
/// </summary>
/// <param name="id">Brugerens unikke ID.</param>
/// <returns>Brugerens detaljer.</returns>
/// <response code="404">Bruger ikke fundet.</response>
[HttpGet("{id}")]
public async Task<ActionResult<User>> GetUser(string id)
{
    ...
}
```

---

### Bonus: Sådan aktiverer du dokumentation i Swagger

1. Åbn din `csproj` og tilføj:

```xml
<PropertyGroup>
  <GenerateDocumentationFile>true</GenerateDocumentationFile>
</PropertyGroup>
```

1. Tilføj dette i `Program.cs` hvis du bruger Swagger:

```csharp
builder.Services.AddSwaggerGen(c =>
{
    var xmlFilename = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
    c.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, xmlFilename));
});
```

Nu vises dine kommentarer som lækker dokumentation i Swagger UI!

---

### Øvelse til jer

Gå igennem din controller og tilføj XML-kommentarer på alle endpoints. Brug `<summary>`, `<param>` og `<returns>`, og tilføj mindst én `<response code="...">` pr. endpoint.

Husk: En veldokumenteret API er nemmere at teste, bruge og fejlfinde – både for dig og andre!

For en konkret implementering, kan man se følgende commit med XML til 2 controllers

[https://github.com/Mercantec-GHC/h2-projekt-z2-2025-magsteacher/commit/0efefb5e92ddeaea5cfb5e1260a2f5e0f92975c3](https://github.com/Mercantec-GHC/h2-projekt-z2-2025-magsteacher/commit/0efefb5e92ddeaea5cfb5e1260a2f5e0f92975c3)
![[Pasted image 20250904220928.png]]
![[Pasted image 20250904220942.png]]

XML er også generelt acceptere på mange API-værktøjer, såsom Postman, Swagger og [[Bruno]]!