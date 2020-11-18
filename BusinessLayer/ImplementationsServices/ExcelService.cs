using BusinessLayer.Abstractions.Service;
using ClosedXML.Excel;
using Entities.Entities;
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

                var t = Task.Run(() => {
                    foreach (var transaction in transactions)
                    {
                        currentRow++;
                        worksheet.Cell(currentRow, 1).Value = transaction.TransactionId;
                        worksheet.Cell(currentRow, 2).Value = transaction.Status.Name;
                        worksheet.Cell(currentRow, 3).Value = transaction.Type.Name;
                        worksheet.Cell(currentRow, 4).Value = transaction.Client.Name + " " + transaction.Client.Surname;
                        worksheet.Cell(currentRow, 5).Value = "$" + " " + transaction.Amount.ToString();
                    }
                });

                t.Wait();

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
