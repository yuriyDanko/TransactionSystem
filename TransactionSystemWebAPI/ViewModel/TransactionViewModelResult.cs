using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TransactionSystemWebAPI.ViewModel
{
    public class TransactionViewModelResult
    {
        public int TransactionId { get; set; }
        public string ClientName { get; set; }
        public string ClientSurname { get; set; }
        public string StatusName { get; set; }
        public string TypeName { get; set; }
        public decimal Amount { get; set; }
    }
}
