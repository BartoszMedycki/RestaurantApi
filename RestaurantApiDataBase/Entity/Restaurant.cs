using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestaurantApiDataBase 
{
    public class Restaurant : IEqualityComparer<Restaurant>
    {
        public int Id { get; set; }
        public int AdressId { get; set; }
        public string Name { get; set; }
        public string Type { get; set; }
        public string Description { get; set; }

        public virtual Adress Adress { get; set; }

        public virtual List<Dish> Dishes { get; set; }

        public bool Equals(Restaurant x, Restaurant y)
        {
            return true;
        }

        public int GetHashCode([DisallowNull] Restaurant obj)
        {
            return 1;
        }
    }
}
