using Entities.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer.Abstractions.Repositories
{
    public interface IStatusRepository : IRepository<Status>
    {
        Task<bool> IsExist(string statusName);
        Task<Status> GetByName(string statusName);
    }
}
