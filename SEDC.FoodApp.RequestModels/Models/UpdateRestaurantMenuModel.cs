using System;
using System.Collections.Generic;
using System.Text;

namespace SEDC.FoodApp.RequestModels.Models
{
    public class UpdateRestaurantMenuModel
    {
        public string Id { get; set; }

        public string Name { get; set; }

        public MenuItemRequestModel MenuItem { get; set; }
    }
}
