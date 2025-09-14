namespace Blazor.Models
{
    /// <summary>
    /// DTO for hentning af ticket information
    /// </summary>
    public class TicketGetDto
    {
        public string Id { get; set; } = string.Empty;
        public string TicketNumber { get; set; } = string.Empty;
        public string Title { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public string ServiceType { get; set; } = string.Empty;
        public string Priority { get; set; } = string.Empty;
        public string Status { get; set; } = string.Empty;
        public string Category { get; set; } = string.Empty;
        public string SubCategory { get; set; } = string.Empty;
        public string RiskLevel { get; set; } = string.Empty;
        public string Impact { get; set; } = string.Empty;
        
        // Requester information
        public string RequesterId { get; set; } = string.Empty;
        public string? RequesterEmail { get; set; }
        public string? RequesterUsername { get; set; }
        
        // Assignee information
        public string? AssigneeId { get; set; }
        public string? AssigneeEmail { get; set; }
        public string? AssigneeUsername { get; set; }
        
        // Related entities
        public string? BookingId { get; set; }
        public string? RoomId { get; set; }
        public string? RoomNumber { get; set; }
        public string? HotelId { get; set; }
        public string? HotelName { get; set; }
        
        // Timestamps
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        public DateTime? DueDate { get; set; }
        public DateTime? ResolvedAt { get; set; }
        public DateTime? ClosedAt { get; set; }
        
        // Resolution
        public string? Resolution { get; set; }
        public string? WorkNotes { get; set; }
        
        // Comments count
        public int CommentsCount { get; set; }
        public int AttachmentsCount { get; set; }
    }
}
