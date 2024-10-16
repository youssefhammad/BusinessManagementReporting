using System.ComponentModel.DataAnnotations;

namespace BusinessManagementReporting.Core.DTOs.Branch
{
    public class BranchCreateDto
    {
        [Required]
        [StringLength(100)]
        public string Name { get; set; } = null!;

        [StringLength(200)]
        public string? Address { get; set; }

        [StringLength(100)]
        public string? City { get; set; }

        [StringLength(100)]
        public string? Country { get; set; }

        [StringLength(20)]
        public string? Phone { get; set; }
    }
}
