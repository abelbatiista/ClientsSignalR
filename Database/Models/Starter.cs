using System;
using System.Collections.Generic;

#nullable disable

namespace Database.Models
{
    public partial class Starter
    {
        public Starter()
        {
            RestaurantOrders = new HashSet<RestaurantOrder>();
        }

        public int StarterId { get; set; }
        public string Name { get; set; }
        public decimal? Cost { get; set; }

        public virtual ICollection<RestaurantOrder> RestaurantOrders { get; set; }
    }
}
