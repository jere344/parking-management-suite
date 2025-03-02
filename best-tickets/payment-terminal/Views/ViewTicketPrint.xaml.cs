using System.Windows.Controls;

namespace paymentterminal.Views
{
    public partial class ViewTicketPrint : Page
    {
        public ViewTicketPrint()
        {
            InitializeComponent();
            DataContext = new ViewModels.VMTicketPrint();
        }
    }
}
