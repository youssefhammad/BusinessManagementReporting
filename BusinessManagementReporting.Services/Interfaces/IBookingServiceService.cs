using BusinessManagementReporting.Core.DTOs.BookingService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessManagementReporting.Services.Interfaces
{
    public interface IBookingServiceService
    {
        Task<IEnumerable<BookingServiceDto>> GetAllBookingServicesAsync();
        Task<BookingServiceDto> GetBookingServiceByIdAsync(int id);
        Task<int> AddBookingServiceAsync(BookingServiceCreateDto bookingServiceDto);
        Task UpdateBookingServiceAsync(int id, BookingServiceUpdateDto bookingServiceDto);
        Task DeleteBookingServiceAsync(int id);
    }
}
