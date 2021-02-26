using Microsoft.Extensions.DependencyInjection;
using SEDC.FoddApp.DataAccess.Mongo.Repositories.Classes;
using SEDC.FoddApp.DataAccess.Mongo.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace SEDC.FoodApp.Services.Helpers
{
    public static class DIRepositoryModule
    {
        public static IServiceCollection RegisterRepositories(IServiceCollection services, 
                                                            string mongoConnectionString,
                                                            string mongoDatabase)
        {
            //register mongodb resotries

            services.AddScoped<IRestaurantRepository, RestaurantRepository>(provider => 
                                new RestaurantRepository(mongoConnectionString, mongoDatabase));

            return services;
        }
    }
}
