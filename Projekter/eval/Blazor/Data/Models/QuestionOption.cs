namespace Blazor.Data.Models;

public class QuestionOption
{
    public string Id { get; set; } = string.Empty;
    public string Text { get; set; } = string.Empty;
    public int Order { get; set; }
    public bool IsOther { get; set; } = false; // For "Andet" muligheder
}
