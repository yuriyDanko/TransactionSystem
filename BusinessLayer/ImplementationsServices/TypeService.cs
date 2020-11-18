using BusinessLayer.Abstractions.Service;
using DataLayer.Abstractions.Repositories;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Type = Entities.Entities.Type;

namespace BusinessLayer.ImplementationsServices
{
    public class TypeService : ITypeService
    {
        private readonly ITypeRepository _typeRepository;
        public TypeService(ITypeRepository typeRepository)
        {
            _typeRepository = typeRepository;
        }
        public async Task<ICollection<Type>> GetAll()
        {
            return await _typeRepository.FindAllAsync();
        }

        public async Task<Type> GetById(int id)
        {
            var type = await _typeRepository.GetById(id);
            if (type == null)
            {
                throw new Exception($"Type with id: {id} was not found");
            }
            return type;
        }

        public async Task<Type> GetByName(string typeName)
        {
           
             var type = await _typeRepository.GetByName(typeName);
             if (type == null)
             {
                 throw new Exception($"Type with name: {typeName} was not found");
             }
              return type;
        }

        public async Task<bool> IsExist(string typeName)
        {
            return await _typeRepository.IsExist(typeName);
        }
    }
}
