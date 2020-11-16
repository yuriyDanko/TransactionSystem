using BusinessLayer.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Abstractions.Service
{
    public interface IExcelService
    {
        Task<byte[]> BuildContentForExcelFile(ICollection<Transaction> transactions);
    }
}
