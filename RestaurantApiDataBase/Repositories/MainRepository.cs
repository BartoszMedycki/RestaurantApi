using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestaurantApiDataBase.Repositories
{
    public abstract class MainRepository<EntityT> where  EntityT : class
        
    {
        protected readonly IServiceProvider mServiceProvider;
        protected readonly RestaurantApiDbContext dbContext;
        protected abstract DbSet<EntityT> DbSet { get; }
        public MainRepository(IServiceProvider serviceProvider)
        {
            mServiceProvider = serviceProvider;
            dbContext =mServiceProvider.GetService(typeof(RestaurantApiDbContext)) as RestaurantApiDbContext;
        }
        public void SaveChanges()
        {
            dbContext.SaveChanges();
        }
        public IEnumerable<EntityT> GetAll()
        {
            List<EntityT> entities = new List<EntityT>();
            var collection = DbSet;
            foreach (var item in collection)
            {
                entities.Add(item);
            }
            return entities;
        
        }
    }
}
