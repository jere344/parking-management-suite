using System.Windows.Controls;
using admintickets.ViewModels;

namespace admintickets.Views
{
    public partial class PriceBrackets : Page
    {
        public PriceBrackets()
        {
            InitializeComponent();
            DataContext = new VMPriceBrackets();
        }
    }
}
