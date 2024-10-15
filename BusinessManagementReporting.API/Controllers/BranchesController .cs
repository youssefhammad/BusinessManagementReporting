using BusinessManagementReporting.Core.DTOs.Branch;
using BusinessManagementReporting.Core.DTOs.ResponseModel;
using BusinessManagementReporting.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace BusinessManagementReporting.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class BranchesController : ControllerBase
    {
        private readonly IBranchService _branchService;
        private readonly ILogger<BranchesController> _logger;

        public BranchesController(IBranchService branchService, ILogger<BranchesController> logger)
        {
            _branchService = branchService ?? throw new ArgumentNullException(nameof(branchService));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        [HttpGet]
        public async Task<ActionResult<ApiResponse<IEnumerable<BranchDto>>>> GetBranches()
        {
            _logger.LogInformation("Retrieving all branches");

            try
            {
                var branches = await _branchService.GetAllBranchesAsync();
                _logger.LogInformation("Successfully retrieved branches");
                return Ok(ApiResponse<IEnumerable<BranchDto>>.SuccessResponse(branches, "Branches retrieved successfully."));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while retrieving branches");
                return StatusCode(500, ApiResponse<IEnumerable<BranchDto>>.ErrorResponse("An error occurred while retrieving branches."));
            }
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ApiResponse<BranchDto>>> GetBranch(int id)
        {
            _logger.LogInformation("Retrieving branch with ID: {Id}", id);

            try
            {
                var branch = await _branchService.GetBranchByIdAsync(id);
                _logger.LogInformation("Successfully retrieved branch with ID: {Id}", id);
                return Ok(ApiResponse<BranchDto>.SuccessResponse(branch, "Branch retrieved successfully."));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while retrieving branch with ID: {Id}", id);
                return NotFound(ApiResponse<BranchDto>.ErrorResponse(ex.Message));
            }
        }

        [HttpPost]
        public async Task<ActionResult<ApiResponse<int>>> CreateBranch(BranchCreateDto branchDto)
        {
            _logger.LogInformation("Creating new branch");

            if (!ModelState.IsValid)
            {
                _logger.LogWarning("Invalid model state for branch creation");
                return BadRequest(ApiResponse<int>.ErrorResponse("Invalid data provided."));
            }

            try
            {
                var createdBranchId = await _branchService.AddBranchAsync(branchDto);
                _logger.LogInformation("Successfully created branch with ID: {Id}", createdBranchId);
                return CreatedAtAction(nameof(GetBranch), new { id = createdBranchId }, ApiResponse<int>.SuccessResponse(createdBranchId, "Branch created successfully."));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while creating branch");
                return StatusCode(500, ApiResponse<int>.ErrorResponse("An error occurred while creating the branch."));
            }
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<ApiResponse<string>>> UpdateBranch(int id, BranchUpdateDto branchDto)
        {
            _logger.LogInformation("Updating branch with ID: {Id}", id);

            if (id != branchDto.BranchId)
            {
                _logger.LogWarning("Branch ID mismatch in update request. Route ID: {RouteId}, DTO ID: {DtoId}", id, branchDto.BranchId);
                return BadRequest(ApiResponse<string>.ErrorResponse("Branch ID mismatch."));
            }

            if (!ModelState.IsValid)
            {
                _logger.LogWarning("Invalid model state for branch update");
                return BadRequest(ApiResponse<string>.ErrorResponse("Invalid data provided."));
            }

            try
            {
                await _branchService.UpdateBranchAsync(id, branchDto);
                _logger.LogInformation("Successfully updated branch with ID: {Id}", id);
                return Ok(ApiResponse<string>.SuccessResponse("Branch updated successfully."));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while updating branch with ID: {Id}", id);
                return NotFound(ApiResponse<string>.ErrorResponse(ex.Message));
            }
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<ApiResponse<string>>> DeleteBranch(int id)
        {
            _logger.LogInformation("Deleting branch with ID: {Id}", id);

            try
            {
                await _branchService.DeleteBranchAsync(id);
                _logger.LogInformation("Successfully deleted branch with ID: {Id}", id);
                return Ok(ApiResponse<string>.SuccessResponse("Branch deleted successfully."));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while deleting branch with ID: {Id}", id);
                return NotFound(ApiResponse<string>.ErrorResponse(ex.Message));
            }
        }
    }
}
