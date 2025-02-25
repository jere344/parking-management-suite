using System.Windows;
using System.Windows.Controls;
using paymentterminal.ViewModels;

namespace paymentterminal.Views
{
    public partial class ViewSubscriptionTiers : Page
    {
        public ViewSubscriptionTiers()
        {
            InitializeComponent();
            DataContext = new VMSubscriptionTiers();
        }

    }
}