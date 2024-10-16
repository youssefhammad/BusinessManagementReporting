using System.ComponentModel.DataAnnotations;

namespace BusinessManagementReporting.Core.DTOs.BookingService
{
    public class BookingServiceCreateDto
    {
        [Required]
        public int BookingId { get; set; }

        [Required]
        public int ServiceId { get; set; }

        [Required]
        public decimal Price { get; set; }
    }
}
