using System.ComponentModel.DataAnnotations;

namespace BusinessManagementReporting.Core.Entities
{
    public class Transaction
    {
        public int TransactionId { get; set; }
        [Required]
        public int BookingId { get; set; }
        [Required]
        public decimal Amount { get; set; }
        [Required]
        [StringLength(50)]
        public string PaymentMethod { get; set; } = null!;
        [Required]
        public DateTime PaymentDate { get; set; }

        public Booking Booking { get; set; } = null!;
    }
}
