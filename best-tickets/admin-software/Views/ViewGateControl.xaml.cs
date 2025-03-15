using System.Windows.Controls;
using admintickets.ViewModels;

namespace admintickets.Views
{
    public partial class ViewGateControl : Page
    {
        public ViewGateControl()
        {
            InitializeComponent();
            DataContext = new VMGateControl();
        }
    }
}
