using Entities.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer.Abstractions.Repositories
{
    public interface IClientRepository : IRepository<Client>
    {
        Task<bool> IsExist(string name, string surname);
        Task<Client> GetByNameAndSurname(string name, string surname);

    }
}
