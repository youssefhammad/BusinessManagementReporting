using AutoMapper;
using BusinessManagementReporting.Core.DTOs.BookingService;
using BusinessManagementReporting.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessManagementReporting.Core.Mappings
{
    public class BookingServiceProfile : Profile
    {
        public BookingServiceProfile()
        {
            CreateMap<BookingService, BookingServiceDto>();
            CreateMap<BookingServiceCreateDto, BookingService>();
            CreateMap<BookingServiceUpdateDto, BookingService>();
        }
    }
}
