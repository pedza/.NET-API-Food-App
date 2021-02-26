using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using SEDC.FoodApp.Services.Helpers;
using SEDC.FoodApp.Services.Services.Classes;
using SEDC.FoodApp.Services.Services.Interfaces;

namespace SEDC.FoodApp.Web
{
    public class Startup
    {
        public IConfiguration Configuration { get; }
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();

            var connectionStrings = Configuration.GetSection("ConnectionStrings");

            //mongoDb
            var mongoCs = connectionStrings.GetValue<string>("MongoConnectionString");
            var mongoDbName = connectionStrings.GetValue<string>("MongoDatabase");

            //register services
            services.AddTransient<IRestaurantService, RestaurantService>();

            //Dependency Injection Module
            DIRepositoryModule.RegisterRepositories(services, mongoCs, mongoDbName);

            //cors

            services.AddCors();
        }

        
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseRouting();

            app.UseCors(builder => 
                        builder.WithOrigins("http://localhost:4200", "http://localhost:45551")
                        .AllowAnyHeader()
                        .AllowAnyMethod());

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
