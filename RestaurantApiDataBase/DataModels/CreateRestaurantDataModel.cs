using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestaurantApiDataBase.DataModels
{
   public class CreateRestaurantDataModel
    {
        public string Name { get; set; }
        public string Type { get; set; }
        public string Description { get; set; }

        public virtual CreateAdressDataModel Adress { get; set; }

        public virtual List<CreateDishDataModel> Dishes { get; set; }
    }
}
