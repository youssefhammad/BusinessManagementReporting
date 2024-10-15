using BusinessManagementReporting.Core.DTOs.Transaction;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessManagementReporting.Services.Interfaces
{
    public interface ITransactionService
    {
        Task<IEnumerable<TransactionDto>> GetAllTransactionsAsync();
        Task<TransactionDto> GetTransactionByIdAsync(int id);
        Task<int> AddTransactionAsync(TransactionCreateDto transactionDto);
        Task UpdateTransactionAsync(int id, TransactionUpdateDto transactionDto);
        Task DeleteTransactionAsync(int id);
    }
}
