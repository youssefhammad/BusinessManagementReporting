using BusinessManagementReporting.Core.Entities;
using BusinessManagementReporting.Core.Interfaces;
using BusinessManagementReporting.Infrastructure.Data;
using BusinessManagementReporting.Infrastructure.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessManagementReporting.Infrastructure.UnitOfWork
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ApplicationDbContext _context;

        private IReportRepository _reportRepository;
        private IGenericRepository<Client> _clients;
        private IGenericRepository<Branch> _branches;
        private IGenericRepository<Service> _services;
        private IGenericRepository<Booking> _bookings;
        private IGenericRepository<Transaction> _transactions;
        private IGenericRepository<BookingService> _bookingServices;

        public UnitOfWork(ApplicationDbContext context)
        {
            _context = context;
        }

        public IReportRepository ReportRepository => _reportRepository ??= new ReportRepository(_context);
        public IGenericRepository<Client> Clients => _clients ??= new GenericRepository<Client>(_context);
        public IGenericRepository<Branch> Branches => _branches ??= new GenericRepository<Branch>(_context);
        public IGenericRepository<Service> Services => _services ??= new GenericRepository<Service>(_context);
        public IGenericRepository<Booking> Bookings => _bookings ??= new GenericRepository<Booking>(_context);
        public IGenericRepository<Transaction> Transactions => _transactions ??= new GenericRepository<Transaction>(_context);
        public IGenericRepository<BookingService> BookingServices => _bookingServices ??= new GenericRepository<BookingService>(_context);

        public async Task<int> CompleteAsync()
        {
            return await _context.SaveChangesAsync();
        }

        public void Dispose()
        {
            _context.Dispose();
        }
    }
}
