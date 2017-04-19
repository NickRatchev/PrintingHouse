namespace PrintingHouse.Client
{
    using Data;
    using System.Collections.Generic;
    using System.Windows;
    using System.Windows.Controls;

    public partial class ClientDataWindow : Window
    {
        //PrintingHouseDbStore context = PrintingHouseDbStore.context;

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
                DialogResult = true;

                txtBoxCompanyName.GetBindingExpression(TextBox.TextProperty).UpdateSource();
                txtBoxVatNumber.GetBindingExpression(TextBox.TextProperty).UpdateSource();
                txtBoxTown.GetBindingExpression(TextBox.TextProperty).UpdateSource();
                txtBoxAddress.GetBindingExpression(TextBox.TextProperty).UpdateSource();
                txtBoxContactPerson.GetBindingExpression(TextBox.TextProperty).UpdateSource();
                txtBoxPhoneNumbers.GetBindingExpression(TextBox.TextProperty).UpdateSource();
                checkBoxActive.GetBindingExpression(CheckBox.IsCheckedProperty).UpdateSource();

                Close();
            }
        }
    }
}
