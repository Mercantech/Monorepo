using System.IO;
using System.Text;

namespace MSTest_Opgaver;

/// <summary>
/// Hjælper til at fange Console-output (og evt. sende indtastning til Console.ReadLine)
/// så vi kan teste opgaverne uden at ændre selve opgavemetoderne.
/// </summary>
public static class TestHelper
{
    /// <summary>
    /// Lås så kun én test ad gangen bruger Console (undgår blandet output og ObjectDisposedException).
    /// </summary>
    private static readonly object ConsoleLock = new();

    /// <summary>
    /// Kører en action og returnerer alt, der blev skrevet til Console.Out.
    /// </summary>
    public static string CaptureOutput(Action action)
    {
        lock (ConsoleLock)
        {
            var previousOut = Console.Out;
            try
            {
                var writer = new StringWriter(new StringBuilder());
                Console.SetOut(writer);
                action();
                return writer.ToString();
            }
            finally
            {
                Console.SetOut(previousOut);
            }
        }
    }

    /// <summary>
    /// Sætter Console.In til de angivne linjer (som brugeren ville taste),
    /// kører action, og returnerer alt der blev skrevet til Console.Out.
    /// Hver linje svarer til ét kald til Console.ReadLine().
    /// </summary>
    public static string CaptureOutputWithInput(Action action, params string[] inputLines)
    {
        lock (ConsoleLock)
        {
            var previousIn = Console.In;
            var previousOut = Console.Out;
            try
            {
                var input = string.Join(Environment.NewLine, inputLines);
                Console.SetIn(new StringReader(input));
                var writer = new StringWriter(new StringBuilder());
                Console.SetOut(writer);
                action();
                return writer.ToString();
            }
            finally
            {
                Console.SetIn(previousIn);
                Console.SetOut(previousOut);
            }
        }
    }

    /// <summary>
    /// Henter de sidste N ikke-tomme linjer fra output – det er typisk elevens svar (efter opgaveteksten).
    /// </summary>
    public static string[] GetStudentOutputLines(string output, int maxLines = 5)
    {
        var lines = output.Split(["\r\n", "\r", "\n"], StringSplitOptions.None)
            .Select(l => l.Trim())
            .Where(l => l.Length > 0)
            .ToArray();
        if (lines.Length == 0) return [];
        var take = Math.Min(maxLines, lines.Length);
        return lines[^take..];
    }

    /// <summary>
    /// Sidste ikke-tomme linje – ofte det elevens kode har udskrevet.
    /// </summary>
    public static string? GetLastStudentLine(string output)
    {
        var student = GetStudentOutputLines(output, 1);
        return student.Length > 0 ? student[0] : null;
    }

    /// <summary>
    /// Tjekker om der findes en linje (hele linjen) der matcher præcis én af værdierne.
    /// </summary>
    public static bool HasExactLine(string output, params string[] exactValues)
    {
        var lines = output.Split(["\r\n", "\r", "\n"], StringSplitOptions.None)
            .Select(l => l.Trim())
            .ToArray();
        foreach (var line in lines)
            if (exactValues.Contains(line))
                return true;
        return false;
    }

    /// <summary>
    /// Bygger en guide-besked til elev: hvad forventedes, hvad fik vi, og evt. tip.
    /// </summary>
    public static string GuideMessage(string opgaveNavn, string forventet, string hvadDuHar, string? tip = null)
    {
        var sb = new StringBuilder();
        sb.AppendLine($"[{opgaveNavn}]");
        sb.AppendLine($"  Forventet: {forventet}");
        sb.AppendLine($"  Din output: {(string.IsNullOrWhiteSpace(hvadDuHar) ? "(ingen output eller tom)" : hvadDuHar.Trim())}");
        if (!string.IsNullOrWhiteSpace(tip))
            sb.AppendLine($"  Tip: {tip}");
        return sb.ToString();
    }
}
