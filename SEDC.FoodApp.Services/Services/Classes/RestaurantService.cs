using SEDC.FoddApp.DataAccess.Mongo.Repositories.Interfaces;
using SEDC.FoodApp.DomainModels.Enum;
using SEDC.FoodApp.DomainModels.Models;
using SEDC.FoodApp.RequestModels.Models;
using SEDC.FoodApp.Services.Helpers;
using SEDC.FoodApp.Services.Services.Interfaces; 
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace SEDC.FoodApp.Services.Services.Classes
{
    public class RestaurantService: IRestaurantService
    {
        private readonly IRestaurantRepository _restauranRepository;

        public RestaurantService(IRestaurantRepository restauranRepository)
        {
            _restauranRepository = restauranRepository;
        }
        public async Task CreateNewRestaurantAsync(RestaurantRequestModel model)
        {
            var dtoRestaurant = new Restaurant()
            {
                Name = model.Name,
                Address = model.Address,
                Municipality = (Municipality)model.Municipality,
                Menu = new List<MenuItem>()
                
                
            };

             await _restauranRepository.InsertRestaurantAsync(dtoRestaurant);

        }

        public async Task DeleteRestaurantByIdAsync(string id)
        {
            await _restauranRepository.DeleteRestaurantByIdAsync(id);
        }

        public async Task<List<RestaurantRequestModel>> GetRestaurantAsync(RestaurantRequestModel requestModel)
        {
            Expression<Func<Restaurant, bool>> filter = f => true;

            if (!string.IsNullOrEmpty(requestModel.Name))
            {
                filter = filter.AndAlso(x => x.Name.ToLower().Contains(requestModel.Name));
            }

            if (!string.IsNullOrEmpty(requestModel.Address))
            {
                filter = filter.AndAlso(x => x.Address.ToLower().Contains(requestModel.Address));
            }

            if (requestModel.Municipality.HasValue)
            {
                filter = filter.AndAlso(x => x.Municipality == requestModel.Municipality);
            }


            var restaurantList = await _restauranRepository.GetRestaurantsAsync(filter);

            var mapToRestaurantRequestModels = new List<RestaurantRequestModel>();

            foreach (var restaurant in restaurantList)
            {
                var tempModel = new RestaurantRequestModel();

                tempModel.Id = restaurant.Id;
                tempModel.Name = restaurant.Name;
                tempModel.Address = restaurant.Address;
                tempModel.Municipality = restaurant.Municipality;
                tempModel.Menu = restaurant.Menu;

                mapToRestaurantRequestModels.Add(tempModel);
            }

            return mapToRestaurantRequestModels;
        }

        public async Task<Restaurant> GetRestaurantByIdAsync(string id)
        {
            return await _restauranRepository.GetRestaurantByIdAsync(id);
        }

        public async Task UpdateRestaurantAsync(UpdateRestaurantRequestModel model)
        {

            //var restaurant = await GetRestaurantByIdAsync(model.Id);

            //restaurant.Address = model.Address;
            //restaurant.Name = model.Name;
            //restaurant.Municipality = model.Municipality;

            var restaurant = new Restaurant()
            {
                Id = model.Id,
                Address = model.Address,
                Name = model.Name,
                Municipality = model.Municipality
            };



        await _restauranRepository.UpdateRestaurantAsync(restaurant);
        }

        public async Task UpdateRestaurantMenuAsync(UpdateRestaurantRequestModel requestModel)
        {
            var restaurant = await GetRestaurantByIdAsync(requestModel.Id);

            var menuItem = requestModel.MenuItem;

            if(string.IsNullOrEmpty(menuItem.Id))
            {
                var dtoMenuItem = new MenuItem()
                {
                    Id = Guid.NewGuid().ToString(),
                    Name = menuItem.Name,
                    Calories = menuItem.Calories,
                    Price = menuItem.Price,
                    IsVege = menuItem.IsVege,
                    MealType = menuItem.MealType
                };

                restaurant.Menu.Add(dtoMenuItem);
            }
            else
            {
                for (int i = 0; i < restaurant.Menu.Count; i++)
                {
                    if(restaurant.Menu[i].Id == menuItem.Id)
                    {
                        restaurant.Menu[i].Id = menuItem.Id;
                        restaurant.Menu[i].Name = menuItem.Name;
                        restaurant.Menu[i].Calories = menuItem.Calories;
                        restaurant.Menu[i].Price = menuItem.Price;
                        restaurant.Menu[i].MealType = menuItem.MealType;
                        restaurant.Menu[i].IsVege = menuItem.IsVege;
                        break;
                    }
                }
            }

            await _restauranRepository.UpdateRestaurantAsync(restaurant);
        }

        public async Task DeleteRestaurantMenuItemAsync(Restaurant restaurant, string menuItemId)
        {
            var menuItem = restaurant.Menu.FirstOrDefault(x => x.Id == menuItemId);

            restaurant.Menu.Remove(menuItem);

            await _restauranRepository.UpdateRestaurantAsync(restaurant);
        }

    } 
}
