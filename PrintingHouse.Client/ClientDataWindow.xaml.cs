namespace PrintingHouse.Client
{
    using Data;
    using Models;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Windows;
    using System.Windows.Controls;

    public partial class ClientDataWindow : Window
    {
        PrintingHouseDbStore store = new PrintingHouseDbStore();

        public ClientDataWindow()
        {
            InitializeComponent();

        }

        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            // Undo TextBox changes
            IEnumerable<TextBox> txtBoxes = Utils.FindVisualChildren<TextBox>(gridClientData);
            foreach (TextBox txtBox in txtBoxes)
            {
                txtBox.Undo();
            }

            /*DependencyPropertyDescriptor
                .FromProperty(RadioButton.IsCheckedProperty, typeof(RadioButton))
                .AddValueChanged(radioButton2, (s, t) => { if ((bool)radioButton2.IsChecked) radioButton2.IsChecked = false; });*/

            Close();
        }

        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            if (txtBoxCompanyName.Text == "")
            {
                MessageBox.Show("You must fill a Company Name!");
            }
            else
            {
                Button b = sender as Button;
                Client c = (Client)((Button)e.Source).DataContext;
                Client client = store.GetClient(c.Id);
                client.CompanyName = txtBoxCompanyName.Text;
                store.SaveChanges();

                Close();
            }
        }
    }
}
