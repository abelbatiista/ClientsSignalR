using System;
using System.Collections.Generic;

#nullable disable

namespace Database.Models
{
    public partial class Client
    {
        public Client()
        {
            RestaurantOrders = new HashSet<RestaurantOrder>();
        }

        public int ClientId { get; set; }
        public string Name { get; set; }

        public virtual ICollection<RestaurantOrder> RestaurantOrders { get; set; }
    }
}
