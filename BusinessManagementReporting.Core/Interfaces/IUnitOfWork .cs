using BusinessManagementReporting.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessManagementReporting.Core.Interfaces
{
    public interface IUnitOfWork : IDisposable
    {
        IReportRepository ReportRepository { get; }
        IGenericRepository<Client> Clients { get; }
        IGenericRepository<Branch> Branches { get; }
        IGenericRepository<Service> Services { get; }
        IGenericRepository<Booking> Bookings { get; }
        IGenericRepository<Transaction> Transactions { get; }
        IGenericRepository<BookingService> BookingServices { get; }

        Task<int> CompleteAsync();
    }
}
