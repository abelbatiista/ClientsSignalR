using LogicBusiness.Services.RestaurantOrderService;
using LogicBusiness.Services.ViewModels;
using Microsoft.AspNetCore.SignalR.Client;
using Newtonsoft.Json;
using OrderMonitor.InsertOrder;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace OrderMonitor
{
    public partial class FmOrderMonitorMain : Form
    {

        private readonly CRestaurantOrderService _orderService;

        private readonly string _url = @"http://localhost:5000/serverHub";
        private readonly HubConnection connection;

        private int _id;

        public FmOrderMonitorMain()
        {
            InitializeComponent();

            connection = new HubConnectionBuilder().WithUrl(_url).Build();
            connection.Closed += async (error) =>
            {
                Thread.Sleep(10 * 1000);
                await connection.StartAsync();
            };

            _orderService = new CRestaurantOrderService();

            _id = 0;

            this.Load += new EventHandler(this.FmOrderMonitorMain_Load);
        }

        #region Events

        private async void FmOrderMonitorMain_Load(object sender, EventArgs e)
        {
            await ThrowChoice();
        }

        private async void BtnInsertOrder_Click(object sender, EventArgs e)
        {
            await ShowInsertOrderForm();
        }

        private void DgvOrders_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            OrderSelected(e);
        }

        private async void BtnFinishOrder_Click(object sender, EventArgs e)
        {
            await FinishingOrder();
        }

        #endregion

        #region "Private Methods"

        private void Clean()
        {
            BtnFinishOrder.Enabled = false;
            DgvOrders.ClearSelection();
        }

        private async Task LoadOrders()
        {
            BindingSource source = new BindingSource
            {
                DataSource = await _orderService.GetAllViewModel()
            };
            DgvOrders.DataSource = source;
            DgvOrders.ClearSelection();
        }

        private async Task ShowInsertOrderForm()
        {
            FmInsertOrder insertOrder = new FmInsertOrder();
            this.Hide();
            insertOrder.ShowDialog();
            this.Show();
            await Emit();
            await ThrowChoice();
        }

        private void OrderSelected(DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                _id = Convert.ToInt32(DgvOrders.Rows[e.RowIndex].Cells[0].Value.ToString());
                BtnFinishOrder.Enabled = true;
            }
        }

        private async Task FinishingOrder()
        {

            var order = await _orderService.FindById(_id);

            if (order != null)
            {
                if (order.StatusId == 3)
                {
                    order.StatusId = 4;

                    var response = await _orderService.ChangeOrderStatus(order);

                    if (response)
                    {
                        MessageBox.Show("Orden terminada.", "Notificación");
                    }
                    else
                    {
                        MessageBox.Show("Ha ocurrido un error.", "Error");
                    }
                }
                else
                {
                    MessageBox.Show("Para terminar una orden debe estar lista para entregar.", "Advertencia");
                }
            }
            else
            {
                MessageBox.Show("Ha ocurrido un error.", "Error");
            }

            Clean();
            await Emit();
            await ThrowChoice();

        }

        private async Task Emit()
        {
            if (connection.State.ToString() != "Connected")
                await connection.StartAsync();

            var orders = await _orderService.GetAllViewModel();

            List<string> _orders = new List<string>();

            foreach (var order in orders)
            {
                _orders.Add(JsonConvert.SerializeObject(order));
            }

            await connection.InvokeCoreAsync("SendSome", args: new[] { _orders });
            await connection.StopAsync();
        }

        private async Task Receive()
        {
            if (connection.State.ToString() != "Connected")
                await connection.StartAsync();

            List<COrdersViewModel> orders = new List<COrdersViewModel>();

            connection.On<List<string>>("ReceiveSome", (items) =>
            {
                foreach (var item in items)
                {
                    orders.Add(JsonConvert.DeserializeObject<COrdersViewModel>(item));
                }

                DgvOrders.Invoke((MethodInvoker)delegate
                {
                    BindingSource source = new BindingSource
                    {
                        DataSource = orders
                    };

                    DgvOrders.DataSource = source;
                    DgvOrders.ClearSelection();
                });

            });
            await connection.StopAsync();
        }

        private async Task ThrowChoice()
        {
            if (DgvOrders.Rows.Count == 0)
                await LoadOrders();
            else
                await Receive();
        }

        #endregion
    }
}
