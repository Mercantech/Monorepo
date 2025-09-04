# 🎭 Bogus (Faker) Guide - Generer Realistisk Test Data

## Hvad er Bogus?

**Bogus** er et C# bibliotek til at generere **fake/mock data** til testformål. Det er inspireret af det populære JavaScript bibliotek **Faker.js** og giver os mulighed for at oprette realistiske testdata på en hurtig og konsistent måde. Vi kan bruge det til at gøre følgende

- **Realistiske data**: Navne, adresser, telefonnumre, emails, osv.
- **Fleksible regler**: Definer præcist hvordan data skal genereres
- **Relationer**: Håndter komplekse data-relationer
- **Performant**: Genererer tusindvis af records hurtigt

1. **Realistisk Test Data**
    
    ```csharp
    // I stedet for:
    var user = new User { Name = "Test User", Email = "test@test.com" };
    
    // Får vi:
    var user = new User { Name = "Mads Hansen", Email = "mads.hansen@gmail.com" };
    
    ```
    
2. **Skalerbarhed**
    
    - Generer 1 eller 10.000 records med samme kode
    - Perfekt til performance testing
3. **Konsistens**
    
    - Samme data struktur hver gang
    - Reproducerbare tests med seeds
4. **Tidsbesparelse**
    
    - Ingen manuel oprettelse af test data
    - Automatisk population af development databaser
5. **Bedre Testing**
    
    - Test med varieret, realistisk data
    - Opdager edge cases lettere

## 📦 Installation

### NuGet Package Manager:

```bash
dotnet add package Bogus
```

## Grundlæggende Koncepter

### 1. **Faker Objekt**

```csharp
var faker = new Faker("en");
var name = faker.Person.FullName; // "Lars Andersen"
var email = faker.Internet.Email(); // "lars.andersen@gmail.com"

```

### 2. **Typed Faker**

```csharp
var userFaker = new Faker<User>("en")
    .RuleFor(u => u.Name, f => f.Person.FullName)
    .RuleFor(u => u.Email, f => f.Internet.Email())
    .RuleFor(u => u.Age, f => f.Random.Int(18, 80));

var user = userFaker.Generate(); // Genererer én bruger
var users = userFaker.Generate(100); // Genererer 100 brugere

```

### 3. **Lokalisering**

```csharp
var danishFaker = new Faker("da"); // Danske navne og adresser
var englishFaker = new Faker("en"); // Engelske navne og adresser

```

## Implementering i vores Hotel System

### 📁 **Struktur:**

```
API/
├── Services/
│   └── DataSeederService.cs    # Hovedservice til seeding
├── Controllers/
│   └── DataSeederController.cs # API endpoints til seeding
└── Program.cs                  # DI registrering

```

### 🔧 **DataSeederService.cs**

```csharp
public class DataSeederService
{
    private readonly AppDBContext _context;
    private readonly ILogger<DataSeederService> _logger;

    // Seed brugere
    private async Task<List<User>> SeedUsersAsync(int count)
    {
        var faker = new Faker<User>("en")
            .RuleFor(u => u.Id, f => Guid.NewGuid().ToString())
            .RuleFor(u => u.Email, f => f.Internet.Email().ToLower())
            .RuleFor(u => u.Username, (f, u) => u.Email.Split('@')[0])
            .RuleFor(u => u.HashedPassword, f => HashPassword("Password123!"))
            .RuleFor(u => u.RoleId, f => 
            f.PickRandom("user-role-id", "admin-role-id"))
            .RuleFor(u => u.LastLogin, f => f.Date.Recent(30))
            .RuleFor(u => u.CreatedAt, f => f.Date.Past(2))
            .RuleFor(u => u.UpdatedAt, (f, u) => 
            f.Date.Between(u.CreatedAt, DateTime.UtcNow));

        return faker.Generate(count);
    }

    // Seed hoteller
    private async Task<List<Hotel>> SeedHotelsAsync(int count)
    {
        var hotelNames = new[] { "Hotel Royal", "Grand Hotel", "Scandic Hotel" };
        var cities = new[] { "København", "Aarhus", "Odense" };

        var faker = new Faker<Hotel>("en")
            .RuleFor(h => h.Id, f => Guid.NewGuid().ToString())
            .RuleFor(h => h.Name, f => f.PickRandom(hotelNames) + " " 
            + f.PickRandom(cities))
            .RuleFor(h => h.Address, f => f.Address.FullAddress())
            .RuleFor(h => h.CreatedAt, f => f.Date.Past(5))
            .RuleFor(h => h.UpdatedAt, (f, h) => 
            f.Date.Between(h.CreatedAt, DateTime.UtcNow));

        return faker.Generate(count);
    }
}
```

