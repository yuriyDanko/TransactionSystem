using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BusinessLayer.Abstractions.Service;
using BusinessLayer.ImplementationsServices;
using DataLayer.Abstractions.Repositories;
using DataLayer.Implementations;
using DataLayer.Models;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace TransactionSystemWebAPI
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<DatabaseContext>(opts => opts.UseSqlServer(Configuration.GetConnectionString("sqlConnection"), b => b.MigrationsAssembly("DataLayer")));
            services.AddScoped<ITransactionRepository, TransactionRepository>();
            services.AddScoped<IClientRepository, ClientRepository>();
            services.AddScoped<ITypeRepository, TypeRepository>();
            services.AddScoped<IStatusRepository, StatusRepository>();
            services.AddScoped<ITransactionService, TransactionService>();
            services.AddScoped<ICsvService, CsvService>();
            services.AddScoped<IClientService, ClientService>();
            services.AddScoped<IStatusService, StatusService>();
            services.AddScoped<ITypeService, TypeService>();
            services.AddControllers();
            services.AddSwaggerGen();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "My API V1");
            });
            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
