using System.Windows.Controls;

namespace paymentterminal.Views
{
    public partial class ViewSingleTicketPayment : Page
    {
        public ViewSingleTicketPayment()
        {
            InitializeComponent();
            DataContext = new ViewModels.VMSingleTicketPayment();
        }
    }
}
