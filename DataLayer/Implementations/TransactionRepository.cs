using DataLayer.Abstractions.Repositories;
using DataLayer.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace DataLayer.Implementations
{
    public class TransactionRepository : RepositoryBase<Transaction>, ITransactionRepository
    {
        public TransactionRepository(DatabaseContext dbContext) : base(dbContext)
        {
        }
    }
}
