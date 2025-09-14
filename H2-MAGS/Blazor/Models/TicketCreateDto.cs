using System.ComponentModel.DataAnnotations;

namespace Blazor.Models
{
    /// <summary>
    /// DTO for oprettelse af nyt ticket
    /// </summary>
    public class TicketCreateDto
    {
        [Required(ErrorMessage = "Titel er påkrævet")]
        [StringLength(200, ErrorMessage = "Titel må maksimalt være 200 tegn")]
        public string Title { get; set; } = string.Empty;

        [Required(ErrorMessage = "Beskrivelse er påkrævet")]
        [StringLength(2000, ErrorMessage = "Beskrivelse må maksimalt være 2000 tegn")]
        public string Description { get; set; } = string.Empty;

        [Required(ErrorMessage = "Service type er påkrævet")]
        public string ServiceType { get; set; } = string.Empty;

        [Required(ErrorMessage = "Kategori er påkrævet")]
        public string Category { get; set; } = string.Empty;

        public string? SubCategory { get; set; }

        [Required(ErrorMessage = "Prioritet er påkrævet")]
        public string Priority { get; set; } = "Medium";

        public string? BookingId { get; set; }
        public string? RoomId { get; set; }
        public string? HotelId { get; set; }
    }
}
