using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace LogicBusiness.Services.ViewModels
{
    public class COrderDetailsViewModel
    {

        public int Codigo { get; set; }
        [StringLength(500)]
        public string Cliente { get; set; }
        [StringLength(500)]
        public string Entrada { get; set; }
        [StringLength(500)]
        public string Principal { get; set; }
        [StringLength(500)]
        public string Postre { get; set; }
        [StringLength(500)]
        public string Bebida { get; set; }
        public DateTime? FechaHora { get; set; }
        public decimal? Subtotal { get; set; }
        public decimal? ITBIS { get; set; }
        public decimal? Total { get; set; }
        public string Estado { get; set; }

    }
}
