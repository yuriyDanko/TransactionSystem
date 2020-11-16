using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using BusinessLayer.Abstractions.Service;
using TransactionSystemWebAPI.ViewModel;
using System.IO;
using System.Collections.ObjectModel;
using BusinessLayer.Models;
using ClosedXML.Excel;
using Microsoft.AspNetCore.Authorization;

namespace TransactionSystemWebAPI.Controllers
{
    [Authorize]
    [Route("api/transactions")]

    public class TransactionController : Controller
    {
        private readonly ITransactionService _transactionService;
        private readonly IClientService _clientService;
        private readonly ITypeService _typeService;
        private readonly IStatusService _statusService;
        private readonly ICsvService _csvService;
        private readonly IExcelService _excelService;
        public TransactionController(ITransactionService transactionService, ICsvService csvService,
            IExcelService excelService, IClientService clientService, IStatusService statusService, ITypeService typeService)
        {
            _transactionService = transactionService;
            _clientService = clientService;
            _statusService = statusService;
            _typeService = typeService;
            _csvService = csvService;
            _excelService = excelService;
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<TransactionViewModelResult>> GetById(int id)
        {
            var transaction = await _transactionService.GetByTransactionId(id);
            return Ok(new TransactionViewModelResult()
            {
                TransactionId = transaction.TransactionId,
                ClientName = (await _clientService.GetById(transaction.ClientId)).Name,
                ClientSurname = (await _clientService.GetById(transaction.ClientId)).Surname,
                StatusName = (await _statusService.GetById(transaction.StatusId)).Name,
                TypeName = (await _typeService.GetById(transaction.TypeId)).Name,
                Amount = transaction.Amount
            });
        }

        [HttpGet]
        public async Task<ActionResult<ICollection<TransactionViewModelResult>>> GetAll()
        {
            var transactions = await _transactionService.LoadAllAsync();
            var transactionsViewModelResult = new Collection<TransactionViewModelResult>();
            foreach (var transaction in transactions)
            {
                transactionsViewModelResult.Add(new TransactionViewModelResult()
                {
                    TransactionId = transaction.TransactionId,
                    ClientName = (await _clientService.GetById(transaction.ClientId)).Name,
                    ClientSurname = (await _clientService.GetById(transaction.ClientId)).Surname,
                    StatusName = (await _statusService.GetById(transaction.StatusId)).Name,
                    TypeName = (await _typeService.GetById(transaction.TypeId)).Name,
                    Amount = transaction.Amount
                });
            }
            return Ok(transactionsViewModelResult);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] TransactionViewModel transactionViewModel)
        {
            if (await _statusService.GetById(transactionViewModel.StatusId) == null)
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

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var transaction = await _transactionService.GetByTransactionId(id);
            await _transactionService.DeleteAsync(transaction);
            return Ok();
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, TransactionViewModel transactionViewModel)
        {
            Transaction transaction = null;
            transaction = await _transactionService.GetByTransactionId(id);
            transaction.StatusId = transactionViewModel.StatusId;
            await _transactionService.UpdateAsync(transaction);
            return Ok();
        }

        [HttpPost("import")]
        public async Task<IActionResult> ImportCsv([FromForm] CsvFileViewModel csvFileViewModel)
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
            var content = await _excelService.BuildContentForExcelFile(transactions);
            return File(
                content,
                "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet",
                "transactions.xlsx");
        }
    
    }
}
