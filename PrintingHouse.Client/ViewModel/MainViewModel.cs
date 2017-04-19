namespace PrintingHouse.Client.ViewModel
{
    using System.Collections.ObjectModel;
    using View;

    public class MainViewModel : BaseViewModel
    {
        public ObservableCollection<TabItem> Tabs { get; set; }

        public MainViewModel()
        {
            Tabs = new ObservableCollection<TabItem>
            {
                new TabItem{Content = new ClientsView() {DataContext = new ClientsViewModel()}, Header = "Clients"},
                new TabItem{Content = new OrdersView() {DataContext = new OrdersViewModel()}, Header = "Orders"}
            };
        }
    }

    public class TabItem
    {
        public string Header { get; set; }

        public object Content { get; set; }
    }
}
