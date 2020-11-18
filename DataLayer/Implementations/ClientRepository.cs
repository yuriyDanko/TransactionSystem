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
    public class ClientRepository : RepositoryBase<Client>, IClientRepository
    {
        public ClientRepository(DatabaseContext dbContext) : base(dbContext)
        {
        }

        public async Task<Client> GetByNameAndSurname(string name, string surname)
        {
           return await DbSet.FirstOrDefaultAsync(c => c.Name == name && c.Surname == surname);
        }

        public async Task<bool> IsExist(string name, string surname)
        {
            return await DbContext.Set<Client>().AnyAsync(c => c.Name == name && c.Surname == surname);
        }
    }
}
