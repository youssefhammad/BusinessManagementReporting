using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessManagementReporting.Core.Mappings
{
    public static class AutoMapperConfig
    {
        public static MapperConfiguration Configure()
        {
            return new MapperConfiguration(cfg =>
            {
                cfg.AddProfile<BookingProfile>();
                cfg.AddProfile<BookingServiceProfile>();
                cfg.AddProfile<BranchProfile>();
                cfg.AddProfile<ClientProfile>();
                cfg.AddProfile<ServiceProfile>();
                cfg.AddProfile<TransactionProfile>();
            });
        }
    }
}
