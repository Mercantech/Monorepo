using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Blazor.Data.Models;

public class ResponseData
{
    public int Id { get; set; }
    
    [Required]
    public int ResponseId { get; set; }
    
    [Required]
    public int QuestionId { get; set; }
    
    // Dynamisk data gemt som JSON
    [Required]
    [Column(TypeName = "jsonb")]
    public string Data { get; set; } = string.Empty;
    
    // For nemmere søgning og filtrering
    [StringLength(1000)]
    public string? TextValue { get; set; } // For tekst svar
    
    public decimal? NumericValue { get; set; } // For tal svar
    
    public DateTime? DateValue { get; set; } // For dato svar
    
    public bool? BooleanValue { get; set; } // For ja/nej svar
    
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    
    // Navigation properties
    [ForeignKey("ResponseId")]
    public Response Response { get; set; } = null!;
    
    [ForeignKey("QuestionId")]
    public Question Question { get; set; } = null!;
}
