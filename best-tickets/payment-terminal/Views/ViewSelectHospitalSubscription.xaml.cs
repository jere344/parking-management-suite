using System.Windows.Controls;
using paymentterminal.ViewModels;

namespace paymentterminal.Views
{
    public partial class ViewSelectHospitalSubscription : Page
    {
        public ViewSelectHospitalSubscription()
        {
            InitializeComponent();
            DataContext = new VMSelectHospitalSubscription();
        }
    }
}
