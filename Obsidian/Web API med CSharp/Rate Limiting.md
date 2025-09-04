## Rate Limiting og Sikkerhed

### Login Rate Limiting

For at beskytte mod brute force angreb har vi implementeret et avanceret rate limiting system for login forsøg:

### Funktionalitet

- **Progressive delays**: Hver mislykket forsøg resulterer i længere ventetid
- **Account lockout**: Efter 5 mislykkede forsøg låses kontoen i 15 minutter
- **Memory-baseret tracking**: Bruger in-memory cache til at tracke forsøg
- **Automatisk cleanup**: Succesfuldt login rydder fejl-cache

### Konfiguration

```csharp
private const int MaxAttempts = 5; // Maksimalt antal forsøg før lockout
private const int LockoutMinutes = 15; // Lockout periode i minutter
private const int DelayIncrementSeconds = 2; // Sekunder at tilføje per forsøg
```

### Implementering i Login Endpoint

```csharp
[HttpPost("login")]
public async Task<IActionResult> Login(LoginDto dto)
{
    try
    {
        // 1. Tjek om email er låst
        if (_loginAttemptService.IsLockedOut(dto.Email))
        {
            var remainingSeconds = _loginAttemptService.GetRemainingLockoutSeconds(dto.Email);
            return StatusCode(429, new {
                message = "Konto midlertidigt låst på grund af for mange mislykkede login forsøg.",
                remainingLockoutSeconds = remainingSeconds
            });
        }

        // 2. Valider credentials
        var user = await _context.Users
            .Include(u => u.Role)
            .FirstOrDefaultAsync(u => u.Email == dto.Email);

        if (user == null || !BCrypt.Net.BCrypt.Verify(dto.Password, user.HashedPassword))
        {
            // 3. Registrer mislykket forsøg og påfør delay
            var delaySeconds = _loginAttemptService.RecordFailedAttempt(dto.Email);

            if (delaySeconds > 0)
            {
                await Task.Delay(TimeSpan.FromSeconds(delaySeconds));
                return Unauthorized(new {
                    message = "Forkert email eller adgangskode",
                    delayApplied = delaySeconds
                });
            }
        }

        // 4. Succesfuldt login - ryd fejl cache
        _loginAttemptService.RecordSuccessfulLogin(dto.Email);

        // ... fortsæt med normal login logik
    }
    catch (Exception ex)
    {
        _logger.LogError(ex, "Fejl ved login for email: {Email}", dto?.Email);
        return StatusCode(500, "Der opstod en intern serverfejl ved login");
    }
}
```

### HTTP Status Koder for Rate Limiting

- **429 Too Many Requests**: Når konto er låst eller for mange forsøg
- **401 Unauthorized**: Ved forkerte credentials (med delay information)

### Login Status Endpoint

Administratorer kan tjekke login status for en email:

```csharp
[Authorize(Roles = "Admin")]
[HttpGet("login-status/{email}")]
public IActionResult GetLoginStatus(string email)
{
    var attemptInfo = _loginAttemptService.GetLoginAttemptInfo(email);
    var isLockedOut = _loginAttemptService.IsLockedOut(email);

    return Ok(new {
        email = email,
        isLockedOut = isLockedOut,
        failedAttempts = attemptInfo?.FailedAttempts ?? 0,
        lastAttempt = attemptInfo?.LastAttempt,
        lockoutUntil = attemptInfo?.LockoutUntil,
        remainingLockoutSeconds = _loginAttemptService.GetRemainingLockoutSeconds(email)
    });
}
```

### LoginAttemptService

Servicen håndterer alle aspekter af login attempt tracking:

```csharp
public class LoginAttemptService
{
    // Tjek om email er låst
    public bool IsLockedOut(string email)

    // Registrer mislykket forsøg og returner delay
    public int RecordFailedAttempt(string email)

    // Ryd cache ved succesfuldt login
    public void RecordSuccessfulLogin(string email)

    // Hent resterende lockout tid
    public int GetRemainingLockoutSeconds(string email)

    // Hent detaljeret attempt info
    public LoginAttemptInfo? GetLoginAttemptInfo(string email)
}
```

### Sikkerhedsfordele

1. **Brute Force Protection**: Forhindrer automatiserede angreb
2. **Progressive Delays**: Gør angreb ineffektive og tidskrævende
3. **Temporary Lockouts**: Beskytter konti mod vedvarende angreb
4. **Logging**: Detaljeret sporing af alle sikkerhedshændelser
5. **Admin Oversight**: Administratorer kan overvåge mistænkelig aktivitet

### Monitoring og Alerts

Overvej at implementere alerts for:

- Mange mislykkede forsøg fra samme IP
- Gentagne lockouts for samme email
- Unormalt høj fejlrate på login endpoint
- Mistænkelige mønstre i login forsøg