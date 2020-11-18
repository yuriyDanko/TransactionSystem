using BusinessLayer.Abstractions.Service;
using DataLayer.Abstractions.Repositories;
using Entities.Entities;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.ImplementationsServices
{
    public class StatusService : IStatusService
    {
        private readonly IStatusRepository _statusRepository;
        public StatusService(IStatusRepository statusRepository)
        {
            _statusRepository = statusRepository;
        }
        public async Task<ICollection<Status>> GetAll()
        {
            return await _statusRepository.FindAllAsync();
        }

        public async Task<Status> GetById(int id)
        {
            var status = await _statusRepository.GetById(id);
            if (status == null)
            {
                throw new Exception($"Status with id: {id} was not found");
            }
            return status;
        }

        public async Task<Status> GetByName(string statusName)
        {
            var status = await _statusRepository.GetByName(statusName);
            if (status == null)
            {
                throw new Exception($"Status with name: {statusName} was not found");
            }
            return status;
        }

        public async Task<bool> IsExist(string statusName)
        {
            return await _statusRepository.IsExist(statusName);
        }
       
    }
}
