using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using RestaurantApiDataBase.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using RestaurantApiDataBase.Mappers;
using RestaurantApiDataBase;
using RestaurantApiDataBase.DataModels;
using RestaurantApiDataBase.Exceptions;

namespace RestaurantApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class RestaurantController : ControllerBase
    {


        private readonly ILogger<RestaurantController> _logger;
        private readonly IRestaurantRepository mRestaurantRepository;

        public ILogger<RestaurantController> Logger1 { get; }

        public RestaurantController(ILogger<RestaurantController> logger, IRestaurantRepository restaurantRepository,
            ILogger<RestaurantController> logger1)
        {
            _logger = logger;
            mRestaurantRepository = restaurantRepository;
            Logger1 = logger1;
        }

        [HttpGet]
        public ActionResult<IEnumerable<RestaurantDataModel>> Get()
        {
            
          
          
            
            var restaurants = mRestaurantRepository.GetRestaurantsInclude();
            
            
                return Ok(restaurants);
            

           
        }
        [HttpGet("{id}")]
        public ActionResult<RestaurantDataModel> Get([FromRoute] int id)
        {
            var restaurant = mRestaurantRepository.GetRestaurantById(id);
            
                return Ok(restaurant);
            

        }
        [HttpDelete("{id}")]
        public ActionResult Delete([FromRoute] int id)
        {
            mRestaurantRepository.DeleteRestaurant(id);
           
               return Ok();
            
           

        }
        [HttpPost]
        public ActionResult AddRestaurant([FromBody] CreateRestaurantDataModel createRestaurantData)
        {
           int restaurantId = mRestaurantRepository.AddRestaurant(createRestaurantData);
            return Created($"/restaurant/{restaurantId}", null);
        }
        [HttpPatch("{id}")]
        public ActionResult UpdateRestaurant([FromRoute] int id, [FromBody] CreateRestaurantDataModel createRestaurantData)
        {
           mRestaurantRepository.UpdateRestaurantById(createRestaurantData, id);
            
                return Ok();
          
        
        }













    }
    }

