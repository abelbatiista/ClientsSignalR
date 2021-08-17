using System;
using System.Collections.Generic;

#nullable disable

namespace Database.Models
{
    public partial class Status
    {
        public Status()
        {
            RestaurantOrders = new HashSet<RestaurantOrder>();
        }

        public int StatusId { get; set; }
        public string Name { get; set; }

        public virtual ICollection<RestaurantOrder> RestaurantOrders { get; set; }
    }
}
