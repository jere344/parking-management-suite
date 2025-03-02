using System.Windows.Controls;

namespace paymentterminal.Views
{
    public partial class ViewSingleTicket : Page
    {
        public ViewSingleTicket()
        {
            InitializeComponent();
            DataContext = new ViewModels.VMSingleTicket();
        }
    }
}
