using BusinessLayer.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Type = BusinessLayer.Models.Type;

namespace BusinessLayer.Abstractions.Service
{
    public interface ITypeService
    {
        Task<bool> IsExist(string typeName);
        Task<Type> GetByName(string typeName);
        Task<Type> GetById(int id);
        Task<ICollection<Type>> GetAll();
    }
}
