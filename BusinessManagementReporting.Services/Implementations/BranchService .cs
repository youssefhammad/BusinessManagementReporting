using AutoMapper;
using BusinessManagementReporting.Core.DTOs.Branch;
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
    public class BranchService : IBranchService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public BranchService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<IEnumerable<BranchDto>> GetAllBranchesAsync()
        {
            var branches = await _unitOfWork.Branches.GetAllAsync();
            return _mapper.Map<IEnumerable<BranchDto>>(branches);
        }

        public async Task<BranchDto> GetBranchByIdAsync(int id)
        {
            var branch = await _unitOfWork.Branches.GetByIdAsync(id);
            if (branch == null)
                throw new Exception($"Branch with id {id} not found.");

            return _mapper.Map<BranchDto>(branch);
        }

        public async Task<int> AddBranchAsync(BranchCreateDto branchDto)
        {
            var branch = _mapper.Map<Branch>(branchDto);

            await _unitOfWork.Branches.AddAsync(branch);
            await _unitOfWork.CompleteAsync();

            return branch.BranchId;
        }

        public async Task UpdateBranchAsync(int id, BranchUpdateDto branchDto)
        {
            if (id != branchDto.BranchId)
                throw new Exception($"Branch ID mismatch.");

            var branch = await _unitOfWork.Branches.GetByIdAsync(id);
            if (branch == null)
                throw new Exception($"Branch with id {id} not found.");

            _mapper.Map(branchDto, branch);

            _unitOfWork.Branches.Update(branch);
            await _unitOfWork.CompleteAsync();
        }

        public async Task DeleteBranchAsync(int id)
        {
            var branch = await _unitOfWork.Branches.GetByIdAsync(id);
            if (branch == null)
                throw new Exception($"Branch with id {id} not found.");

            _unitOfWork.Branches.Remove(branch);
            await _unitOfWork.CompleteAsync();
        }
    }
}
