using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestaurantApiDataBase.DataModels
{
    public class RestaurantDataModel
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public string Type { get; set; }

        public virtual AdressDataModel Adress { get; set; }

        public virtual List<DishDataModel> Dishes { get; set; }
    }
}
