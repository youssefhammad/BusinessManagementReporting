using System.ComponentModel.DataAnnotations;

namespace BusinessManagementReporting.Core.DTOs.Service
{
    public class ServiceCreateDto
    {
        [Required]
        [StringLength(100)]
        public string Name { get; set; } = null!;

        [StringLength(500)]
        public string? Description { get; set; }

        [Required]
        public decimal Price { get; set; }

        [Required]
        public int Duration { get; set; }
    }
}
