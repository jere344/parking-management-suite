using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using paymentterminal.ViewModels;
using ticketlibrary.Models;

namespace paymentterminal.Views
{
    public partial class ViewHospitals : Page
    {
        public ViewHospitals()
        {
            InitializeComponent();
            DataContext = new VMHospitals();
        }

        // Update the view model when the password changes.
        private void NewHospitalPasswordBox_PasswordChanged(object sender, RoutedEventArgs e)
        {
            if (DataContext is VMHospitals vm)
            {
                vm.NewHospitalPassword = ((PasswordBox)sender).Password;
            }
        }

        // Update the view model when the password changes for a specific hospital.
        private void EditHospitalPasswordBox_PasswordChanged(object sender, RoutedEventArgs e)
        {
            if (DataContext is VMHospitals vm && ((PasswordBox)sender).DataContext is Hospital hospital)
            {
                hospital.Password = ((PasswordBox)sender).Password;
            }
        }

        // Handle the click event for the Edit Password button.
        private void EditPasswordButton_Click(object sender, RoutedEventArgs e)
        {
            if (DataContext is VMHospitals vm && ((Button)sender).DataContext is Hospital hospital)
            {
                MessageBoxResult result = MessageBox.Show("Changing the password will disconnect the ticket machines. Are you sure you want to continue?", "Change Password", MessageBoxButton.YesNo, MessageBoxImage.Warning);
                if (result == MessageBoxResult.Yes)
                {
                    vm.EditHospitalPasswordCommand.Execute(hospital);
                    var passwordBox = (PasswordBox)((FrameworkElement)((Button)sender).Parent).FindName("EditHospitalPasswordBox");
                    if (passwordBox != null)
                    {
                        passwordBox.Password = string.Empty;
                    }
                }
            }
        }

        // we pass through the .xaml.cs instead of directly call the command because we need to clear the password field after the command is executed
        private void AddHospitalButton_Click(object sender, RoutedEventArgs e)
        {
            if (DataContext is VMHospitals vm)
            {
                vm.AddHospitalCommand.Execute(null);
                vm.NewHospitalPassword = "";
            }
        }

        private void ToggleAddHospitalButton_Click(object sender, RoutedEventArgs e)
        {
            if (AddHospitalCard.Visibility == Visibility.Collapsed)
            {
                AddHospitalCard.Visibility = Visibility.Visible;
                ToggleAddHospitalButton.Content = "Hide Add New Hospital";
            }
            else
            {
                AddHospitalCard.Visibility = Visibility.Collapsed;
                ToggleAddHospitalButton.Content = "Show Add New Hospital";
            }
        }
    }
}
