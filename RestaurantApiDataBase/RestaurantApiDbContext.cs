using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestaurantApiDataBase
{
  public  class RestaurantApiDbContext : DbContext
    {
        public DbSet<Restaurant> Restaurants { get; set; }
        public DbSet<Dish> Dishes { get; set; }
        public DbSet<Adress> Adresses { get; set; }
        public RestaurantApiDbContext(DbContextOptions options) : base(options)
        {

        }
    }
}
