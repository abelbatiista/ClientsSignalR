using LogicBusiness.Services.RestaurantOrderService;
using LogicBusiness.Services.ViewModels;
using Microsoft.AspNetCore.SignalR.Client;
using Newtonsoft.Json;
using SalePoint.Order;
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

namespace SalePoint
{
    public partial class SalePointMain : Form
    {
        private readonly CRestaurantOrderService _orderService;

        private readonly string _url = @"http://localhost:5000/serverHub";
        private readonly HubConnection connection;

        private int _id;

        public SalePointMain()
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
        }

        #region Events

        private async void SalePointMain_Load(object sender, EventArgs e)
        {
            await ThrowChoice();
        }

        private void DgvOrders_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            OrderSelected(e);
        }

        private async void BtnToProcess_Click(object sender, EventArgs e)
        {
            await ProcessingOrder();
        }

        private async void BtnToDeliverOrder_Click(object sender, EventArgs e)
        {
            await ReadyForDeliver();
        }

        private async void BtnOrderSelected_Click(object sender, EventArgs e)
        {
            await ShowOrderSelectedForm();
        }

        #endregion

        #region Private Methods

        private void Clean()
        {
            BtnToDeliverOrder.Enabled = false;
            BtnToProcess.Enabled = false;
            BtnOrderSelected.Enabled = false;
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

        private void OrderSelected(DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                _id = Convert.ToInt32(DgvOrders.Rows[e.RowIndex].Cells[0].Value.ToString());
                BtnToDeliverOrder.Enabled = true;
                BtnToProcess.Enabled = true;
                BtnOrderSelected.Enabled = true;
            }
        }

        private async Task ProcessingOrder()
        {

            var order = await _orderService.FindById(_id);

            if (order != null)
            {
                if (order.StatusId == 1)
                {
                    order.StatusId = 2;

                    var response = await _orderService.ChangeOrderStatus(order);

                    if (response)
                    {
                        MessageBox.Show("Procesando orden.", "Notificación");
                    }
                    else
                    {
                        MessageBox.Show("Ha ocurrido un error.", "Error");
                    }
                }
                else
                {
                    MessageBox.Show("Para procesar una orden debe estar pendiente.", "Advertencia");
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

        private async Task ReadyForDeliver()
        {

            var order = await _orderService.FindById(_id);

            if (order != null)
            {
                if (order.StatusId == 2)
                {
                    order.StatusId = 3;

                    var response = await _orderService.ChangeOrderStatus(order);

                    if (response)
                    {
                        MessageBox.Show("Orden para entregar.", "Notificación");
                    }
                    else
                    {
                        MessageBox.Show("Ha ocurrido un error.", "Error");
                    }
                }
                else
                {
                    MessageBox.Show("Para listar una orden para entregar debe estar en proceso.", "Advertencia");
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
        }

        private async Task Receive()
        {
            if (connection.State.ToString() != "Connected")
                await connection.StartAsync();

            List<COrdersViewModel> orders = new List<COrdersViewModel>();

            connection.On<List<string>>("ReceiveSome", (items) =>
            {
                orders = null;

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
            
        }

        private async Task ThrowChoice()
        {
            if (DgvOrders.Rows.Count == 0)
                await LoadOrders();
            else
                await Receive();
        }

        private async Task ShowOrderSelectedForm()
        {
            FmOrderSelected orderSelected = new FmOrderSelected { Id = _id };
            this.Hide();
            orderSelected.ShowDialog();
            this.Show();
            await Emit();
            await ThrowChoice();
        }

        #endregion

    }
}
