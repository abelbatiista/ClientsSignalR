using System;
using System.Collections.Generic;

#nullable disable

namespace Database.Models
{
    public partial class Dessert
    {
        public Dessert()
        {
            RestaurantOrders = new HashSet<RestaurantOrder>();
        }

        public int DessertId { get; set; }
        public string Name { get; set; }
        public decimal? Cost { get; set; }

        public virtual ICollection<RestaurantOrder> RestaurantOrders { get; set; }
    }
}
