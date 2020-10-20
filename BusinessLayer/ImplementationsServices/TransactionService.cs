using BusinessLayer.Abstractions.Service;
using BusinessLayer.Models;
using DataLayer.Implementations;
using DataLayer.Abstractions.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Type = BusinessLayer.Models.Type;
using System.Collections.ObjectModel;

namespace BusinessLayer.ImplementationsServices
{
    public class TransactionService : ITransactionService
    {
        private readonly ITransactionRepository _transactionRepository;

        public TransactionService(ITransactionRepository transactionRepository)
        {
            _transactionRepository = transactionRepository;
        }
        public async Task AddListOfTransactionsToSystem(IList<Transaction> transactions)
        {
            foreach(var transaction in transactions)
            {
                await CreateAsync(transaction);
            }
        }

        public async Task CreateAsync(Transaction transaction)
        {
            await _transactionRepository.CreateAsync(ConvertTransactionFromBLToDL(transaction));
        }

        public async Task DeleteAsync(Transaction transaction)
        {
            await _transactionRepository.DeleteAsync(ConvertTransactionFromBLToDL(transaction));
        }

        public async Task<ICollection<Transaction>> LoadAllAsync()
        {
            var transactionsDL = await _transactionRepository.FindAllAsync();
            var transactionsBL = new Collection<Transaction>();
            foreach(var transaction in transactionsDL)
            {
                transactionsBL.Add(ConvertTransactionFromDLToBL(transaction));
            }
            return transactionsBL;
        }

        public async Task UpdateAsync(Transaction transaction)
        {
            await _transactionRepository.UpdateAsync(ConvertTransactionFromBLToDL(transaction));
        }

        private DataLayer.Models.Transaction ConvertTransactionFromBLToDL(Transaction transaction)
        {
            var transactionDataLayer = new DataLayer.Models.Transaction();
            transactionDataLayer.Id = transaction.Id;
            transactionDataLayer.Client = new DataLayer.Models.Client()
            {
                Id = transaction.Client.Id,
                Name = transaction.Client.Name,
                Surname = transaction.Client.Surname
            };
            transactionDataLayer.Type = new DataLayer.Models.Type()
            {
                Id = transaction.Type.Id,
                Name = transaction.Type.Name
            };
            transactionDataLayer.Status = new DataLayer.Models.Status()
            {
                Id = transaction.Status.Id,
                Name = transaction.Status.Name
            };
            transactionDataLayer.Amount = transaction.Amount;
            return transactionDataLayer;
        }

        private Transaction ConvertTransactionFromDLToBL(DataLayer.Models.Transaction transaction)
        {
            var transactionBusinessLayer = new Transaction();
            transactionBusinessLayer.Id = transaction.Id;
            transactionBusinessLayer.Client = new Client()
            {
                Id = transaction.Client.Id,
                Name = transaction.Client.Name,
                Surname = transaction.Client.Surname
            };
            transactionBusinessLayer.Type = new Type()
            {
                Id = transaction.Type.Id,
                Name = transaction.Type.Name
            };
            transactionBusinessLayer.Status = new Status()
            {
                Id = transaction.Status.Id,
                Name = transaction.Status.Name
            };
            transactionBusinessLayer.Amount = transaction.Amount;
            return transactionBusinessLayer;
        }
    }
}
