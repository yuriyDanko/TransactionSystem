using DataLayer.Abstractions.Repositories;
using DataLayer.Contexts;
using Microsoft.EntityFrameworkCore;
using System;
using System.Threading.Tasks;
using Entities.Entities;
using Type = Entities.Entities.Type;

namespace DataLayer.Implementations
{
    public class TypeRepository : RepositoryBase<Entities.Entities.Type>, ITypeRepository
    {
        public TypeRepository(DatabaseContext dbContext) : base(dbContext)
        {
        }

        public async Task<Entities.Entities.Type> GetByName(string typeName)
        {
            return await DbSet.FirstOrDefaultAsync(t => t.Name == typeName);
        }

        public async Task<bool> IsExist(string typeName)
        {
            return await DbSet.AnyAsync(t => t.Name == typeName);
        }
    }
}