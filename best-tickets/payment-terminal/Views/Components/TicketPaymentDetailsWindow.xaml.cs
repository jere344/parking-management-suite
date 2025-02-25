using System.Windows;
using ticketlibrary.Models;

namespace paymentterminal.Views
{
    public partial class TicketPaymentDetailsWindow : Window
    {
        public TicketPaymentDetailsWindow(TicketPayment payment)
        {
            InitializeComponent();
            DataContext = payment;
        }
    }
}
