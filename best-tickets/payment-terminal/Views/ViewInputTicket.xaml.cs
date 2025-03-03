using System.Windows.Controls;

namespace paymentterminal.Views
{
    public partial class ViewInputTicket : Page
    {
        public ViewInputTicket()
        {
            InitializeComponent();
            DataContext = new ViewModels.VMInputTicket();
        }
    }
}
