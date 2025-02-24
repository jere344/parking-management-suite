using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using admintickets.ViewModels;
using admintickets.Models.DBModels;

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