using BusinessManagementReporting.Core.DTOs.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessManagementReporting.Services.Interfaces
{
    public interface IServiceService
    {
        Task<IEnumerable<ServiceDto>> GetAllServicesAsync();
        Task<ServiceDto> GetServiceByIdAsync(int id);
        Task<int> AddServiceAsync(ServiceCreateDto serviceDto);
        Task UpdateServiceAsync(int id, ServiceUpdateDto serviceDto);
        Task DeleteServiceAsync(int id);
    }
}
