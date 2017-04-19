namespace PrintingHouse.Client.ViewModel
{
    using Data;
    using Models;
    using System.Collections.ObjectModel;
    using System.Windows;
    using System.Windows.Data;

    public class OrdersViewModel : BaseViewModel
    {
        public OrdersViewModel()
        {
            Orders = new ObservableCollection<Order>(PrintingHouseDbStore.GetOrders());
            OrderCreate = new RelayCommand(OrderCreateCommand);
            OrderDelete = new RelayCommandParam<object>(OrderDeleteCommand);
            OrderEdit = new RelayCommandParam<object>(OrderEditCommand);
            CollectionViewOrders = new CollectionViewSource();
            CollectionViewOrders.Source = Orders;
        }

        public static CollectionViewSource CollectionViewOrders { get; set; }

        public ObservableCollection<Order> Orders { get; set; }

        public RelayCommand OrderCreate { get; set; }

        public RelayCommandParam<object> OrderDelete { get; set; }

        public RelayCommandParam<object> OrderEdit { get; set; }

        public void OrderCreateCommand(object obj)
        {
            Order order = new Order();
            PrintingHouseDbStore.context.Orders.Add(order);
            OrderDataWindow orderDataWindow = new OrderDataWindow();
            orderDataWindow.DataContext = order;
            orderDataWindow.ShowDialog();
            Orders.Add(order);
        }

        private void OrderDeleteCommand(object obj)
        {
            Order order = (Order)obj;
            string message = $"Are you sure you want to delete order {order.Product.Title}";
            MessageBoxResult messageBoxResult =
                MessageBox.Show(message, "Delete Confirmation", MessageBoxButton.YesNo);
            if (messageBoxResult == MessageBoxResult.Yes)
            {
                PrintingHouseDbStore.DeleteOrder(order.Id);
                Orders.Remove(order);
            }
        }

        private void OrderEditCommand(object obj)
        {
            Order order = (Order)obj;
            OrderDataWindow orderDataWindow = new OrderDataWindow();
            orderDataWindow.DataContext = order;
            orderDataWindow.ShowDialog();
        }

        private string filterString;
        public string SearchFilter
        {
            get { return filterString; }
            set
            {
                filterString = value;
                if (!string.IsNullOrEmpty(SearchFilter))
                {
                    AddFilter();
                }

                CollectionViewOrders.View.Refresh();
            }
        }

        private void AddFilter()
        {
            CollectionViewOrders.Filter -= new FilterEventHandler(Filter);
            CollectionViewOrders.Filter += new FilterEventHandler(Filter);
        }

        private void Filter(object sender, FilterEventArgs e)
        {
            Order order = e.Item as Order;
            if (order.Client.CompanyName.ToLower().Contains(SearchFilter.ToLower()) ||
                    order.Product.Title.ToLower().Contains(SearchFilter.ToLower()))
            {
                e.Accepted = true;
            }
            else
            {
                e.Accepted = false;
            }
        }
    }
}
