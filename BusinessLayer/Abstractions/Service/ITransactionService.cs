using Entities.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Abstractions.Service
{
    public interface ITransactionService
    {
        Task<ICollection<Transaction>> LoadAllAsync();
        Task CreateAsync(Transaction transaction);
        Task AddListOfTransactionsToSystem(ICollection<Transaction> transactions);
        Task UpdateAsync(Transaction transaction);
        Task DeleteAsync(Transaction transaction);
        Task<Transaction> GetByTransactionId(int id);
        int GetCountTransactions();
    }
}
