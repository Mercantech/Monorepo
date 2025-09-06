using System.ComponentModel.DataAnnotations;

namespace Blazor.Data.Models;

public class Response
{
    public int Id { get; set; }
    
    [Required]
    public int SurveyId { get; set; }
    
    [StringLength(100)]
    public string? StudentName { get; set; } // Valgfrit navn
    
    [StringLength(200)]
    public string? StudentEmail { get; set; } // Valgfri email
    
    public DateTime SubmittedAt { get; set; } = DateTime.UtcNow;
    
    public string? IpAddress { get; set; } // For at undgå duplikater
    
    // Navigation properties
    public Survey Survey { get; set; } = null!;
    public ICollection<ResponseData> ResponseData { get; set; } = new List<ResponseData>();
}
