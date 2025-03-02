using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System.Windows.Input;

namespace paymentterminal.ViewModels
{
    public class VMHome : ObservableObject
    {
        public ICommand NavigateToSingleTicketCommand { get; }
        public ICommand NavigateToTicketWithSubscriptionCommand { get; }
        public ICommand NavigateToBuySubscriptionCommand { get; }

        public VMHome()
        {
            NavigateToSingleTicketCommand = new RelayCommand(NavigateToSingleTicket);
            NavigateToTicketWithSubscriptionCommand = new RelayCommand(NavigateToTicketWithSubscription);
            NavigateToBuySubscriptionCommand = new RelayCommand(NavigateToBuySubscription);
        }

        private void NavigateToSingleTicket()
        {
            // Navigation to the "Créditer un ticket unique" page.
            ((MainWindow)App.Current.MainWindow).NavigateTo("Views/ViewSingleTicket.xaml");
        }

        private void NavigateToTicketWithSubscription()
        {
            // Navigation to the "Créditer un ticket avec un abonnement" page.
            ((MainWindow)App.Current.MainWindow).NavigateTo("Views/ViewTicketWithSubscription.xaml");
        }

        private void NavigateToBuySubscription()
        {
            // Navigation to the "Acheter un abonnement" page.
            ((MainWindow)App.Current.MainWindow).NavigateTo("Views/ViewBuySubscription.xaml");
        }
    }
}
