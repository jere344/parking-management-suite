using System.Windows.Controls;
using admintickets.ViewModels;

namespace admintickets.Views
{
    public partial class ViewSubscription : Page
    {
        public ViewSubscription()
        {
            InitializeComponent();
            DataContext = new VMSubscription();
        }
    }
}
