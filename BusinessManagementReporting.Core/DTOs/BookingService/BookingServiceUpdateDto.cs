using System.ComponentModel.DataAnnotations;

namespace BusinessManagementReporting.Core.DTOs.BookingService
{
    public class BookingServiceUpdateDto
    {
        public int BookingServiceId { get; set; }

        [Required]
        public int BookingId { get; set; }

        [Required]
        public int ServiceId { get; set; }

        [Required]
        public decimal Price { get; set; }
    }
}
