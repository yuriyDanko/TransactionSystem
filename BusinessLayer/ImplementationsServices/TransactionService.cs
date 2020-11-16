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
        public async Task AddListOfTransactionsToSystem(ICollection<Transaction> transactions)
        {
            foreach(var transaction in transactions)
            {
                await CreateAsync(transaction);
            }


        }
        public async Task CreateAsync(Transaction transaction)
        {
            var en = ConvertTransactionFromBLToDL(transaction);
            await _transactionRepository.CreateAsync(en);
        }

        public async Task DeleteAsync(Transaction transaction)
        {
            await _transactionRepository.DeleteAsync(ConvertTransactionFromBLToDL(transaction));
        }

        public async Task<Transaction> GetByTransactionId(int id)
        {
            var transaction = await _transactionRepository.GetByTransactionId(id);
            if(transaction == null)
            {
                throw new Exception($"Transaction with id: {id} not found");
            }
            return ConvertTransactionFromDLToBL(await _transactionRepository.GetByTransactionId(id));
        }

        public int GetCountTransactions()
        {
            return _transactionRepository.GetCountOfRecords();
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
            transactionDataLayer.TransactionId = transaction.TransactionId;
            transactionDataLayer.ClientId = transaction.ClientId;
            transactionDataLayer.StatusId = transaction.StatusId;
            transactionDataLayer.TypeId = transaction.TypeId;
            transactionDataLayer.Amount = transaction.Amount;
            return transactionDataLayer;
        }

        private Transaction ConvertTransactionFromDLToBL(DataLayer.Models.Transaction transaction)
        {
            var transactionBusinessLayer = new Transaction();
            transactionBusinessLayer.Id = transaction.Id;
            transactionBusinessLayer.TransactionId = transaction.TransactionId;
            transactionBusinessLayer.ClientId = transaction.ClientId;
            transactionBusinessLayer.TypeId = transaction.TypeId;
            transactionBusinessLayer.StatusId = transaction.StatusId;
            transactionBusinessLayer.Amount = transaction.Amount;
            return transactionBusinessLayer;
        }
    }
}
