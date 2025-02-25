using System.Windows.Controls;
using paymentterminal.ViewModels;

namespace paymentterminal.Views
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