### 🎮 **Controller Endpoints:**

```csharp
[Route("api/[controller]")]
[ApiController]
[Authorize(Roles = "Admin")]
public class DataSeederController : ControllerBase
{
    // Seed komplet database
    [HttpPost("seed")]
    public async Task<ActionResult> SeedDatabase(
        [FromQuery] int userCount = 50,
        [FromQuery] int hotelCount = 10,
        [FromQuery] int roomsPerHotel = 20,
        [FromQuery] int bookingCount = 100)

    // Ryd database
    [HttpDelete("clear")]
    public async Task<ActionResult> ClearDatabase()

    // Hent statistikker
    [HttpGet("stats")]
    public async Task<ActionResult> GetDatabaseStats()
}
```

## 🔥 Avancerede Funktioner

### 1. **Betingede Regler**

```csharp
var faker = new Faker<User>("en")
    .RuleFor(u => u.Age, f => f.Random.Int(18, 80))
    .RuleFor(u => u.IsStudent, (f, u) => u.Age < 25) // Afhænger af alder
    .RuleFor(u => u.Income, (f, u) => u.IsStudent ?
        f.Random.Decimal(0, 15000) :
        f.Random.Decimal(25000, 80000));
```

### 2. **Vægtede Valg**

```csharp
var faker = new Faker<Room>("en")
    .RuleFor(r => r.Type, f => f.Random.WeightedRandom(
        new[] { "Standard", "Deluxe", "Suite" },
        new[] { 0.6f, 0.3f, 0.1f })); // 60% Standard, 30% Deluxe, 10% Suite
```

### 3. **Komplekse Relationer**

```csharp
// Først opret hoteller
var hotels = hotelFaker.Generate(10);

// Derefter rum der refererer til hoteller
var roomFaker = new Faker<Room>("en")
    .RuleFor(r => r.HotelId, f => f.PickRandom(hotels).Id)
    .RuleFor(r => r.Number, f => f.Random.Int(101, 999).ToString());

var rooms = roomFaker.Generate(200);
```

### 4. **Undgå Dubletter**

```csharp
var usedEmails = new HashSet<string>();

var userFaker = new Faker<User>("en")
    .RuleFor(u => u.Email, f => {
        string email;
        do {
            email = f.Internet.Email();
        } while (usedEmails.Contains(email));

        usedEmails.Add(email);
        return email;
    });
```

### 5. **Seeds for Reproducerbare Tests**

```csharp
var faker = new Faker("en");
faker.Random = new Randomizer(12345); // Fast seed

// Genererer samme data hver gang
var user1 = faker.Person.FullName; // Altid samme navn
```

## 🎯 Best Practices

1. **Valider Constraints**
    
    ```csharp
    .RuleFor(u => u.Email, f => f.Internet.Email().ToLower()) // Lowercase emails
    .RuleFor(u => u.Age, f => f.Random.Int(18, 120)) // Realistisk alder
    ```
    
2. **Håndter Relationer Korrekt**
    
    ```csharp
    // Opret users (parents) først, bookings (children) bagefter
    var users = userFaker.Generate(100);
    var bookings = bookingFaker.Generate(500); // Referencer users
    ```
    
3. **Brug Meaningful Data**
    
    ```csharp
    .RuleFor(h => h.StarRating, f => f.Random.Int(1, 5)) // Hotel stjerner
    .RuleFor(r => r.PricePerNight,
    f => f.Random.Decimal(200, 2000)) // Realistiske priser
    ```
    

### ❌ **Undgå:**

1. **For Mange Records i Tests**
    
    ```csharp
    // Dårligt - langsom test
    var users = faker.Generate(10000);
    
    // Godt - hurtig test
    var users = faker.Generate(10);
    
    ```
    
2. **Hardcodede Værdier**
    
    ```csharp
    // Dårligt
    .RuleFor(u => u.Name, f => "Test User")
    
    // Godt
    .RuleFor(u => u.Name, f => f.Person.FullName)
    
    ```
    

