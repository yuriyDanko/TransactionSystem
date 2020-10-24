using DataLayer.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Type = DataLayer.Models.Type;

namespace DataLayer.Abstractions.Repositories
{
    public interface ITypeRepository : IRepository<Type>
    {
        Task<bool> IsExist(string typeName);
        Task<Type> GetByName(string typeName);

    }
}
