using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System.Windows.Input;

namespace paymentterminal.ViewModels
{
    public class VMHome : ObservableObject
    {
        public ICommand NavigateToInputTicketCommand { get; }
        public ICommand NavigateToTicketWithSubscriptionCommand { get; }
        public ICommand NavigateToBuySubscriptionCommand { get; }

        public VMHome()
        {
            NavigateToInputTicketCommand = new RelayCommand(NavigateToInputTicket);
            NavigateToTicketWithSubscriptionCommand = new RelayCommand(NavigateToTicketWithSubscription);
            NavigateToBuySubscriptionCommand = new RelayCommand(NavigateToBuySubscription);
        }

        private void NavigateToInputTicket()
        {
            // Navigation to the "Créditer un ticket unique" page.
            ((MainWindow)App.Current.MainWindow).NavigateTo("Views/ViewInputTicket.xaml", "unique");
        }

        private void NavigateToTicketWithSubscription()
        {
            // Navigation to the "Créditer un ticket avec un abonnement" page.
            ((MainWindow)App.Current.MainWindow).NavigateTo("Views/ViewInputTicket.xaml", "subscription");
        }

        private void NavigateToBuySubscription()
        {
            // Navigation to the "Acheter un abonnement" page.
            ((MainWindow)App.Current.MainWindow).NavigateTo("Views/ViewBuySubscription.xaml");
        }
    }
}
