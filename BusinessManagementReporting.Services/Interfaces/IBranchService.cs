using BusinessManagementReporting.Core.DTOs.Branch;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessManagementReporting.Services.Interfaces
{
    public interface IBranchService
    {
        Task<IEnumerable<BranchDto>> GetAllBranchesAsync();
        Task<BranchDto> GetBranchByIdAsync(int id);
        Task<int> AddBranchAsync(BranchCreateDto branchDto);
        Task UpdateBranchAsync(int id, BranchUpdateDto branchDto);
        Task DeleteBranchAsync(int id);
    }
}
