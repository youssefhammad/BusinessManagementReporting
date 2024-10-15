using System.ComponentModel.DataAnnotations;

namespace BusinessManagementReporting.Core.Entities
{
    public class BookingService
    {
        public int BookingServiceId { get; set; }
        [Required]
        public int BookingId { get; set; }
        [Required]
        public int ServiceId { get; set; }
        [Required]
        public decimal Price { get; set; }
        public Booking Booking { get; set; } = null!;
        public Service Service { get; set; } = null!;
    }
}
