using BusinessLayer.Abstractions.Service;
using BusinessLayer.Models;
using DataLayer.Abstractions.Repositories;
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
        public async Task<ICollection<Models.Status>> GetAll()
        {
            var statusDL = await _statusRepository.FindAllAsync();
            var statusBL = new Collection<Models.Status>();
            foreach (var status in statusDL)
            {
                statusBL.Add(ConvertDLToBL(status));
            }
            return statusBL;
        }

        public async Task<Status> GetById(int id)
        {
            return ConvertDLToBL(await _statusRepository.GetById(id));
        }

        public async Task<Status> GetByName(string statusName)
        {
            try
            {
                var statusDL = await _statusRepository.GetByName(statusName);
                return ConvertDLToBL(statusDL);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<bool> IsExist(string statusName)
        {
            return await _statusRepository.IsExist(statusName);
        }
        private Models.Status ConvertDLToBL(DataLayer.Models.Status statusEntityToConvert)
        {
            return new Models.Status()
            {
                Id = statusEntityToConvert.Id,
                Name = statusEntityToConvert.Name
            };
        }
    }
}
