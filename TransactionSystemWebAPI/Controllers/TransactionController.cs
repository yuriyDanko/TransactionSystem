using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using BusinessLayer.Abstractions.Service;
using TransactionSystemWebAPI.ViewModel;
using System.IO;
using System.Collections.ObjectModel;
using BusinessLayer.Models;
using System.Reflection.Metadata.Ecma335;

namespace TransactionSystemWebAPI.Controllers
{
    [Route("api/transactions")]
    
    public class TransactionController : Controller
    {
        private readonly ITransactionService _transactionService;
        private readonly ICsvService _csvService;
        public TransactionController(ITransactionService transactionService, ICsvService csvService)
        {
            _transactionService = transactionService;
            _csvService = csvService;
        }

        [HttpPost("leha")]
        public IActionResult look()
        {
            return Ok("soso");
        }

        [HttpPost("import")]
        public async Task<IActionResult> ImportCsv(IFormFile formFile)
        {
            CsvFileViewModel csvFileViewModel = new CsvFileViewModel() { FormFile = formFile};

            Console.WriteLine(1);
            if (csvFileViewModel.FormFile != null)
            {
                try
                {
                    string fileExtension = Path.GetExtension(csvFileViewModel.FormFile.FileName);

                    //Validate uploaded file and return error.
                    if (fileExtension != ".csv")
                    {
                        return BadRequest("Please select the csv file with .csv extension");
                    }


                    ICollection<Transaction> transactions;
                    using (var memoryStream = new MemoryStream())
                    {
                        await csvFileViewModel.FormFile.CopyToAsync(memoryStream);
                        transactions = await _csvService.Parse(memoryStream);
                        //await _transactionService.AddListOfTransactionsToSystem(transactions);

                    }

                    return Ok();
                }
                catch (Exception ex)
                {
                    return BadRequest("loh");
                }
            }
            else
            {
                return BadRequest("Please select the file first to upload.");
            }
           
        }
    }
}
