using System.ComponentModel.DataAnnotations;

namespace Blazor.Data.Models;

public class Survey
{
    public int Id { get; set; }
    
    [Required]
    [StringLength(200)]
    public string Title { get; set; } = string.Empty;
    
    [StringLength(1000)]
    public string? Description { get; set; }
    
    [Required]
    [StringLength(4)]
    public string AccessCode { get; set; } = string.Empty;
    
    public bool IsActive { get; set; } = true;
    
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    
    public DateTime? ExpiresAt { get; set; }
    
    // Navigation properties
    public ICollection<Question> Questions { get; set; } = new List<Question>();
    public ICollection<Response> Responses { get; set; } = new List<Response>();
}
