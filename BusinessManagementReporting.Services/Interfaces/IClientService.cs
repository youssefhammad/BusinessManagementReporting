using BusinessManagementReporting.Core.DTOs.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessManagementReporting.Services.Interfaces
{
    public interface IClientService
    {
        Task<IEnumerable<ClientDto>> GetAllClientsAsync();
        Task<ClientDto> GetClientByIdAsync(int id);
        Task<int> AddClientAsync(ClientCreateDto clientDto);
        Task UpdateClientAsync(int id, ClientUpdateDto clientDto);
        Task DeleteClientAsync(int id);
    }
}
