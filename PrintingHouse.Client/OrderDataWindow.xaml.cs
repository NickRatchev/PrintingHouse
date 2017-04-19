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
    using System.Text.RegularExpressions;

    public partial class OrderDataWindow : Window
    {
        public ObservableCollection<Client> Clients = new ObservableCollection<Client>();
        public ObservableCollection<MachineData> MachineData = new ObservableCollection<MachineData>();
        public ObservableCollection<Paper> Papers = new ObservableCollection<Paper>();

        public OrderDataWindow()
        {
            InitializeComponent();

            Clients = new ObservableCollection<Client>(PrintingHouseDbStore.GetClients());
            MachineData = new ObservableCollection<MachineData>(PrintingHouseDbStore.GetMachineData());
            Papers = new ObservableCollection<Paper>(PrintingHouseDbStore.GetPapers());
            comboBoxClients.ItemsSource = Clients;
            comboBoxNumberOfPages.ItemsSource = MachineData;
            comboBoxPapers.ItemsSource = Papers;
        }

        private void DatePicker_SelectedDateChanged(object sender,
            SelectionChangedEventArgs e)
        {
            // Get DatePicker reference.
            var picker = sender as DatePicker;

            // Get nullable DateTime from SelectedDate.
            DateTime? date = picker.SelectedDate;
            if (date == null)
            {
                // A null object.
                Title = "No date";
            }
            else
            {
                // No need to display the time.
                Title = date.Value.ToShortDateString();
            }
        }

        // Validate input in TextBox Print Run
        private void NumberValidationTextBox(object sender, TextCompositionEventArgs e)
        {
            Regex regex = new Regex("[^0-9]+");
            e.Handled = regex.IsMatch(e.Text);
        }

        private static bool IsTextAllowed(string text)
        {
            Regex regex = new Regex("[^0-9.-]+"); //regex that matches disallowed text
            return !regex.IsMatch(text);
        }

        // Validate pasting in TextBox Print Run
        private void TextBoxPasting(object sender, DataObjectPastingEventArgs e)
        {
            if (e.DataObject.GetDataPresent(typeof(String)))
            {
                String text = (String)e.DataObject.GetData(typeof(String));
                if (!IsTextAllowed(text))
                {
                    e.CancelCommand();
                }
            }
            else
            {
                e.CancelCommand();
            }
        }

        private void btnCancelOrder_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void btnSaveOrder_Click(object sender, RoutedEventArgs e)
        {
            if (comboBoxClients.Text == "")
            {
                MessageBox.Show("You must select a Company Name!");
            }
            else
            {
                PrintingHouseDbStore.SaveChanges();
                Close();
            }
        }
    }
}
