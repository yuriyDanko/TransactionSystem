using BusinessLayer.Abstractions.Service;
using BusinessLayer.ImplementationsServices;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessLayer.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddServiceConfig(this IServiceCollection services)
        {
            services.AddScoped<ICsvService, CsvService>();
            services.AddScoped<IExcelService, ExcelService>();
            services.AddScoped<ITransactionService, TransactionService>();
            services.AddScoped<IClientService, ClientService>();
            services.AddScoped<IStatusService, StatusService>();
            services.AddScoped<ITypeService, TypeService>();
            return services;
        }
    }
}
