using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using paymentterminal.ViewModels;
using ticketlibrary.Models;

namespace paymentterminal.Views
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