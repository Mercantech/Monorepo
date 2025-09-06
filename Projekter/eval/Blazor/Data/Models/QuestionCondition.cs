using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Blazor.Data.Models;

public class QuestionCondition
{
    public int Id { get; set; }

    [Required]
    public int QuestionId { get; set; } // Spørgsmålet der skal vises

    [Required]
    public int ParentQuestionId { get; set; } // Spørgsmålet der skal evalueres

    [Required]
    public ConditionType Type { get; set; }

    [Required]
    public string Value { get; set; } = string.Empty; // Værdi at sammenligne med

    public string? Operator { get; set; } // "equals", "greater_than", "less_than", "contains", etc.

    public int Order { get; set; } // Rækkefølge hvis flere conditions

    public bool IsActive { get; set; } = true;

    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    // Navigation properties
    [ForeignKey("QuestionId")]
    public Question Question { get; set; } = null!;

    [ForeignKey("ParentQuestionId")]
    public Question ParentQuestion { get; set; } = null!;
}

public enum ConditionType
{
    RatingEquals,        // Rating = specifik værdi
    RatingGreaterThan,   // Rating > værdi
    RatingLessThan,      // Rating < værdi
    RatingBetween,       // Rating mellem to værdier
    TextContains,        // Tekst indeholder
    TextEquals,          // Tekst er lig med
    MultipleChoiceSelected, // Flervalgs spørgsmål har valgt specifik option
    SingleChoiceSelected,   // Enkeltvalgs spørgsmål har valgt specifik option
    YesNoSelected,       // Ja/Nej spørgsmål har valgt specifik værdi
    NumberEquals,        // Tal er lig med
    NumberGreaterThan,   // Tal er større end
    NumberLessThan,      // Tal er mindre end
    DateAfter,           // Dato er efter
    DateBefore,          // Dato er før
    DateBetween          // Dato er mellem to værdier
}
