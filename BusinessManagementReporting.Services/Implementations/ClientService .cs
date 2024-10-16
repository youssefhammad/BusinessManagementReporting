using AutoMapper;
using BusinessManagementReporting.Core.DTOs.Client;
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
    public class ClientService : IClientService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public ClientService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<IEnumerable<ClientDto>> GetAllClientsAsync()
        {
            var clients = await _unitOfWork.Clients.GetAllAsync();
            return _mapper.Map<IEnumerable<ClientDto>>(clients);
        }

        public async Task<ClientDto> GetClientByIdAsync(int id)
        {
            var client = await _unitOfWork.Clients.GetByIdAsync(id);
            if (client == null)
                throw new Exception($"Client with id {id} not found.");

            return _mapper.Map<ClientDto>(client);
        }

        public async Task<int> AddClientAsync(ClientCreateDto clientDto)
        {
            var client = _mapper.Map<Client>(clientDto);

            await _unitOfWork.Clients.AddAsync(client);
            await _unitOfWork.CompleteAsync();

            return client.ClientId;
        }

        public async Task UpdateClientAsync(int id, ClientUpdateDto clientDto)
        {
            if (id != clientDto.ClientId)
                throw new Exception($"Client ID mismatch.");

            var client = await _unitOfWork.Clients.GetByIdAsync(id);
            if (client == null)
                throw new Exception($"Client with id {id} not found.");

            _mapper.Map(clientDto, client);

            _unitOfWork.Clients.Update(client);
            await _unitOfWork.CompleteAsync();
        }

        public async Task DeleteClientAsync(int id)
        {
            var client = await _unitOfWork.Clients.GetByIdAsync(id);
            if (client == null)
                throw new Exception($"Client with id {id} not found.");

            _unitOfWork.Clients.Remove(client);
            await _unitOfWork.CompleteAsync();
        }
    }
}
