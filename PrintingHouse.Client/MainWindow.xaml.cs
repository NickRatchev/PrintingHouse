namespace PrintingHouse.Client
{
    using Data;
    using Data.Calculations;
    using Models;
    using System;
    using System.Collections.ObjectModel;
    using System.Windows;
    using System.Windows.Controls;
    using System.Windows.Data;
    using System.Collections.Generic;
    using System.Windows.Input;
    using System.Linq;

    public partial class MainWindow : Window
    {
        //PrintingHouseDbStore store = new PrintingHouseDbStore();
        public ObservableCollection<Client> Clients;
        public ObservableCollection<Component> components;
        public ObservableCollection<Order> orders;

        public ICollection<Client> ClientsList;

        public MainWindow()
        {
            InitializeComponent();

            PrintingHouseDbStore.Initialize();

            ClientsList = PrintingHouseDbStore.GetClients();
            Clients = new ObservableCollection<Client>(ClientsList.Where(c => c.IsActive == true).ToList());
            //Clients = new ObservableCollection<Client>(ClientsList);
            //Clients = new ObservableCollection<Client>(PrintingHouseDbStore.GetClients());

            components = new ObservableCollection<Component>(PrintingHouseDbStore.GetComponents());
            orders = new ObservableCollection<Order>(PrintingHouseDbStore.GetOrders());
            
            listClients.ItemsSource = Clients;

            listOrders.ItemsSource = orders;

            CollectionView viewClients = (CollectionView)CollectionViewSource.GetDefaultView(listClients.ItemsSource);
            CollectionView viewOrders = (CollectionView)CollectionViewSource.GetDefaultView(listOrders.ItemsSource);

            viewClients.Filter = FilterClientByName;
            viewOrders.Filter = FilterOrder;

            viewClients.Filter = FilterClientByActive;
        }

        private void btnAddClient_Click(object sender, RoutedEventArgs e)
        {
            Client client = new Client();

            ClientDataWindow clientDataWindow = new ClientDataWindow();
            clientDataWindow.DataContext = client;
            clientDataWindow.ShowDialog();

            if (clientDataWindow.DialogResult == true)
            {                
                Clients.Add(client);
                PrintingHouseDbStore.context.Clients.Add(client);
                PrintingHouseDbStore.SaveChanges();
            }            
        }

        private void btnEditClient_Click(object sender, RoutedEventArgs e)
        {
            Button b = sender as Button;
            Client client = (Client)((Button)e.Source).DataContext;            

            ClientDataWindow clientDataWindow = new ClientDataWindow();
            clientDataWindow.DataContext = client;
            clientDataWindow.ShowDialog();

            if (clientDataWindow.DialogResult == true)
            {
                MessageBox.Show("Save");
                PrintingHouseDbStore.SaveChanges();
            }
            else
            {
                MessageBox.Show("Cancel");                          
            }
        }

        private void btnCreateNewOrder_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("ToDo");
        }

        private void btnEditOrder_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("ToDo");
        }

        private bool FilterClientByName(object item)
        {
            if (string.IsNullOrEmpty(txtFilterClient.Text))
            {
                return true;
            }
            else
            {
                return ((item as Client).CompanyName.IndexOf(txtFilterClient.Text, StringComparison.OrdinalIgnoreCase) >= 0);
            }
        }

        private bool FilterClientByActive(object item)
        {
            if (!(bool)checkOnlyActive.IsChecked)
            {
                return true;
            }
            else
            {
                return (item as Client).IsActive == true;
            }
        }

        private bool FilterOrder(object item)
        {
            if (string.IsNullOrEmpty(txtFilterOrder.Text))
            {
                return true;
            }
            else
            {
                Order order = item as Order;
                return order.Client.CompanyName.ToLower().Contains(txtFilterOrder.Text.ToLower()) ||
                    order.Product.Title.ToLower().Contains(txtFilterOrder.Text.ToLower());
            }
        }

        private void OnTextChangedClient(object sender, TextChangedEventArgs e)
        {
            CollectionViewSource.GetDefaultView(listClients.ItemsSource).Refresh();
        }

        private void OnTextChangedOrder(object sender, TextChangedEventArgs e)
        {
            CollectionViewSource.GetDefaultView(listOrders.ItemsSource).Refresh();
        }

        // Populate Calculations for selected order in ListView Orders
        private void listView_Click(object sender, RoutedEventArgs e)
        {
            var item = (sender as ListView).SelectedItem;
            if (item != null)
            {
                Order order = (Order)listOrders.SelectedItems[0];
                GetCalculations(order.Components);
            }
        }

        // Arrange columns with in ListViews - CompanyName fills remaining space
        private void ListView_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            ListView listView = sender as ListView;
            GridView gridView = listView.View as GridView;

            double remainingWidth = listView.ActualWidth - SystemParameters.VerticalScrollBarWidth;

            int col = 0;
            for (int i = 0; i < gridView.Columns.Count; i++)
            {
                if (gridView.Columns[i].Header.ToString() == "Company Name")
                {
                    col = i;
                    continue;
                }

                remainingWidth -= gridView.Columns[i].ActualWidth;
            }

            gridView.Columns[col].Width = remainingWidth;
        }

        private void GetCalculations(ICollection<Component> components)
        {
            // Claculate material consumption
            string calculatePlates = Calculations.CalculatePlates(components).ToString("N");
            string calculateBlinds = Calculations.CalculateBlinds(components).ToString("N");
            string calculatePaperKg = Calculations.CalculatePaperKg(components).ToString("N");
            string calculatePaperWasteKg = Calculations.CalculatePaperWasteKg(components).ToString("N");
            string calculateBlackInkKg = Calculations.CalculateBlackInkKg(components).ToString("N");
            string calculateColorInkKg = Calculations.CalculateColorInkKg(components).ToString("N");
            string calculateWischwasserKg = Calculations.CalculateWischwasserKg(components).ToString("N");
            string calculateFoilKg = Calculations.CalculateFoilKg(components).ToString("N");
            string calculateTapeMeters = Calculations.CalculateTapeMeters(components).ToString("N");

            txtBlockPlatesPcs.Text = calculatePlates + "  pcs";
            txtBlockBlindsPcs.Text = calculateBlinds + "  pcs";
            txtBlockPaperKg.Text = calculatePaperKg + "  kg";
            txtBlockPaperWasteKg.Text = calculatePaperWasteKg + "  kg";
            txtBlockInkBlackKg.Text = calculateBlackInkKg + "  kg";
            txtBlockInkColorKg.Text = calculateColorInkKg + "  kg";
            txtBlockWischwasserKg.Text = calculateWischwasserKg + "  kg";
            txtBlockFoilKg.Text = calculateFoilKg + "  kg";
            txtBlockTapeM.Text = calculateTapeMeters + "  m";
        }

        private void OnFilterActiveClick(object sender, RoutedEventArgs e)
        {
            if (!(bool)checkOnlyActive.IsChecked)
            {
                Clients = new ObservableCollection<Client>(ClientsList);

                listClients.ItemsSource = Clients;
                CollectionView viewClients = (CollectionView)CollectionViewSource.GetDefaultView(listClients.ItemsSource);
                viewClients.Filter = FilterClientByName;                
                CollectionViewSource.GetDefaultView(listClients.ItemsSource).Refresh();
            }
            else
            {
                Clients = new ObservableCollection<Client>(ClientsList.Where(c => c.IsActive == true).ToList());
                listClients.ItemsSource = Clients;
                CollectionView viewClients = (CollectionView)CollectionViewSource.GetDefaultView(listClients.ItemsSource);
                viewClients.Filter = FilterClientByName;
                CollectionViewSource.GetDefaultView(listClients.ItemsSource).Refresh();
            }
        }
    }
}
