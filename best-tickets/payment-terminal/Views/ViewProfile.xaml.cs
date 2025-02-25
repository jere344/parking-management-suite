using System.Windows;
using System.Windows.Controls;
using paymentterminal.ViewModels;

namespace paymentterminal.Views
{
    public partial class ViewProfile : Page
    {
        public ViewProfile()
        {
            InitializeComponent();
            DataContext = new VMProfile();
            this.Loaded += Page_Loaded;
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            // Set the window title and size as needed.
            ((MainWindow)Application.Current.MainWindow).SetWindowInfos((DataContext as VMProfile).FullName, 700, 900);
        }

        private void UpdatePassword_Click(object sender, RoutedEventArgs e)
        {
            if (DataContext is VMProfile vm)
            {
                // Assign values from the PasswordBoxes to the view model.
                vm.CurrentPassword = CurrentPasswordBox.Password;
                vm.NewPassword = NewPasswordBox.Password;
                vm.ConfirmPassword = ConfirmPasswordBox.Password;
                
                if (vm.ChangePasswordCommand.CanExecute(null))
                {
                    vm.ChangePasswordCommand.Execute(null);
                }
            }
        }
    }
}
