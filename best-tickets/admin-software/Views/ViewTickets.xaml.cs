using System.Windows.Controls;
using admintickets.ViewModels;

namespace admintickets.Views
{
    public partial class ViewTickets : Page
    {
        public ViewTickets()
        {
            InitializeComponent();
            DataContext = new VMTickets();
        }
    }
}
