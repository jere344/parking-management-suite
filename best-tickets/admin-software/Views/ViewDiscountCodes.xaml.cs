using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using admintickets.ViewModels;
using ticketlibrary.Models;

namespace admintickets.Views
{
    public partial class ViewDiscountCodes : Page
    {
        public ViewDiscountCodes()
        {
            InitializeComponent();
            DataContext = new VMDiscountCodes();
        }
    }
}