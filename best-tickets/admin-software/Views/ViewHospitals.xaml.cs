using System.Windows;
using System.Windows.Controls;
using admintickets.ViewModels;
using ticketlibrary.Models;

namespace admintickets.Views
{
    public partial class ViewHospitals : Page
    {
        public ViewHospitals()
        {
            InitializeComponent();
            DataContext = new VMHospitals();
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
                }
            }
        }

        private void AddHospitalButton_Click(object sender, RoutedEventArgs e)
        {
            if (DataContext is VMHospitals vm)
            {
                vm.AddHospitalCommand.Execute(null);
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
