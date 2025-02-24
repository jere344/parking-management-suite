using System.Windows;
using System.Windows.Controls;
using admintickets.ViewModels;

namespace admintickets.Views
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