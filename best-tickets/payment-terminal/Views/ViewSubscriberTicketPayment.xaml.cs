using System.Windows.Controls;

namespace paymentterminal.Views
{
    public partial class ViewSubscriberTicketPayment : Page
    {
        public ViewSubscriberTicketPayment()
        {
            InitializeComponent();
            DataContext = new ViewModels.VMSubscriberTicketPayment();
        }
    }
}
