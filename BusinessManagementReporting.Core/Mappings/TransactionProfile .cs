using AutoMapper;
using BusinessManagementReporting.Core.DTOs.Transaction;
using BusinessManagementReporting.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessManagementReporting.Core.Mappings
{
    public class TransactionProfile : Profile
    {
        public TransactionProfile()
        {
            CreateMap<Transaction, TransactionDto>();
            CreateMap<TransactionCreateDto, Transaction>();
            CreateMap<TransactionUpdateDto, Transaction>();
        }
    }
}
