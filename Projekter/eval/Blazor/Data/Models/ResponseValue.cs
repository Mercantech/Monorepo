namespace Blazor.Data.Models;

public class ResponseValue
{
    public string? Text { get; set; }
    public decimal? Number { get; set; }
    public DateTime? Date { get; set; }
    public bool? Boolean { get; set; }
    public List<string>? SelectedOptions { get; set; } // For multiple choice
    public string? SelectedOption { get; set; } // For single choice
    public Dictionary<string, object>? CustomData { get; set; } // For fremtidige udvidelser
}
