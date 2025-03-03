using System.Windows.Controls;
using paymentterminal.ViewModels;

namespace paymentterminal.Views
{
    public partial class ViewSubscriptionTierSelection : Page
    {
        public ViewSubscriptionTierSelection()
        {
            InitializeComponent();
            DataContext = new VMSubscriptionTierSelection();
        }
    }
}
