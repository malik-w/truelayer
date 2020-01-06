using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;
using Interview.Wajid.Malik.Models;
using Interview.Wajid.Malik.Repositories;
using Interview.Wajid.Malik.Services;
using Interview.Wajid.Malik.Services.HttpClients;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace Interview.Wajid.Malik
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
            services.AddControllers();

            var dbConfiguration = new DBConfiguration();
            dbConfiguration.ConnectionString = Configuration.GetConnectionString("DBConnection");
            services.Add(new ServiceDescriptor(typeof(DBConfiguration), dbConfiguration));

            var clientConfiguration = File.ReadAllText("secrets.json");
            var config = JsonSerializer.Deserialize<ClientConfiguration>(clientConfiguration);
            services.Add(new ServiceDescriptor(typeof(ClientConfiguration), config));

            services.AddHttpClient<IAuthHttpClient, AuthHttpClient>(client =>
            {
                client.BaseAddress = new Uri("https://auth.truelayer.com/");
            });
            services.AddHttpClient<IDataHttpClient, DataHttpClient>(client =>
            {
                client.BaseAddress = new Uri("https://api.truelayer.com/data/v1/");
            });

            services.AddSingleton<IAuthService, AuthService>();

            services.AddSingleton<ITransactionRepository, TransactionRepository>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            //app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
