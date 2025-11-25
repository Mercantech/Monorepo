using System;
using System.Collections.Generic;
using System.Linq;

namespace Blazor.Service;

public interface ITwoStarsWishBoard
{
    event Action? EntriesChanged;

    string? CurrentTheme { get; }

    IReadOnlyList<TwoStarsWishEntry> GetEntriesSnapshot();

    TwoStarsWishEntry AddEntry(string starOne, string starTwo, string wish, string? themeTag = null);

    void SetCurrentTheme(string? theme, bool resetEntries = false);

    void Clear();
}

public sealed class TwoStarsWishBoard : ITwoStarsWishBoard
{
    private const int MaxEntries = 250;

    private readonly object _syncRoot = new();
    private readonly List<TwoStarsWishEntry> _entries = new();

    public string? CurrentTheme { get; private set; }

    public event Action? EntriesChanged;

    public IReadOnlyList<TwoStarsWishEntry> GetEntriesSnapshot()
    {
        lock (_syncRoot)
        {
            // Returnér en kopi i omvendt rækkefølge (nyeste først)
            return _entries.AsEnumerable()
                .Reverse()
                .ToArray();
        }
    }

    public TwoStarsWishEntry AddEntry(string starOne, string starTwo, string wish, string? themeTag = null)
    {
        var trimmedStarOne = starOne?.Trim() ?? string.Empty;
        var trimmedStarTwo = starTwo?.Trim() ?? string.Empty;
        var trimmedWish = wish?.Trim() ?? string.Empty;
        var trimmedThemeTag = string.IsNullOrWhiteSpace(themeTag) ? null : themeTag.Trim();

        if (string.IsNullOrWhiteSpace(trimmedStarOne) &&
            string.IsNullOrWhiteSpace(trimmedStarTwo) &&
            string.IsNullOrWhiteSpace(trimmedWish))
        {
            throw new ArgumentException("Mindst én stjerne eller ønske skal udfyldes.", nameof(wish));
        }

        var entry = new TwoStarsWishEntry(
            Guid.NewGuid(),
            DateTimeOffset.UtcNow,
            trimmedStarOne,
            trimmedStarTwo,
            trimmedWish,
            trimmedThemeTag,
            CurrentTheme);

        lock (_syncRoot)
        {
            _entries.Add(entry);

            if (_entries.Count > MaxEntries)
            {
                _entries.RemoveAt(0);
            }
        }

        EntriesChanged?.Invoke();

        return entry;
    }

    public void SetCurrentTheme(string? theme, bool resetEntries = false)
    {
        lock (_syncRoot)
        {
            var normalizedTheme = string.IsNullOrWhiteSpace(theme) ? null : theme.Trim();

            if (string.Equals(CurrentTheme, normalizedTheme, StringComparison.Ordinal))
            {
                if (!resetEntries)
                {
                    return;
                }
            }

            CurrentTheme = normalizedTheme;

            if (resetEntries)
            {
                _entries.Clear();
            }
        }

        EntriesChanged?.Invoke();
    }

    public void Clear()
    {
        lock (_syncRoot)
        {
            if (_entries.Count == 0)
            {
                return;
            }

            _entries.Clear();
        }

        EntriesChanged?.Invoke();
    }
}

public readonly record struct TwoStarsWishEntry(
    Guid Id,
    DateTimeOffset CreatedAt,
    string StarOne,
    string StarTwo,
    string Wish,
    string? ThemeTag,
    string? ActiveTheme);

