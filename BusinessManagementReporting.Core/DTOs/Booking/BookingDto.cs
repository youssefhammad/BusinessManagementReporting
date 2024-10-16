using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessManagementReporting.Core.DTOs.Booking
{
    public class BookingDto
    {
        public int BookingId { get; set; }
        public int ClientId { get; set; }
        public int BranchId { get; set; }
        public DateTime BookingDate { get; set; }
        public TimeSpan BookingTime { get; set; }
        public string Status { get; set; } = null!;
    }
}
