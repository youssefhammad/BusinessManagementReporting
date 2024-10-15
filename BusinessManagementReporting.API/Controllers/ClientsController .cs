using BusinessManagementReporting.Core.DTOs.Client;
using BusinessManagementReporting.Core.DTOs.ResponseModel;
using BusinessManagementReporting.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace BusinessManagementReporting.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ClientsController : ControllerBase
    {
        private readonly IClientService _clientService;
        private readonly ILogger<ClientsController> _logger;

        public ClientsController(IClientService clientService, ILogger<ClientsController> logger)
        {
            _clientService = clientService ?? throw new ArgumentNullException(nameof(clientService));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        [HttpGet]
        public async Task<ActionResult<ApiResponse<IEnumerable<ClientDto>>>> GetClients()
        {
            _logger.LogInformation("Retrieving all clients");

            try
            {
                var clients = await _clientService.GetAllClientsAsync();
                _logger.LogInformation("Successfully retrieved clients");
                return Ok(ApiResponse<IEnumerable<ClientDto>>.SuccessResponse(clients, "Clients retrieved successfully."));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while retrieving clients");
                return StatusCode(500, ApiResponse<IEnumerable<ClientDto>>.ErrorResponse("An error occurred while retrieving clients."));
            }
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ApiResponse<ClientDto>>> GetClient(int id)
        {
            _logger.LogInformation("Retrieving client with ID: {Id}", id);

            try
            {
                var client = await _clientService.GetClientByIdAsync(id);
                _logger.LogInformation("Successfully retrieved client with ID: {Id}", id);
                return Ok(ApiResponse<ClientDto>.SuccessResponse(client, "Client retrieved successfully."));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while retrieving client with ID: {Id}", id);
                return NotFound(ApiResponse<ClientDto>.ErrorResponse(ex.Message));
            }
        }

        [HttpPost]
        public async Task<ActionResult<ApiResponse<int>>> CreateClient(ClientCreateDto clientDto)
        {
            _logger.LogInformation("Creating new client");

            if (!ModelState.IsValid)
            {
                _logger.LogWarning("Invalid model state for client creation");
                return BadRequest(ApiResponse<int>.ErrorResponse("Invalid data provided."));
            }

            try
            {
                var createdClientId = await _clientService.AddClientAsync(clientDto);
                _logger.LogInformation("Successfully created client with ID: {Id}", createdClientId);
                return CreatedAtAction(nameof(GetClient), new { id = createdClientId }, ApiResponse<int>.SuccessResponse(createdClientId, "Client created successfully."));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while creating client");
                return StatusCode(500, ApiResponse<int>.ErrorResponse("An error occurred while creating the client."));
            }
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<ApiResponse<string>>> UpdateClient(int id, ClientUpdateDto clientDto)
        {
            _logger.LogInformation("Updating client with ID: {Id}", id);

            if (id != clientDto.ClientId)
            {
                _logger.LogWarning("Client ID mismatch in update request. Route ID: {RouteId}, DTO ID: {DtoId}", id, clientDto.ClientId);
                return BadRequest(ApiResponse<string>.ErrorResponse("Client ID mismatch."));
            }

            if (!ModelState.IsValid)
            {
                _logger.LogWarning("Invalid model state for client update");
                return BadRequest(ApiResponse<string>.ErrorResponse("Invalid data provided."));
            }

            try
            {
                await _clientService.UpdateClientAsync(id, clientDto);
                _logger.LogInformation("Successfully updated client with ID: {Id}", id);
                return Ok(ApiResponse<string>.SuccessResponse("Client updated successfully."));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while updating client with ID: {Id}", id);
                return NotFound(ApiResponse<string>.ErrorResponse(ex.Message));
            }
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<ApiResponse<string>>> DeleteClient(int id)
        {
            _logger.LogInformation("Deleting client with ID: {Id}", id);

            try
            {
                await _clientService.DeleteClientAsync(id);
                _logger.LogInformation("Successfully deleted client with ID: {Id}", id);
                return Ok(ApiResponse<string>.SuccessResponse("Client deleted successfully."));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while deleting client with ID: {Id}", id);
                return NotFound(ApiResponse<string>.ErrorResponse(ex.Message));
            }
        }
    }
}
