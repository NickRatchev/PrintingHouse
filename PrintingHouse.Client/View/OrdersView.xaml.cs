namespace PrintingHouse.Client.View
{
    using Models;
    using Data.Calculations;
    using System.Collections.Generic;
    using System.Windows;
    using System.Windows.Controls;

    public partial class OrdersView : UserControl
    {
        public OrdersView()
        {
            InitializeComponent();
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
            // Claculate materials consumption
            string platesKg = Calculations.CalculatePlates(components).ToString("N");
            string blindsKg = Calculations.CalculateBlinds(components).ToString("N");
            string paperKg = Calculations.CalculatePaperKg(components).ToString("N");
            string paperWasteKg = Calculations.CalculatePaperWasteKg(components).ToString("N");
            string blackInkKg = Calculations.CalculateBlackInkKg(components).ToString("N");
            string colorInkKg = Calculations.CalculateColorInkKg(components).ToString("N");
            string wischwasserKg = Calculations.CalculateWischwasserKg(components).ToString("N");
            string foilKg = Calculations.CalculateFoilKg(components).ToString("N");
            string tapeMeters = Calculations.CalculateTapeMeters(components).ToString("N");

            txtBlockPlatesPcs.Text = platesKg + "  pcs";
            txtBlockBlindsPcs.Text = blindsKg + "  pcs";
            txtBlockPaperKg.Text = paperKg + "  kg";
            txtBlockPaperWasteKg.Text = paperWasteKg + "  kg";
            txtBlockInkBlackKg.Text = blackInkKg + "  kg";
            txtBlockInkColorKg.Text = colorInkKg + "  kg";
            txtBlockWischwasserKg.Text = wischwasserKg + "  kg";
            txtBlockFoilKg.Text = foilKg + "  kg";
            txtBlockTapeM.Text = tapeMeters + "  m";
        }
    }
}
