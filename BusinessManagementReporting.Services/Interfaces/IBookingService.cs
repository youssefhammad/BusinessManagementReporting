using BusinessManagementReporting.Core.DTOs.Booking;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessManagementReporting.Services.Interfaces
{
    public interface IBookingService
    {
        Task<IEnumerable<BookingDto>> GetAllBookingsAsync();
        Task<BookingDto> GetBookingByIdAsync(int id);
        Task<int> AddBookingAsync(BookingCreateDto bookingDto);
        Task UpdateBookingAsync(int id, BookingUpdateDto bookingDto);
        Task DeleteBookingAsync(int id);
    }
}
