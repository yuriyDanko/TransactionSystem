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
using ClosedXML.Excel;

namespace TransactionSystemWebAPI.Controllers
{
    [Route("api/transactions")]
    
    public class TransactionController : Controller
    {
        private readonly ITransactionService _transactionService;
        private readonly IClientService _clientService;
        private readonly ITypeService _typeService;
        private readonly IStatusService _statusService;
        private readonly ICsvService _csvService;
        public TransactionController(ITransactionService transactionService, ICsvService csvService,
            IClientService clientService, IStatusService statusService, ITypeService typeService)
        {
            _transactionService = transactionService;
            _clientService = clientService;
            _statusService = statusService;
            _typeService = typeService;
            _csvService = csvService;
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] TransactionViewModel transactionViewModel)
        {
            if(await _statusService.GetById(transactionViewModel.StatusId) == null)
            {
                return BadRequest("Invalid status name");
            }
            if (await _typeService.GetById(transactionViewModel.TypeId) == null)
            {
                return BadRequest("Invalid type name");
            }
            var client = await _clientService.CreateIfNotExist(transactionViewModel.ClientName, transactionViewModel.ClientSurname);
            var transaction = new Transaction()
            {
                TransactionId = _transactionService.GetCountTransactions() + 1,
                ClientId = client.Id,
                TypeId = transactionViewModel.TypeId,
                StatusId = transactionViewModel.StatusId,
                Amount = transactionViewModel.Amount,
            };
            await _transactionService.CreateAsync(transaction);
            return StatusCode(201);
        }

        [HttpDelete]
        public async Task<IActionResult> Delete(int id)
        {
            var transaction = await _transactionService.GetByTransactionId(id);
            await _transactionService.DeleteAsync(transaction);
            return Ok();
        }

        [HttpPut]
        public async Task<IActionResult> Update(int id, TransactionViewModel transactionViewModel)
        {
            var transaction = await _transactionService.GetByTransactionId(id);
            transaction.StatusId = transactionViewModel.StatusId;
            transaction.TypeId = transactionViewModel.TypeId;
            transaction.Amount = transactionViewModel.Amount;
            await _transactionService.UpdateAsync(transaction);
            return Ok();
        }

        [HttpPost("import")]
        public async Task<IActionResult> ImportCsv([FromForm]CsvFileViewModel csvFileViewModel)
        {
            if (csvFileViewModel.FormFile != null)
            {
                try
                {
                    string fileExtension = Path.GetExtension(csvFileViewModel.FormFile.FileName);

                    if (fileExtension != ".csv")
                    {
                        return BadRequest("Please select the csv file with .csv extension");
                    }


                    ICollection<Transaction> transactions = await _csvService.Parse(csvFileViewModel.FormFile.OpenReadStream());
                    await _transactionService.AddListOfTransactionsToSystem(transactions);
                    
                    return Ok();
                }
                catch (Exception ex)
                {
                    return BadRequest(ex.Message);
                }
            }
            else
            {
                return BadRequest("Please select the file first to upload.");
            }
           
        }

        [HttpGet("export")]
        public async Task<IActionResult> ExportExcel()
        {
            var transactions = await _transactionService.LoadAllAsync();
            using (var workbook = new XLWorkbook())
            {
                var worksheet = workbook.Worksheets.Add("Transactions");
                var currentRow = 1;
                worksheet.Cell(currentRow, 1).Value = "TransactionId";
                worksheet.Cell(currentRow, 2).Value = "Status";
                worksheet.Cell(currentRow, 3).Value = "Type";
                worksheet.Cell(currentRow, 4).Value = "ClientName";
                worksheet.Cell(currentRow, 2).Value = "Amount";
                foreach (var transaction in transactions)
                {
                    currentRow++;
                    worksheet.Cell(currentRow, 1).Value = transaction.TransactionId;
                    worksheet.Cell(currentRow, 2).Value = (await _statusService.GetById(transaction.StatusId)).Name;
                    worksheet.Cell(currentRow, 3).Value = (await _typeService.GetById(transaction.TypeId)).Name;
                    worksheet.Cell(currentRow, 4).Value = (await _clientService.GetById(transaction.ClientId)).Name + " " +
                        (await _clientService.GetById(transaction.ClientId)).Surname; 
                    worksheet.Cell(currentRow, 5).Value = "$" + " " + transaction.Amount.ToString();
                }

                using (var stream = new MemoryStream())
                {
                    workbook.SaveAs(stream);
                    var content = stream.ToArray();

                    return File(
                        content,
                        "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet",
                        "transactions.xlsx");
                }
            }
        }
    }
}
