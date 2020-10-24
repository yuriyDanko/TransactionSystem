using BusinessLayer.Abstractions.Service;
using BusinessLayer.Models;
using Microsoft.VisualBasic;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Globalization;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using Type = BusinessLayer.Models.Type;

namespace BusinessLayer.ImplementationsServices
{
    public class CsvService : ICsvService
    {
        private readonly IClientService _clientService;
        private readonly ITypeService _typeService;
        private readonly IStatusService _statusService;

        public CsvService(IClientService clientService, ITypeService typeService, IStatusService statusService)
        {
            _clientService = clientService;
            _typeService = typeService;
            _statusService = statusService;
        }
        public async Task<ICollection<Transaction>> Parse(Stream stream)
        {
            var transactions = new Collection<Transaction>();

            using (var sreader = new StreamReader(stream))
            {
                //First line is header. If header is not passed in csv then we can neglect the below line.
                string[] headers = sreader.ReadLine().Split(',');
                //Loop through the records
                while (!sreader.EndOfStream)
                {
                    string line = await sreader.ReadLineAsync();
                    string[] rows = line.Split(',');
                    transactions.Add(new Transaction
                    {
                        TransactionId = int.Parse(rows[0].ToString()),
                        Status = await _statusService.GetByName(rows[1].ToString()),
                        Type = await _typeService.GetByName(rows[2].ToString()),
                        Client = await _clientService.CreateIfNotExist(rows[3].ToString().Split(' ')[0], rows[3].ToString().Split(' ')[1]),
                        Amount = decimal.Parse(rows[4].ToString().Remove(0, 1), NumberStyles.AllowDecimalPoint | NumberStyles.AllowLeadingSign, NumberFormatInfo.InvariantInfo)
                    });
                    
                }
            }
            return transactions;
        }
    }
}
