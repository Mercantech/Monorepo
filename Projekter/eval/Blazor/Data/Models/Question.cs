using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json;

namespace Blazor.Data.Models;

public class Question
{
    public int Id { get; set; }

    [Required]
    public int SurveyId { get; set; }

    [Required]
    [StringLength(500)]
    public string Text { get; set; } = string.Empty;

    [StringLength(1000)]
    public string? Description { get; set; }

    [Required]
    public QuestionType Type { get; set; }

    public bool IsRequired { get; set; } = true;

    public int Order { get; set; }

    // For rating og scale spørgsmål
    public int? MinValue { get; set; }
    public int? MaxValue { get; set; }

    // For multiple choice og single choice
    public JsonDocument? Options { get; set; } // JSON document med valgmuligheder

    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    // Navigation properties
    [ForeignKey("SurveyId")]
    public Survey Survey { get; set; } = null!;

    public ICollection<ResponseData> ResponseData { get; set; } = new List<ResponseData>();
    
    // Conditions for forgrening
    public ICollection<QuestionCondition> Conditions { get; set; } = new List<QuestionCondition>();
    public ICollection<QuestionCondition> ParentConditions { get; set; } = new List<QuestionCondition>();
}
