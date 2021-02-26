using SEDC.FoodApp.DomainModels.Models;
using SEDC.FoodApp.RequestModels.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace SEDC.FoodApp.Services.Services.Interfaces
{
    public interface IRestaurantService
    {
        Task CreateNewRestaurantAsync(RestaurantRequestModel model);


        Task<List<RestaurantRequestModel>> GetRestaurantAsync(RestaurantRequestModel requestModel);

        Task DeleteRestaurantByIdAsync(string id);

        Task UpdateRestaurantAsync(UpdateRestaurantRequestModel model);

        Task UpdateRestaurantMenuAsync(UpdateRestaurantRequestModel requestModel);

        Task<Restaurant> GetRestaurantByIdAsync(string id);

        Task DeleteRestaurantMenuItemAsync(Restaurant requestModel, string menuId);
    }
}
