using BusinessLayer.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Abstractions.Service
{
    public interface IClientService
    {
        Task<bool> IsExist(string name, string surname);
        Task<Client> GetByNameAndSurname(string name, string surname);
        Task<Client> GetById(int id);
        Task AddClient(Client client);
        Task<Client> CreateIfNotExist(string name, string surname);
    }
}
