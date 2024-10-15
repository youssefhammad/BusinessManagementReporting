using System.ComponentModel.DataAnnotations;

namespace BusinessManagementReporting.Core.DTOs.Transaction
{
    public class TransactionCreateDto
    {
        [Required]
        public int BookingId { get; set; }

        [Required]
        public decimal Amount { get; set; }

        [Required]
        [StringLength(50)]
        public string PaymentMethod { get; set; } = null!;

        [Required]
        public DateTime PaymentDate { get; set; }
    }
}
