using BusinessLayer.Abstractions.Service;
using BusinessLayer.Models;
using Microsoft.VisualBasic;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using Type = BusinessLayer.Models.Type;

namespace BusinessLayer.ImplementationsServices
{
    public class CsvService : ICsvService
    {
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
                        Id = int.Parse(rows[0].ToString()),
                        Status = new Status() { Name = rows[1].ToString() },
                        Type = new Type() { Name = rows[2].ToString() },
                        Client = new Client() { Name = rows[3].ToString().Split(' ')[0], Surname = rows[3].ToString().Split(' ')[1] },
                        Amount = int.Parse(rows[4].ToString())
                    });
                }
            }
            return transactions;
        }
    }
}
