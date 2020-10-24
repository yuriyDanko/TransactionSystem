using BusinessLayer.Abstractions.Service;
using BusinessLayer.Models;
using DataLayer.Abstractions.Repositories;
using DataLayer.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using System.Threading.Tasks;
using Client = DataLayer.Models.Client;

namespace BusinessLayer.ImplementationsServices
{
    public class ClientService : IClientService
    {
        private readonly IClientRepository _clientRepository;
        public ClientService(IClientRepository clientRepository)
        {
            _clientRepository = clientRepository;
        }
        public async Task AddClient(Models.Client client)
        {
           await  _clientRepository.CreateAsync(ConvertBLToDL(client));
        }

        public async Task<Models.Client> GetByNameAndSurname(string name, string surname)
        {
            try
            {
                var clientDL = await _clientRepository.GetByNameAndSurname(name, surname);
                return ConvertDLToBL(clientDL);
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }

        public async Task<bool> IsExist(string name, string surname)
        {
            return await _clientRepository.IsExist(name, surname);
        }

        public async Task<Models.Client> CreateIfNotExist(string name, string surname)
        {
            if(!await IsExist(name, surname))
            {
                await AddClient(new Models.Client() { Name = name, Surname = surname });
            }
            return await GetByNameAndSurname(name, surname);
        }

        public Client ConvertBLToDL(Models.Client clientToConvert)
        {
            return new Client()
            {
                Id = clientToConvert.Id,
                Name = clientToConvert.Name,
                Surname = clientToConvert.Surname
            };
        }

        public Models.Client ConvertDLToBL(Client clientToConvert)
        {
            return new Models.Client()
            {
                Id = clientToConvert.Id,
                Name = clientToConvert.Name,
                Surname = clientToConvert.Surname
            };
        }

        public async Task<Models.Client> GetById(int id)
        {
            return ConvertDLToBL(await _clientRepository.GetById(id));
        }
    }
}
