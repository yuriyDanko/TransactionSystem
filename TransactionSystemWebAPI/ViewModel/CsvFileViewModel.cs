using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TransactionSystemWebAPI.ViewModel
{
    public class CsvFileViewModel
    {
        public IFormFile FormFile { get; set; }
    }
}
