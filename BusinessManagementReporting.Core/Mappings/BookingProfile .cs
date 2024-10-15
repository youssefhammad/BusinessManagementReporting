using AutoMapper;
using BusinessManagementReporting.Core.DTOs.Booking;
using BusinessManagementReporting.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessManagementReporting.Core.Mappings
{
    public class BookingProfile : Profile
    {
        public BookingProfile()
        {
            CreateMap<Booking, BookingDto>();
            CreateMap<BookingCreateDto, Booking>();
            CreateMap<BookingUpdateDto, Booking>();
        }
    }
}
