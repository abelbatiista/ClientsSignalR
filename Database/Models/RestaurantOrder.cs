using System;
using System.Collections.Generic;

#nullable disable

namespace Database.Models
{
    public partial class RestaurantOrder
    {
        public int RestaurantOrderId { get; set; }
        public int ClientId { get; set; }
        public int StarterId { get; set; }
        public int MainPlateId { get; set; }
        public int DessertId { get; set; }
        public int DrinkId { get; set; }
        public int StatusId { get; set; }
        public DateTime? Date { get; set; }
        public decimal? Subtotal { get; set; }
        public decimal? Itbis { get; set; }
        public decimal? Total { get; set; }

        public virtual Client Client { get; set; }
        public virtual Dessert Dessert { get; set; }
        public virtual Drink Drink { get; set; }
        public virtual MainPlate MainPlate { get; set; }
        public virtual Starter Starter { get; set; }
        public virtual Status Status { get; set; }
    }
}
