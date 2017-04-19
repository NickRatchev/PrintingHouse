namespace PrintingHouse.Client.View
{
    using System.Windows;
    using System.Windows.Controls;

    public partial class ClientsView : UserControl
    {
        public ClientsView()
        {
            InitializeComponent();
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
    }
}
