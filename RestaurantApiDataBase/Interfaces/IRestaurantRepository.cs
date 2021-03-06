using System.Collections.Generic;

namespace RestaurantApiDataBase.Interfaces
{
   public interface IRestaurantRepository
    {
        int AddRestaurant(DataModels.CreateRestaurantDataModel restaurant);
        IEnumerable<DataModels.RestaurantDataModel> GetIncludeRestaurantsByName(string name);
        public  DataModels.RestaurantDataModel GetRestaurantById(int id);
        public void DeleteRestaurant(int id);
        public void UpdateRestaurantById(DataModels.CreateRestaurantDataModel createRestaurantData, int id);
        IEnumerable<DataModels.RestaurantDataModel> GetRestaurantsInclude();
        IEnumerable<Restaurant> GetAll();
        public void SaveChanges();
        public void Seed();

        IEnumerable<Restaurant> GetStartRestaurants();
    }
}