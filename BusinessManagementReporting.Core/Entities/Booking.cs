using System.ComponentModel.DataAnnotations;

namespace BusinessManagementReporting.Core.Entities
{
    public class Booking
    {
        public int BookingId { get; set; }
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

        public Client Client { get; set; } = null!;
        public Branch Branch { get; set; } = null!;
        public ICollection<BookingService> BookingServices { get; set; } = new List<BookingService>();
        public ICollection<Transaction> Transactions { get; set; } = new List<Transaction>();
    }
}
