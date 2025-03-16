using System.Windows.Controls;
using admintickets.ViewModels;

namespace admintickets.Views
{
    public partial class ViewTaxes : Page
    {
        public ViewTaxes()
        {
            InitializeComponent();
            DataContext = new VMTaxes();
        }
    }
}
