using DataLayer.Abstractions.Repositories;
using DataLayer.Models;
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
            if (await IsExist(statusName))
            {
                return await DbContext.Set<Status>().AsNoTracking().FirstOrDefaultAsync(t => t.Name == statusName);
            }

            throw new Exception($"Status with name {statusName} not found"); ;
        }

        public async Task<bool> IsExist(string statusName)
        {
           return await DbContext.Set<Status>().AnyAsync(s => s.Name == statusName);
        }
    }
}
