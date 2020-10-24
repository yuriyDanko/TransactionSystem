using BusinessLayer.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Status = BusinessLayer.Models.Status;

namespace BusinessLayer.Abstractions.Service
{
    public interface IStatusService
    {
        Task<bool> IsExist(string statusName);
        Task<Status> GetByName(string statusName);
        Task<Status> GetById(int id);
        Task<ICollection<Status>> GetAll();
    }
}
