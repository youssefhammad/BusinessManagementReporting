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

        Task<int> CompleteAsync();
    }
}
