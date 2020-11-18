using BusinessLayer.Abstractions.Service;
using DataLayer.Abstractions.Repositories;
using Entities.Entities;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.ImplementationsServices
{
    public class ClientService : IClientService
    {
        private readonly IClientRepository _clientRepository;
        public ClientService(IClientRepository clientRepository)
        {
            _clientRepository = clientRepository;
        }
        public async Task AddClient(Client client)
        {
           await  _clientRepository.CreateAsync(client);
        }

        public async Task<Client> GetByNameAndSurname(string name, string surname)
        {
            var client = await _clientRepository.GetByNameAndSurname(name, surname);    
            if (client == null)
            {
                throw new Exception($"Client with name: {name} and surname: {surname} was not found");
            }
            return client;
        }

        public async Task<bool> IsExist(string name, string surname)
        {
            return await _clientRepository.IsExist(name, surname);
        }

        public async Task<Client> CreateIfNotExist(string name, string surname)
        {
            if(!await IsExist(name, surname))
            {
                await AddClient(new Client() { Name = name, Surname = surname });
            }
            return await GetByNameAndSurname(name, surname);
        }

        public async Task<Client> GetById(int id)
        {
            var client = await _clientRepository.GetById(id);
            if(client == null)
            {
                throw new Exception($"Client with id: {id} was not found");
            }
            return client;
        }
    }
}
