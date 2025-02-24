using System.Windows;
using admintickets.Models.DBModels;

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
