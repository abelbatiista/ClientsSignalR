using Database.Models;
using LogicBusiness.Services.ClientService;
using LogicBusiness.Services.DessertService;
using LogicBusiness.Services.DrinkService;
using LogicBusiness.Services.MainPlateService;
using LogicBusiness.Services.RestaurantOrderService;
using LogicBusiness.Services.StarterService;
using OrderMonitor.CustonControlItem;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace OrderMonitor.InsertOrder
{
    public partial class FmInsertOrder : Form
    {

        #region "Services"

        private readonly CClientService _clientService;
        private readonly CStarterService _starterService;
        private readonly CMainPlateService _mainPlateService;
        private readonly CDessertService _dessertService;
        private readonly CDrinkService _drinkService;
        private readonly CRestaurantOrderService _orderService;

        #endregion

        #region Private Varibles

        private double subtotalValue;
        private double itbisValue;
        private double totalValue;

        #endregion


        public FmInsertOrder()
        {
            InitializeComponent();

            #region "Initializing Services"

            _starterService = new CStarterService();
            _mainPlateService = new CMainPlateService();
            _dessertService = new CDessertService();
            _drinkService = new CDrinkService();
            _clientService = new CClientService();
            _orderService = new CRestaurantOrderService();

            #endregion

            #region Initializing Varibles

            subtotalValue = 0;
            itbisValue = 0;
            totalValue = 0;

            #endregion

        }

        #region "Events"

        private async void FmInsertOrder_Load(object sender, EventArgs e)
        {
            await LoadComboBoxes();
            //await Filled();
        }

        private async void CbxStarter_SelectedIndexChanged(object sender, EventArgs e)
        {
            await Filled();
        }

        private async void CbxMainPlate_SelectedIndexChanged(object sender, EventArgs e)
        {
            await Filled();
        }

        private async void CbxDessert_SelectedIndexChanged(object sender, EventArgs e)
        {
            await Filled();
        }

        private async void CbxDrink_SelectedIndexChanged(object sender, EventArgs e)
        {
            await Filled();
        }

        private async void BtnSave_Click(object sender, EventArgs e)
        {
            await NewOrder();
        }

        #endregion

        #region "Private Methods"

        private async Task LoadComboBoxes()
        {

            ComboBoxItem DefaultOption = new ComboBoxItem
            {
                Text = "Seleccione una opción",
                Value = null
            };

            await LoadStarterComboBox(DefaultOption);
            await LoadMainPlateComboBox(DefaultOption);
            await LoadDessertComboBox(DefaultOption);
            await LoadDrinkComboBox(DefaultOption);

        }

        private async Task LoadStarterComboBox(ComboBoxItem DefaultOption)
        {
            DefaultOption.Text = "Entrada";
            CbxStarter.Items.Add(DefaultOption);

            List<Starter> starters = await _starterService.GetAll();

            starters.ForEach(x =>
            {
                ComboBoxItem item = new ComboBoxItem
                {
                    Text = x.Name,
                    Value = x.StarterId
                };
                CbxStarter.Items.Add(item);
            });

            CbxStarter.SelectedItem = DefaultOption;
        }

        private async Task LoadMainPlateComboBox(ComboBoxItem DefaultOption)
        {
            DefaultOption.Text = "Plato Fuerte";
            CbxMainPlate.Items.Add(DefaultOption);

            List<MainPlate> mainPlates = await _mainPlateService.GetAll();

            mainPlates.ForEach(x =>
            {
                ComboBoxItem item = new ComboBoxItem
                {
                    Text = x.Name,
                    Value = x.MainPlateId
                };
                CbxMainPlate.Items.Add(item);
            });

            CbxMainPlate.SelectedItem = DefaultOption;
        }

        private async Task LoadDessertComboBox(ComboBoxItem DefaultOption)
        {
            DefaultOption.Text = "Postre";
            CbxDessert.Items.Add(DefaultOption);

            List<Dessert> desserts = await _dessertService.GetAll();

            desserts.ForEach(x =>
            {
                ComboBoxItem item = new ComboBoxItem
                {
                    Text = x.Name,
                    Value = x.DessertId
                };
                CbxDessert.Items.Add(item);
            });

            CbxDessert.SelectedItem = DefaultOption;
        }

        private async Task LoadDrinkComboBox(ComboBoxItem DefaultOption)
        {
            DefaultOption.Text = "Bebida";
            CbxDrink.Items.Add(DefaultOption);

            List<Drink> drinks = await _drinkService.GetAll();

            drinks.ForEach(x =>
            {
                ComboBoxItem item = new ComboBoxItem
                {
                    Text = x.Name,
                    Value = x.DrinkId
                };
                CbxDrink.Items.Add(item);
            });

            CbxDrink.SelectedItem = DefaultOption;
        }

        private async Task CalculateSubtotal(string starter, string mainPlate, string dessert, string drink)
        {
            subtotalValue = 0;

            var starterResponse = await _starterService.FindByName(starter);
            if (starterResponse != null) subtotalValue += Convert.ToDouble(starterResponse.Cost);
            var mainPlateResponse = await _mainPlateService.FindByName(mainPlate);
            if (mainPlateResponse != null) subtotalValue += Convert.ToDouble(mainPlateResponse.Cost);
            var dessertResponse = await _dessertService.FindByName(dessert);
            if (dessertResponse != null) subtotalValue += Convert.ToDouble(dessertResponse.Cost);
            var drinResponse = await _drinkService.FindByName(drink);
            if (drinResponse != null) subtotalValue += Convert.ToDouble(drinResponse.Cost);

            CalculateItbis(subtotalValue);
        }

        private void CalculateItbis(double subtotal)
        {
            itbisValue = 0;

            itbisValue = subtotal * 0.28;

            CalculateTotal(subtotal, itbisValue);
        }

        private void CalculateTotal(double subtotal, double itbis)
        {
            totalValue = 0;

            totalValue = subtotal + itbis;
        }

        private void FillingLabels(double subtotal, double itbis, double total)
        {
            LblSubtotalValue.Text = $"{subtotal}$";
            LblItbisValue.Text = $"{itbis}$";
            LblTotalValue.Text = $"{total}$";
        }

        private async Task Filled()
        {
            if (CbxStarter.SelectedItem != null && CbxMainPlate.SelectedItem != null 
                && CbxDessert.SelectedItem != null && CbxDrink.SelectedItem != null)
            {
                await CalculateSubtotal(CbxStarter.SelectedItem.ToString(), CbxMainPlate.SelectedItem.ToString(),
                CbxDessert.SelectedItem.ToString(), CbxDrink.SelectedItem.ToString());
                FillingLabels(subtotalValue, itbisValue, totalValue);
            }
        } 

        private async Task NewOrder()
        {
            bool responseClient = true;

            if (CbxStarter.SelectedIndex != 0 && CbxMainPlate.SelectedIndex != 0 
                && CbxDessert.SelectedIndex != 0 && CbxDrink.SelectedIndex != 0
                && TxtClient.TextLength > 0)
            {
                Client client = new Client { Name = TxtClient.Text };

                var existingClient = await _clientService.FindByName(client.Name);

                if (existingClient == null)
                {
                    responseClient = await _clientService.Insert(client);
                }

                if (responseClient)
                {
                    var lastClient = await _clientService.GetLast();

                    if (lastClient != null)
                    {
                        ComboBoxItem selectedStarter = (ComboBoxItem)CbxStarter.SelectedItem;
                        ComboBoxItem selectedMainPlate = (ComboBoxItem)CbxMainPlate.SelectedItem;
                        ComboBoxItem selectedDessert = (ComboBoxItem)CbxDessert.SelectedItem;
                        ComboBoxItem selectedDrink = (ComboBoxItem)CbxDrink.SelectedItem;

                        decimal subtotal = Convert.ToDecimal(subtotalValue);
                        decimal itbis = Convert.ToDecimal(itbisValue);
                        decimal total = Convert.ToDecimal(totalValue);

                        RestaurantOrder order = new RestaurantOrder
                        {
                            ClientId = lastClient.ClientId,
                            StarterId = Convert.ToInt32(selectedStarter.Value),
                            MainPlateId = Convert.ToInt32(selectedMainPlate.Value),
                            DessertId = Convert.ToInt32(selectedDessert.Value),
                            DrinkId = Convert.ToInt32(selectedDrink.Value),
                            Subtotal = subtotal,
                            Itbis = itbis,
                            Total = total
                        };

                        var responseOrder = await _orderService.Insert(order);

                        if (responseOrder)
                        {
                            MessageBox.Show("Insertado Correctamente.", "Notificación");
                        }
                        else
                        {
                            MessageBox.Show("Ha ocurrido un error.", "Error");
                        }

                        Clean();
                        this.Close();

                    }
                    else
                    {
                        MessageBox.Show("Ha ocurrido un error.", "Error");
                    }
                }
                else
                {
                    MessageBox.Show("Ha ocurrido un error.", "Error");
                }


            }
            else
            {
                MessageBox.Show("Llenar todos los campos.", "Advertencia");
            }

        }

        private void Clean()
        {
            TxtClient.Clear();
            TxtClient.Focus();

            CbxStarter.SelectedIndex = 0;
            CbxMainPlate.SelectedIndex = 0;
            CbxDessert.SelectedIndex = 0;
            CbxDrink.SelectedIndex = 0;
        }

        #endregion

    }
}
