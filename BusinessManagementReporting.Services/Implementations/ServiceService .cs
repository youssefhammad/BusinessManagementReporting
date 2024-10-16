using AutoMapper;
using BusinessManagementReporting.Core.DTOs.Service;
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
    public class ServiceService : IServiceService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public ServiceService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<IEnumerable<ServiceDto>> GetAllServicesAsync()
        {
            var services = await _unitOfWork.Services.GetAllAsync();
            return _mapper.Map<IEnumerable<ServiceDto>>(services);
        }

        public async Task<ServiceDto> GetServiceByIdAsync(int id)
        {
            var service = await _unitOfWork.Services.GetByIdAsync(id);
            if (service == null)
                throw new Exception($"Service with id {id} not found.");

            return _mapper.Map<ServiceDto>(service);
        }

        public async Task<int> AddServiceAsync(ServiceCreateDto serviceDto)
        {
            var service = _mapper.Map<Service>(serviceDto);

            await _unitOfWork.Services.AddAsync(service);
            await _unitOfWork.CompleteAsync();

            return service.ServiceId;
        }

        public async Task UpdateServiceAsync(int id, ServiceUpdateDto serviceDto)
        {
            if (id != serviceDto.ServiceId)
                throw new Exception($"Service ID mismatch.");

            var service = await _unitOfWork.Services.GetByIdAsync(id);
            if (service == null)
                throw new Exception($"Service with id {id} not found.");

            _mapper.Map(serviceDto, service);

            _unitOfWork.Services.Update(service);
            await _unitOfWork.CompleteAsync();
        }

        public async Task DeleteServiceAsync(int id)
        {
            var service = await _unitOfWork.Services.GetByIdAsync(id);
            if (service == null)
                throw new Exception($"Service with id {id} not found.");

            _unitOfWork.Services.Remove(service);
            await _unitOfWork.CompleteAsync();
        }
    }
}
