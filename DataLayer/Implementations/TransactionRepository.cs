using DataLayer.Abstractions.Repositories;
using DataLayer.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer.Implementations
{
    public class TransactionRepository : RepositoryBase<Transaction>, ITransactionRepository
    {
        public TransactionRepository(DatabaseContext dbContext) : base(dbContext)
        {
        }

        public async Task<Transaction> GetByTransactionId(int id)
        {
            return await DbContext.Set<Transaction>().AsNoTracking().FirstOrDefaultAsync(t => t.TransactionId == id);
        }

        public int GetCountOfRecords()
        {
            return DbContext.Set<Transaction>().Count();
        }
    }
}
