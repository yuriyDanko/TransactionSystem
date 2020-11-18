using BusinessLayer.Abstractions.Service;
using Entities.Entities;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Globalization;
using System.IO;
using System.Threading.Tasks;

namespace BusinessLayer.ImplementationsServices
{
    public static class CsvHelper
    {    
        public static async Task<ICollection<Transaction>> Parse(Stream stream)
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
                        Status = new Status() { Name = rows[1].ToString()},
                        Type = new Type() { Name  = rows[2].ToString() },
                        Client = new Client() { Name = rows[3].ToString().Split(' ')[0], Surname = rows[3].ToString().Split(' ')[1] },
                        Amount = decimal.Parse(rows[4].ToString().Remove(0, 1), NumberStyles.AllowDecimalPoint | NumberStyles.AllowLeadingSign, NumberFormatInfo.InvariantInfo)
                    });
                    
                }
            }
            return transactions;
        }
    }
}
