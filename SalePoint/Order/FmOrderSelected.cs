using LogicBusiness.Services.RestaurantOrderService;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SalePoint.Order
{
    public partial class FmOrderSelected : Form
    {

        private readonly CRestaurantOrderService _orderService;

        public int Id { get; set; }

        public FmOrderSelected()
        {
            InitializeComponent();

            _orderService = new CRestaurantOrderService();
        }

        #region Events

        private async void FmOrderSelected_Load(object sender, EventArgs e)
        {
            await LoadOrder();
        }

        #endregion

        #region Private Methods

        private async Task LoadOrder()
        {
            BindingSource source = new BindingSource
            {
                DataSource = await _orderService.GetDetailViewModel(Id)
            };

            DgvOrder.DataSource = source;
            DgvOrder.ClearSelection();
        }

        #endregion


    }
}
