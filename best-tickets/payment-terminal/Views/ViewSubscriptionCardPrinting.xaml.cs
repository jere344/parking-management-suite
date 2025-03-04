using System.Windows.Controls;

namespace paymentterminal.Views
{
    public partial class ViewSubscriptionCardPrinting : Page
    {
        public ViewSubscriptionCardPrinting()
        {
            InitializeComponent();
            DataContext = new ViewModels.VMSubscriptionCardPrinting();
        }
    }
}
