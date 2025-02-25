using System.Windows;
using System.Windows.Controls;
using GatesSoftware.ViewModels;

namespace GatesSoftware.Views
{
    public partial class FirstSetup : Page
    {
        public FirstSetup()
        {
            InitializeComponent();
            DataContext = new FirstSetupViewModel();
        }

        private void PasswordBox_PasswordChanged(object sender, RoutedEventArgs e)
        {
            if (DataContext is FirstSetupViewModel vm)
            {
                vm.Password = ((PasswordBox)sender).Password;
            }
        }
    }
}
