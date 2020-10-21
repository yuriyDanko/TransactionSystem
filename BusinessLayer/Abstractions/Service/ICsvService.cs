using BusinessLayer.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Abstractions.Service
{
    public interface ICsvService
    {
        Task<ICollection<Transaction>> Parse(Stream stream);
    }
}
