using System.Windows;
using ticketlibrary.Models;

namespace admintickets.Views
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
