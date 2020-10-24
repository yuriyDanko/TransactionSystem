using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TransactionSystemWebAPI.ViewModel
{
    public class TransactionViewModel
    {
        public int TransactionId { get; set; }
        public string ClientName { get; set; }
        public string ClientSurname { get; set; }
        public int StatusId { get; set; }
        public int TypeId { get; set; }
        public decimal Amount { get; set; }
    }
}
