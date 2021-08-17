using System;
using System.Collections.Generic;

#nullable disable

namespace Database.Models
{
    public partial class Drink
    {
        public Drink()
        {
            RestaurantOrders = new HashSet<RestaurantOrder>();
        }

        public int DrinkId { get; set; }
        public string Name { get; set; }
        public decimal? Cost { get; set; }

        public virtual ICollection<RestaurantOrder> RestaurantOrders { get; set; }
    }
}
