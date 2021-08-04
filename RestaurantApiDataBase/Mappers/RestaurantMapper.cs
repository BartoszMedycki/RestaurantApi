using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestaurantApiDataBase.Mappers
{
    public class RestaurantMapper
    {
        IMapper mapper;
        public RestaurantMapper()
        {
            mapper = new MapperConfiguration(
                configure => {
                    configure.CreateMap<Restaurant, DataModels.RestaurantDataModel>().ReverseMap();
                    configure.CreateMap<Adress, DataModels.AdressDataModel>().ReverseMap();
                    configure.CreateMap<Dish, DataModels.DishDataModel>().ReverseMap();

                }



                ).CreateMapper();



        }
        public IEnumerable<DataModels.RestaurantDataModel> Map(IEnumerable<Restaurant> restaurants)
        {
            var listOfRestaurantDataModel = new List<DataModels.RestaurantDataModel>();
            foreach (var restaurant in restaurants)
            {
                listOfRestaurantDataModel.Add(mapper.Map<DataModels.RestaurantDataModel>(restaurant));
            }
            return listOfRestaurantDataModel;
            
        }   
        public IEnumerable<Restaurant> Map(IEnumerable<DataModels.RestaurantDataModel> restaurants)
        {
            var listOfRestaurantDataModel = new List<Restaurant>();
            foreach (var restaurant in restaurants)
            {
                listOfRestaurantDataModel.Add(mapper.Map<Restaurant>(restaurant));
            }
            return listOfRestaurantDataModel;
            
        }
        public DataModels.RestaurantDataModel Map(Restaurant restaurant)
        {
            return mapper.Map<DataModels.RestaurantDataModel>(restaurant);
        }
    }
}