## Eksempler

### 🏨 **Hotel Booking System**

```csharp
// 1. Opret brugere
var userFaker = new Faker<User>("da")
    .RuleFor(u => u.Email, f => f.Internet.Email())
    .RuleFor(u => u.Name, f => f.Person.FullName);

var users = userFaker.Generate(50);

// 2. Opret hoteller
var hotelFaker = new Faker<Hotel>("da")
    .RuleFor(h => h.Name, f => "Hotel " + f.Address.City())
    .RuleFor(h => h.Address, f => f.Address.FullAddress())
    .RuleFor(h => h.StarRating, f => f.Random.Int(1, 5));

var hotels = hotelFaker.Generate(10);

// 3. Opret rum
var roomFaker = new Faker<Room>("da")
    .RuleFor(r => r.HotelId, f => f.PickRandom(hotels).Id)
    .RuleFor(r => r.Number, f => f.Random.Int(101, 999).ToString())
    .RuleFor(r => r.Type, f => f.PickRandom("Standard", "Deluxe", "Suite"))
    .RuleFor(r => r.PricePerNight, f => f.Random.Decimal(500, 3000))
    .RuleFor(r => r.Capacity, f => f.Random.Int(1, 4))
    .RuleFor(r => r.HasBalcony, f => f.Random.Bool(0.3f)) // 30% har balkon
    .RuleFor(r => r.HasSeaView, f => f.Random.Bool(0.2f)); // 20% har havudsigt

var rooms = roomFaker.Generate(200);

// 4. Opret bookinger (uden overlap)
var bookingFaker = new Faker<Booking>("da");
var bookings = new List<Booking>();

for (int i = 0; i < 300; i++)
{
    var user = faker.PickRandom(users);
    var room = faker.PickRandom(rooms);
    var startDate = faker.Date.Future(1);
    var nights = faker.Random.Int(1, 14);
    var endDate = startDate.AddDays(nights);

    // Tjek for overlap (forenklet)
    var hasOverlap = bookings.Any(b =>
        b.RoomId == room.Id &&
        b.StartDate < endDate &&
        b.EndDate > startDate);

    if (!hasOverlap)
    {
        bookings.Add(new Booking
        {
            UserId = user.Id,
            RoomId = room.Id,
            StartDate = startDate,
            EndDate = endDate,
            NumberOfGuests = faker.Random.Int(1, room.Capacity),
            TotalPrice = room.PricePerNight * nights,
            Status = faker.PickRandom("Confirmed", "Pending", "Cancelled")
        });
    }
}

```

### 🔧 **API Endpoints Usage**

```bash
# Seed komplet database
POST /api/dataseeder/seed?userCount=100&hotelCount=15&roomsPerHotel=25&bookingCount=200

# Seed kun brugere
POST /api/dataseeder/seed-users?count=50

# Seed kun hoteller og rum
POST /api/dataseeder/seed-hotels?hotelCount=5&roomsPerHotel=20

# Hent database statistikker
GET /api/dataseeder/stats

# Ryd hele databasen
DELETE /api/dataseeder/clear

```

## 🔐 **Sikkerhed og Miljøer**

⚠️ **VIGTIGT**: Brug kun Bogus i **development** og **test** miljøer!

```csharp
// Sikkerhedstjek i controller
if (!_environment.IsDevelopment())
{
    return BadRequest("Database seeding er kun tilladt i development miljø");
}

```

## 🎯 **Konklusion**

Bogus er et godt **værktøj** til moderne .NET udvikling. Det gør det nemt at:

- ✅ Oprette realistisk test data
- ✅ Populere development databaser
- ✅ Teste med varieret data
- ✅ Spare tid på manuel data-oprettelse
- ✅ Forbedre demo-præsentationer

### 🚀 **Næste Skridt:**

1. Installer Bogus i dit projekt
2. Opret en DataSeederService
3. Definer Faker regler for dine modeller
4. Byg API endpoints til seeding
5. Brug i development og testing

**Happy Coding! 🎭✨**

> [!question] Tjek din viden på videnstjek.mags.dk
> Tjek din viden omkring Bogus med en quiz på [Videnstjek](https://videnstjek.mags.dk/quiz/bogus-faker)