namespace PrintingHouse.Client.ViewModel
{
    using Data;
    using Models;
    using System;
    using System.Collections.ObjectModel;
    using System.Linq;
    using System.Windows;
    using System.Windows.Data;

    public class ClientsViewModel : BaseViewModel
    {
        public ClientsViewModel()
        {
            Clients = new ObservableCollection<Client>(PrintingHouseDbStore.GetClients());
            ClientCancel = new RelayCommand(ClientCancelCommand);
            ClientCreate = new RelayCommand(ClientCreateCommand);
            ClientEdit = new RelayCommandParam<object>(ClientEditCommand);
            CollectionViewClients = new CollectionViewSource();
            CollectionViewClients.Source = Clients;
        }

        public static CollectionViewSource CollectionViewClients { get; set; }

        public static ObservableCollection<Client> Clients { get; set; }

        public RelayCommand ClientCreate { get; set; }

        public RelayCommand ClientCancel { get; set; }

        public RelayCommandParam<object> ClientEdit { get; set; }

        public void ClientCancelCommand(object obj)
        {
            MessageBox.Show("click cancel");
        }

        public void ClientCreateCommand(object obj)
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

        private void ClientEditCommand(object obj)
        {
            Client client = (Client)obj;
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

        private string filterString;

        private bool filterActive;
        
        

        public string SearchFilter
        {
            get { return filterString; }
            set
            {
                filterString = value;
                if (!string.IsNullOrEmpty(SearchFilter))
                {
                    AddFilterByName();
                }

                CollectionViewClients.View.Refresh();
            }
        }

        public bool SearchFilterByActive
        {
            get { return filterActive; }
            set
            {
                filterActive = value;
                if (filterActive == true)
                {
                    AddFilterByActive();
                }
                
                //CollectionViewClients.Source = new ObservableCollection<Client>(PrintingHouseDbStore.GetClients());
                CollectionViewClients.View.Refresh();
            }
        }

        private void AddFilterByName()
        {
            CollectionViewClients.Filter -= new FilterEventHandler(FilterByName);
            CollectionViewClients.Filter += new FilterEventHandler(FilterByName);
        }

        private void AddFilterByActive()
        {
            CollectionViewClients.Filter -= new FilterEventHandler(FilterByActive);
            CollectionViewClients.Filter += new FilterEventHandler(FilterByActive);
        }

        private void FilterByName(object sender, FilterEventArgs e)
        {
            Client client = e.Item as Client;
            if (client.CompanyName.IndexOf(SearchFilter, StringComparison.OrdinalIgnoreCase) >= 0)
            {
                e.Accepted = true;
            }
            else
            {
                e.Accepted = false;
            }
        }

        private void FilterByActive(object sender, FilterEventArgs e)
        {
            Client client = e.Item as Client;
            if (client.IsActive)
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
