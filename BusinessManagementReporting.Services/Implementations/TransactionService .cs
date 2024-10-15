using AutoMapper;
using BusinessManagementReporting.Core.DTOs.Transaction;
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
    public class TransactionService : ITransactionService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public TransactionService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<IEnumerable<TransactionDto>> GetAllTransactionsAsync()
        {
            var transactions = await _unitOfWork.Transactions.GetAllAsync();
            return _mapper.Map<IEnumerable<TransactionDto>>(transactions);
        }

        public async Task<TransactionDto> GetTransactionByIdAsync(int id)
        {
            var transaction = await _unitOfWork.Transactions.GetByIdAsync(id);
            if (transaction == null)
                throw new Exception($"Transaction with id {id} not found.");

            return _mapper.Map<TransactionDto>(transaction);
        }

        public async Task<int> AddTransactionAsync(TransactionCreateDto transactionDto)
        {
            var transaction = _mapper.Map<Transaction>(transactionDto);

            await _unitOfWork.Transactions.AddAsync(transaction);
            await _unitOfWork.CompleteAsync();

            return transaction.TransactionId;
        }

        public async Task UpdateTransactionAsync(int id, TransactionUpdateDto transactionDto)
        {
            if (id != transactionDto.TransactionId)
                throw new Exception($"Transaction ID mismatch.");

            var transaction = await _unitOfWork.Transactions.GetByIdAsync(id);
            if (transaction == null)
                throw new Exception($"Transaction with id {id} not found.");

            _mapper.Map(transactionDto, transaction);

            _unitOfWork.Transactions.Update(transaction);
            await _unitOfWork.CompleteAsync();
        }

        public async Task DeleteTransactionAsync(int id)
        {
            var transaction = await _unitOfWork.Transactions.GetByIdAsync(id);
            if (transaction == null)
                throw new Exception($"Transaction with id {id} not found.");

            _unitOfWork.Transactions.Remove(transaction);
            await _unitOfWork.CompleteAsync();
        }
    }
}
