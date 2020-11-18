using BusinessLayer.Abstractions.Service;
using DataLayer.Implementations;
using DataLayer.Abstractions.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.ObjectModel;
using Entities.Entities;

namespace BusinessLayer.ImplementationsServices
{
    public class TransactionService : ITransactionService
    {
        private readonly ITransactionRepository _transactionRepository;

        public TransactionService(ITransactionRepository transactionRepository)
        {
            _transactionRepository = transactionRepository;
        }
        public async Task AddListOfTransactionsToSystem(ICollection<Transaction> transactions)
        {
            await _transactionRepository.AddTransactions(transactions);
        }

        public async Task CreateAsync(Transaction transaction)
        {
            await _transactionRepository.CreateAsync(transaction);
        }

        public async Task DeleteAsync(Transaction transaction)
        {
            await _transactionRepository.DeleteAsync(transaction);
        }

        public async Task<Transaction> GetByTransactionId(int id)
        {
            var transaction = await _transactionRepository.GetByTransactionId(id);
            if (transaction == null)
            {
                throw new Exception($"Transaction with id: {id} was not found");
            }
            return transaction;
        }

        public int GetCountTransactions()
        {
            return _transactionRepository.GetCountOfRecords();
        }

        public async Task<ICollection<Transaction>> LoadAllAsync()
        {
            return await _transactionRepository.FindAllAsync();
        }

        public async Task UpdateAsync(Transaction transaction)
        {
            await _transactionRepository.UpdateAsync(transaction);
        }
    }
}
