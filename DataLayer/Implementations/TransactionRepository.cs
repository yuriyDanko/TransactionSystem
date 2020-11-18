using DataLayer.Abstractions.Repositories;
using DataLayer.Contexts;
using Entities.Entities;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DataLayer.Implementations
{
    public class TransactionRepository : RepositoryBase<Transaction>, ITransactionRepository
    {
        public TransactionRepository(DatabaseContext dbContext) : base(dbContext)
        {
        }

        public async Task AddTransactions(ICollection<Transaction> transactions)
        {
            await DbSet.AddRangeAsync(transactions);
            await SaveChangesAsync();
        }

        public async Task<Transaction> GetByTransactionId(int id)
        {
            return await DbSet.FirstOrDefaultAsync(t => t.TransactionId == id);
        }

        public int GetCountOfRecords()
        {
            return DbSet.Count();
        }
    }
}
