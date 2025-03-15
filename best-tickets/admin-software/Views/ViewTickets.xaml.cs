using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Input;
using admintickets.ViewModels;
using System.Windows;

namespace admintickets.Views
{
    public partial class ViewTickets : Page
    {
        public ViewTickets()
        {
            InitializeComponent();
            DataContext = new VMTickets();
        }

        private void DataGridColumnHeader_Click(object sender, RoutedEventArgs e)
        {
            if (sender is DataGridColumnHeader header && header.Tag is string columnName)
            {
                var viewModel = DataContext as VMTickets;
                viewModel?.SortCommand.Execute(columnName);
            }
        }

        private void ItemsPerPage_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (DataContext is VMTickets viewModel && sender is ComboBox comboBox && 
                comboBox.SelectedItem is ComboBoxItem selectedItem)
            {
                if (int.TryParse(selectedItem.Content.ToString(), out int pageSize))
                {
                    viewModel.ItemsPerPage = pageSize;
                    viewModel.RefreshCommand.Execute(null);
                }
            }
        }
    }
}
