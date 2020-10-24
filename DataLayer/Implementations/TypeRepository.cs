using DataLayer.Abstractions.Repositories;
using DataLayer.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Type = DataLayer.Models.Type;

namespace DataLayer.Implementations
{
    public class TypeRepository : RepositoryBase<Models.Type>, ITypeRepository
    {
        public TypeRepository(DatabaseContext dbContext) : base(dbContext)
        {
        }

        public async Task<Type> GetByName(string typeName)
        {
            if (await IsExist(typeName))
            {
                return await DbContext.Set<Type>().AsNoTracking().FirstOrDefaultAsync(t => t.Name == typeName);
            }

            throw new Exception($"Type with name {typeName} not found");
            
        }

        public async Task<bool> IsExist(string typeName)
        {
            return await DbContext.Set<Type>().AnyAsync(t => t.Name == typeName);
        }
    }
}