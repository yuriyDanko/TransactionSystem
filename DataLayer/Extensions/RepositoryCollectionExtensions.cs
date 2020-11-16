using DataLayer.Abstractions.Repositories;
using DataLayer.Implementations;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;

namespace DataLayer.Extensions
{
    public static class RepositoryCollectionExtensions
    {
        public static IServiceCollection AddRepositoryConfig(this IServiceCollection services)
        {
            services.AddScoped<ITransactionRepository, TransactionRepository>();
            services.AddScoped<IClientRepository, ClientRepository>();
            services.AddScoped<ITypeRepository, TypeRepository>();
            services.AddScoped<IStatusRepository, StatusRepository>();
            return services;
        }
    }
}
