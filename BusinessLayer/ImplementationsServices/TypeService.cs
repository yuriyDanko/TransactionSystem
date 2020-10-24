using BusinessLayer.Abstractions.Service;
using BusinessLayer.Models;
using DataLayer.Abstractions.Repositories;
using DataLayer.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Threading.Tasks;
using Type = DataLayer.Models.Type;

namespace BusinessLayer.ImplementationsServices
{
    public class TypeService : ITypeService
    {
        private readonly ITypeRepository _typeRepository;
        public TypeService(ITypeRepository typeRepository)
        {
            _typeRepository = typeRepository;
        }
        public async Task<ICollection<Models.Type>> GetAll()
        {
            var typesDL = await _typeRepository.FindAllAsync();
            var typesBL = new Collection<Models.Type>();
            foreach(var type in typesDL)
            {
                typesBL.Add(ConvertDLToBL(type));
            }
            return typesBL;
        }

        public async Task<Models.Type> GetById(int id)
        {
            return ConvertDLToBL(await _typeRepository.GetById(id));
        }

        public async Task<Models.Type> GetByName(string typeName)
        {
            try
            {
                var typeDL = await _typeRepository.GetByName(typeName);
                return ConvertDLToBL(typeDL);
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }

        public async Task<bool> IsExist(string typeName)
        {
            return await _typeRepository.IsExist(typeName);
        }

        private Models.Type ConvertDLToBL(Type typeEntityToConvert)
        {
            return new Models.Type()
            {
                Id = typeEntityToConvert.Id,
                Name = typeEntityToConvert.Name
            };
        }
    }
}
