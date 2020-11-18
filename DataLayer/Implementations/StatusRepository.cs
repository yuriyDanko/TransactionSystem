using DataLayer.Abstractions.Repositories;
using DataLayer.Contexts;
using Entities.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer.Implementations
{
    public class StatusRepository : RepositoryBase<Status>, IStatusRepository
    {
        public StatusRepository(DatabaseContext dbContext) : base(dbContext)
        {
        }

        public async Task<Status> GetByName(string statusName)
        {
           return await DbSet.FirstOrDefaultAsync(t => t.Name == statusName);  
        }

        public async Task<bool> IsExist(string statusName)
        {
           return await DbSet.AnyAsync(s => s.Name == statusName);
        }
    }
}
