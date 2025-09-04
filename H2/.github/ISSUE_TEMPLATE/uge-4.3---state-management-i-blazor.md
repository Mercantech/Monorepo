---
name: Uge 4.3 - State Management i Blazor
about: Uge 4.3 - State Management i Blazor
title: Uge 4.3 - State Management i Blazor
labels: ''
assignees: ''

---

## State Management i Blazor

- [ ] Læs pensum om State Management
- [ ] Implementér state management i jeres Blazor app:
  - [ ] Brug et built-in state management system eller lav jeres egen som tilgår local-storage i browseren
  - [ ] Håndter global state (fx user authentication)
- [ ] Test at state opdateres korrekt mellem komponenter
- [ ] Dokumentér jeres state management løsning

---

### Eksempel på state container

```csharp
public class AppState
{
    public event Action? OnChange;
    private User? _currentUser;

    public User? CurrentUser
    {
        get => _currentUser;
        set
        {
            _currentUser = value;
            NotifyStateChanged();
        }
    }

    private void NotifyStateChanged() => OnChange?.Invoke();
}
```
