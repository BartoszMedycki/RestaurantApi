using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestaurantApiDataBase.Mappers
{
    public class CreateRestaurantMapper
    {
        IMapper mapper;
        public CreateRestaurantMapper()
        {
            mapper = new MapperConfiguration(configure =>
            {
                configure.CreateMap<DataModels.CreateRestaurantDataModel, Restaurant>().ReverseMap();
                configure.CreateMap<DataModels.CreateDishDataModel, Dish>().ReverseMap();
                configure.CreateMap<DataModels.CreateAdressDataModel, Adress>().ReverseMap();
            }).CreateMapper();
        }
        public IEnumerable<Restaurant> Map (IEnumerable<DataModels.CreateRestaurantDataModel> restaurants)
        {
            List<Restaurant> listOfRestaurants = new List<Restaurant>();
            foreach (var restaurant in restaurants)
            {
                listOfRestaurants.Add(mapper.Map<Restaurant>(restaurant));
            }
            return listOfRestaurants;
            
        }
        public IEnumerable<DataModels.CreateRestaurantDataModel> Map (IEnumerable<Restaurant> restaurants)
        {
            List<DataModels.CreateRestaurantDataModel> listOfRestaurants = new List<DataModels.CreateRestaurantDataModel>();
            foreach (var restaurant in restaurants)
            {
                listOfRestaurants.Add(mapper.Map<DataModels.CreateRestaurantDataModel>(restaurant));
            }
            return listOfRestaurants;
            
        }     
        public DataModels.CreateRestaurantDataModel Map (Restaurant restaurant)
            {


            return mapper.Map<DataModels.CreateRestaurantDataModel>(restaurant);
            } 
            public Restaurant Map (DataModels.CreateRestaurantDataModel restaurant)
            {


            return mapper.Map<Restaurant>(restaurant);
            }
            
            
        }
    }

