using System.ComponentModel.DataAnnotations;

namespace BusinessManagementReporting.Core.Entities
{
    public class Client
    {
        public int ClientId { get; set; }
        [Required]
        [StringLength(100)]
        public string FirstName { get; set; } = null!;
        [Required]
        [StringLength(100)]
        public string LastName { get; set; } = null!;
        [Required]
        [StringLength(10)]
        public string Gender { get; set; } = null!;
        [Required]
        [StringLength(100)]
        public string Email { get; set; } = null!;
        [Required]
        [StringLength(20)]
        public string Phone { get; set; } = null!;
        [StringLength(200)]
        public string? Address { get; set; }
        [StringLength(100)]
        public string? City { get; set; }
        [StringLength(100)]
        public string? Country { get; set; }
        public DateTime Birthdate { get; set; }
        public ICollection<Booking> Bookings { get; set; } = new List<Booking>();
    }
}
