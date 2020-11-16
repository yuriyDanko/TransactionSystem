using BusinessLayer.Abstractions.Service;
using BusinessLayer.Models;
using ClosedXML.Excel;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.ImplementationsServices
{
    public class ExcelService : IExcelService
    {
        private readonly IClientService _clientService;
        private readonly ITypeService _typeService;
        private readonly IStatusService _statusService;

        public ExcelService(IClientService clientService, IStatusService statusService, ITypeService typeService)
        {
            _clientService = clientService;
            _statusService = statusService;
            _typeService = typeService;
        }
        public async Task<byte[]> BuildContentForExcelFile(ICollection<Transaction> transactions)
        {
            byte[] content;
            using (var workbook = new XLWorkbook())
            {
                var worksheet = workbook.Worksheets.Add("Transactions");
                var currentRow = 1;
                worksheet.Cell(currentRow, 1).Value = "TransactionId";
                worksheet.Cell(currentRow, 2).Value = "Status";
                worksheet.Cell(currentRow, 3).Value = "Type";
                worksheet.Cell(currentRow, 4).Value = "ClientName";
                worksheet.Cell(currentRow, 5).Value = "Amount";
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
                    content = stream.ToArray();
                }

                return content;
            }
        }
    }
}
