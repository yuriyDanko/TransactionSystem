using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Entities.Entities;
using Type = Entities.Entities.Type;

namespace DataLayer.Abstractions.Repositories
{
    public interface ITypeRepository : IRepository<Type>
    {
        Task<bool> IsExist(string typeName);
        Task<Type> GetByName(string typeName);

    }
}
