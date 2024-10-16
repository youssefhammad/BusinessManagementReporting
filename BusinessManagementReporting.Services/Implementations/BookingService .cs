using AutoMapper;
using BusinessManagementReporting.Core.DTOs.Booking;
using BusinessManagementReporting.Core.Entities;
using BusinessManagementReporting.Core.Interfaces;
using BusinessManagementReporting.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessManagementReporting.Services.Implementations
{
    public class BookingService : IBookingService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public BookingService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<IEnumerable<BookingDto>> GetAllBookingsAsync()
        {
            var bookings = await _unitOfWork.Bookings.GetAllAsync();
            return _mapper.Map<IEnumerable<BookingDto>>(bookings);
        }

        public async Task<BookingDto> GetBookingByIdAsync(int id)
        {
            var booking = await _unitOfWork.Bookings.GetByIdAsync(id);
            if (booking == null)
                throw new Exception($"Booking with id {id} not found.");

            return _mapper.Map<BookingDto>(booking);
        }

        public async Task<int> AddBookingAsync(BookingCreateDto bookingDto)
        {
            var booking = _mapper.Map<Booking>(bookingDto);

            await _unitOfWork.Bookings.AddAsync(booking);
            await _unitOfWork.CompleteAsync();

            return booking.BookingId;
        }

        public async Task UpdateBookingAsync(int id, BookingUpdateDto bookingDto)
        {
            if (id != bookingDto.BookingId)
                throw new Exception($"Booking ID mismatch.");

            var booking = await _unitOfWork.Bookings.GetByIdAsync(id);
            if (booking == null)
                throw new Exception($"Booking with id {id} not found.");

            _mapper.Map(bookingDto, booking);

            _unitOfWork.Bookings.Update(booking);
            await _unitOfWork.CompleteAsync();
        }

        public async Task DeleteBookingAsync(int id)
        {
            var booking = await _unitOfWork.Bookings.GetByIdAsync(id);
            if (booking == null)
                throw new Exception($"Booking with id {id} not found.");

            _unitOfWork.Bookings.Remove(booking);
            await _unitOfWork.CompleteAsync();
        }
    }
}
