using System.Windows.Controls;

namespace paymentterminal.Views
{
    public partial class ViewHome : Page
    {
        public ViewHome()
        {
            InitializeComponent();
            DataContext = new ViewModels.VMHome();
        }
    }
}
