using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace LogicBusiness.Services.ViewModels
{
    public class COrdersViewModel
    {

        public int Codigo { get; set; }
        [StringLength(500)]
        public string Cliente { get; set; }
        public DateTime? FechaHora { get; set; }
        public decimal? Subtotal { get; set; }
        public decimal? ITBIS { get; set; }
        public decimal? Total { get; set; }
        public string Estado { get; set; }

    }
}
