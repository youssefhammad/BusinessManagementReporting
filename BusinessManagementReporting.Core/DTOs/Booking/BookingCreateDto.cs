using System.ComponentModel.DataAnnotations;

namespace BusinessManagementReporting.Core.DTOs.Booking
{
    public class BookingCreateDto
    {
        [Required]
        public int ClientId { get; set; }

        [Required]
        public int BranchId { get; set; }

        [Required]
        public DateTime BookingDate { get; set; }

        [Required]
        public TimeSpan BookingTime { get; set; }

        [Required]
        [StringLength(20)]
        public string Status { get; set; } = null!;
    }
}
