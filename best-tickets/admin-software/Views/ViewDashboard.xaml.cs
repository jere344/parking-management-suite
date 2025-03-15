using System.Windows.Controls;
using admintickets.ViewModels;

namespace admintickets.Views
{
    public partial class ViewDashboard : Page
    {
        public ViewDashboard()
        {
            InitializeComponent();
            DataContext = new VMDashboard();
        }
    }
}
