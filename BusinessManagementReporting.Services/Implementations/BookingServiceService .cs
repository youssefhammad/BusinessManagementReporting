using AutoMapper;
using BusinessManagementReporting.Core.DTOs.BookingService;
using BusinessManagementReporting.Core.Interfaces;
using BusinessManagementReporting.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessManagementReporting.Services.Implementations
{
    public class BookingServiceService : IBookingServiceService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public BookingServiceService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<IEnumerable<BookingServiceDto>> GetAllBookingServicesAsync()
        {
            var bookingServices = await _unitOfWork.BookingServices.GetAllAsync();
            return _mapper.Map<IEnumerable<BookingServiceDto>>(bookingServices);
        }

        public async Task<BookingServiceDto> GetBookingServiceByIdAsync(int id)
        {
            var bookingService = await _unitOfWork.BookingServices.GetByIdAsync(id);
            if (bookingService == null)
                throw new Exception($"Booking Service with id {id} not found.");

            return _mapper.Map<BookingServiceDto>(bookingService);
        }

        public async Task<int> AddBookingServiceAsync(BookingServiceCreateDto bookingServiceDto)
        {
            var bookingService = _mapper.Map<Core.Entities.BookingService>(bookingServiceDto);

            await _unitOfWork.BookingServices.AddAsync(bookingService);
            await _unitOfWork.CompleteAsync();

            return bookingService.BookingServiceId;
        }

        public async Task UpdateBookingServiceAsync(int id, BookingServiceUpdateDto bookingServiceDto)
        {
            if (id != bookingServiceDto.BookingServiceId)
                throw new Exception($"Booking Service ID mismatch.");

            var bookingService = await _unitOfWork.BookingServices.GetByIdAsync(id);
            if (bookingService == null)
                throw new Exception($"Booking Service with id {id} not found.");

            _mapper.Map(bookingServiceDto, bookingService);

            _unitOfWork.BookingServices.Update(bookingService);
            await _unitOfWork.CompleteAsync();
        }

        public async Task DeleteBookingServiceAsync(int id)
        {
            var bookingService = await _unitOfWork.BookingServices.GetByIdAsync(id);
            if (bookingService == null)
                throw new Exception($"Booking Service with id {id} not found.");

            _unitOfWork.BookingServices.Remove(bookingService);
            await _unitOfWork.CompleteAsync();
        }
    }
}
