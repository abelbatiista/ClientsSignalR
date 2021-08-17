using System;
using System.Collections.Generic;

#nullable disable

namespace Database.Models
{
    public partial class MainPlate
    {
        public MainPlate()
        {
            RestaurantOrders = new HashSet<RestaurantOrder>();
        }

        public int MainPlateId { get; set; }
        public string Name { get; set; }
        public decimal? Cost { get; set; }

        public virtual ICollection<RestaurantOrder> RestaurantOrders { get; set; }
    }
}
