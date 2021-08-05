using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using RestaurantApiDataBase.Exceptions;
using RestaurantApiDataBase.Interfaces;
using RestaurantApiDataBase.Mappers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestaurantApiDataBase.Repositories
{
    public class RestaurantRepository : MainRepository<Restaurant>, IRestaurantRepository
    {
        public RestaurantRepository(IServiceProvider serviceProvider,
            CreateRestaurantMapper createRestaurantMapper, RestaurantMapper restaurantMapper,
            ILogger<RestaurantRepository> logger) : base(serviceProvider) 
        {
            CreateRestaurantMapper = createRestaurantMapper;
            RestaurantMapper = restaurantMapper;
            Logger = logger;
        }

        protected override DbSet<Restaurant> DbSet => dbContext.Restaurants;

        public CreateRestaurantMapper CreateRestaurantMapper { get; }
        public RestaurantMapper RestaurantMapper { get; }
        public ILogger<RestaurantRepository> Logger { get; }

        public IEnumerable<DataModels.RestaurantDataModel> GetRestaurantsInclude()
        {
            
            Logger.LogTrace("RestaurantRepository LogTrace");
            var tmpDbSet = dbContext.Restaurants.Include(x => x.Adress).Include(y => y.Dishes).ToList();
            var mappedListOfRestaurants = RestaurantMapper.Map(tmpDbSet);
            List<DataModels.RestaurantDataModel> IncludeRestaurantsList = new List<DataModels.RestaurantDataModel>();
            foreach (var restaurant in mappedListOfRestaurants)
            {
                IncludeRestaurantsList.Add(restaurant);
            }
            return IncludeRestaurantsList;


        }
        public IEnumerable<DataModels.RestaurantDataModel> GetIncludeRestaurantsByName(string name)
        {
            var restaurantsByName = dbContext.Restaurants.Include(x => x.Adress).Include(y => y.Dishes)
                .Where(restaurantName => restaurantName.Name == name).ToList();
            var restaurantsDataModel = RestaurantMapper.Map(restaurantsByName);
            List<DataModels.RestaurantDataModel> IncludeRestaurantsList = new List<DataModels.RestaurantDataModel>();
            foreach (var restaurant in restaurantsDataModel)
            {
                IncludeRestaurantsList.Add(restaurant);
            }
            return IncludeRestaurantsList;
        }
        public  DataModels.RestaurantDataModel GetRestaurantById(int id)
        {
            var restaurantById = dbContext.Restaurants.Include(x => x.Adress).Include(s => s.Dishes)
                .Where(RestaurantId => RestaurantId.Id == id).FirstOrDefault();
            var restaurantDataModel = RestaurantMapper.Map(restaurantById);
            return restaurantDataModel;
        }
        public int AddRestaurant(DataModels.CreateRestaurantDataModel restaurant)
        {
            var mappedRestaurant = CreateRestaurantMapper.Map(restaurant);
            
                DbSet.Add(mappedRestaurant);
            this.SaveChanges();
            return mappedRestaurant.Id;
            
        }
        public bool DeleteRestaurant(int id)
        {
            var restaurant = dbContext.Restaurants.Where(restaurantId => restaurantId.Id == id).FirstOrDefault();
            if (restaurant != null)
            {
                dbContext.Restaurants.Remove(restaurant);
                this.SaveChanges();
                return true;
                
            }
            return false;
          
        }
        public bool UpdateRestaurantById(DataModels.CreateRestaurantDataModel createRestaurantData, int id)
         {
            var restaurant = dbContext.Restaurants.Include(x=>x.Adress).Include(s=>s.Dishes).Where(restaurantId => restaurantId.Id == id).FirstOrDefault();
            if (restaurant != null)
            {
                var updateRestaurant = CreateRestaurantMapper.Map(createRestaurantData);
                restaurant.Adress = updateRestaurant.Adress;
                restaurant.Dishes = updateRestaurant.Dishes;
                restaurant.Description = updateRestaurant.Description;
                restaurant.Name = updateRestaurant.Name;
                
                this.SaveChanges();
                return true;
            }
            return false;
        }
        public void Seed()
        {
            if (dbContext.Database.CanConnect())
            {
                if (!DbSet.Any())
                {
                    var ListOfStartRestaurants = GetStartRestaurants();
                    foreach (var restaurant in ListOfStartRestaurants)
                    {
                        DbSet.Add(restaurant);
                    }
                    this.SaveChanges();
                }
            }
            
        }

        public IEnumerable<Restaurant> GetStartRestaurants()
        {
            List<Restaurant> listOfRestaurants = new List<Restaurant>();
            listOfRestaurants.Add(new Restaurant()
            {
                Name = "KFC",
                Type = "FastFood",
                Description = "Kentucky Fri Chicken - American chain of fast food bars",
                Adress = new Adress()
                {
                    City = "Krakow",
                    Street = "Florianska 15",
                    PostalCode = "30-059"
                },
                Dishes = new List<Dish>()
                {
                    new Dish()
                    {
                        Name = "longer",
                        Description = "Small chicken burger",
                        Price = 4
                    },
                    new Dish()
                    {
                        Name = "MegaPocket",
                        Description = "Big chicken Tortila",
                        Price = 17
                    }

                }
            });
            listOfRestaurants.Add(new Restaurant()
            {
                Name = "McDonald",
                Type = "FastFood",
                Description = "McDonad - American chain of fast food bars",
                Adress = new Adress
                {
                    City = "Krakow",
                    Street = "Szewska 30",
                    PostalCode = "30-059"
                },
                Dishes = new List<Dish>()
                {
                    new Dish()
                    {
                        Name = "McDouble",
                        Description = "chees burger with double meat",
                        Price = 4
                    },
                    new Dish()
                    {
                        Name = "BigMac",
                        Description = "BigBurger",
                        Price = 17
                    }

                }
                
                
            });





            
            return listOfRestaurants;
         
        }
    }
}
