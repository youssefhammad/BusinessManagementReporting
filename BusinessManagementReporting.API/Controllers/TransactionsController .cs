using BusinessManagementReporting.Core.DTOs.ResponseModel;
using BusinessManagementReporting.Core.DTOs.Transaction;
using BusinessManagementReporting.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BusinessManagementReporting.API.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class TransactionsController : ControllerBase
    {
        private readonly ITransactionService _transactionService;
        private readonly ILogger<TransactionsController> _logger;

        public TransactionsController(ITransactionService transactionService, ILogger<TransactionsController> logger)
        {
            _transactionService = transactionService ?? throw new ArgumentNullException(nameof(transactionService));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        [HttpGet]
        public async Task<ActionResult<ApiResponse<IEnumerable<TransactionDto>>>> GetTransactions()
        {
            _logger.LogInformation("Retrieving all transactions");

            try
            {
                var transactions = await _transactionService.GetAllTransactionsAsync();
                _logger.LogInformation("Successfully retrieved transactions");
                return Ok(ApiResponse<IEnumerable<TransactionDto>>.SuccessResponse(transactions, "Transactions retrieved successfully."));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while retrieving transactions");
                return StatusCode(500, ApiResponse<IEnumerable<TransactionDto>>.ErrorResponse("An error occurred while retrieving transactions."));
            }
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ApiResponse<TransactionDto>>> GetTransaction(int id)
        {
            _logger.LogInformation("Retrieving transaction with ID: {Id}", id);

            try
            {
                var transaction = await _transactionService.GetTransactionByIdAsync(id);
                _logger.LogInformation("Successfully retrieved transaction with ID: {Id}", id);
                return Ok(ApiResponse<TransactionDto>.SuccessResponse(transaction, "Transaction retrieved successfully."));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while retrieving transaction with ID: {Id}", id);
                return NotFound(ApiResponse<TransactionDto>.ErrorResponse(ex.Message));
            }
        }

        [HttpPost]
        public async Task<ActionResult<ApiResponse<int>>> CreateTransaction(TransactionCreateDto transactionDto)
        {
            _logger.LogInformation("Creating new transaction");

            if (!ModelState.IsValid)
            {
                _logger.LogWarning("Invalid model state for transaction creation");
                return BadRequest(ApiResponse<int>.ErrorResponse("Invalid data provided."));
            }

            try
            {
                var createdTransactionId = await _transactionService.AddTransactionAsync(transactionDto);
                _logger.LogInformation("Successfully created transaction with ID: {Id}", createdTransactionId);
                return CreatedAtAction(nameof(GetTransaction), new { id = createdTransactionId }, ApiResponse<int>.SuccessResponse(createdTransactionId, "Transaction created successfully."));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while creating transaction");
                return StatusCode(500, ApiResponse<int>.ErrorResponse("An error occurred while creating the transaction."));
            }
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<ApiResponse<string>>> UpdateTransaction(int id, TransactionUpdateDto transactionDto)
        {
            _logger.LogInformation("Updating transaction with ID: {Id}", id);

            if (id != transactionDto.TransactionId)
            {
                _logger.LogWarning("Transaction ID mismatch in update request. Route ID: {RouteId}, DTO ID: {DtoId}", id, transactionDto.TransactionId);
                return BadRequest(ApiResponse<string>.ErrorResponse("Transaction ID mismatch."));
            }

            if (!ModelState.IsValid)
            {
                _logger.LogWarning("Invalid model state for transaction update");
                return BadRequest(ApiResponse<string>.ErrorResponse("Invalid data provided."));
            }

            try
            {
                await _transactionService.UpdateTransactionAsync(id, transactionDto);
                _logger.LogInformation("Successfully updated transaction with ID: {Id}", id);
                return Ok(ApiResponse<string>.SuccessResponse("Transaction updated successfully."));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while updating transaction with ID: {Id}", id);
                return NotFound(ApiResponse<string>.ErrorResponse(ex.Message));
            }
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<ApiResponse<string>>> DeleteTransaction(int id)
        {
            _logger.LogInformation("Deleting transaction with ID: {Id}", id);

            try
            {
                await _transactionService.DeleteTransactionAsync(id);
                _logger.LogInformation("Successfully deleted transaction with ID: {Id}", id);
                return Ok(ApiResponse<string>.SuccessResponse("Transaction deleted successfully."));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while deleting transaction with ID: {Id}", id);
                return NotFound(ApiResponse<string>.ErrorResponse(ex.Message));
            }
        }
    }
}
