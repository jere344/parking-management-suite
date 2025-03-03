using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using ticketlibrary.Models;
using paymentterminal.Context;

namespace paymentterminal.ViewModels
{
    public class VMSubscriberTicketPayment : ObservableObject
    {
        private readonly BestTicketContext _context;

        // The ticket passed from the previous view.
        public Ticket CurrentTicket { get; set; }

        // Holds the card number entered (or scanned).
        private string _subscriptionCardInput;
        public string SubscriptionCardInput
        {
            get => _subscriptionCardInput;
            set { SubscriptionErrorMessage = string.Empty; SetProperty(ref _subscriptionCardInput, value); }
        }

        // To display any error messages.
        private string _subscriptionErrorMessage;
        public string SubscriptionErrorMessage
        {
            get => _subscriptionErrorMessage;
            set => SetProperty(ref _subscriptionErrorMessage, value);
        }

        // Commands bound to the view.
        public ICommand ScanCardCommand { get; }
        public ICommand ProcessPaymentCommand { get; }
        public ICommand RetourCommand { get; }

        public VMSubscriberTicketPayment()
        {
            _context = new BestTicketContext();

            ScanCardCommand = new RelayCommand(ScanCard);
            ProcessPaymentCommand = new AsyncRelayCommand(ProcessPayment);
            RetourCommand = new RelayCommand(Retour);

            LoadTicketData();
        }

        /// <summary>
        /// Simulate a card scan by filling the input with a placeholder value.
        /// In a real implementation this would interact with a card reader.
        /// </summary>
        private void ScanCard()
        {
            SubscriptionCardInput = "1234-5678";
        }

        /// <summary>
        /// Loads the current ticket from the navigation parameters.
        /// </summary>
        private async void LoadTicketData()
        {
            try
            {
                Ticket? passedTicket = SimpleNavigationService.Instance.Parameter as Ticket;
                SimpleNavigationService.Instance.Parameter = null;
                if (passedTicket == null)
                {
                    MessageBox.Show("Ticket not found.");
                    return;
                }
                CurrentTicket = passedTicket;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Erreur lors du chargement du ticket: {ex.Message}");
            }
        }

        /// <summary>
        /// Process the payment using the subscription card.
        /// Checks for subscription validity and daily usage limits.
        /// </summary>
        private async Task ProcessPayment()
        {
            SubscriptionErrorMessage = string.Empty;

            if (string.IsNullOrWhiteSpace(SubscriptionCardInput))
            {
                SubscriptionErrorMessage = "Veuillez entrer ou scanner votre carte d'abonnement.";
                return;
            }

            // Retrieve subscriptions from the DB and compare the computed CardNumber.
            var subscriptions = await _context.Subscription.ToListAsync();
            var subscription = subscriptions.FirstOrDefault(s =>
                s.CardNumber.Equals(SubscriptionCardInput, StringComparison.OrdinalIgnoreCase));

            if (subscription == null)
            {
                SubscriptionErrorMessage = "Carte d'abonnement invalide.";
                return;
            }

            // Check that the subscription is active.
            var now = DateTime.Now;
            if (subscription.DateStart > now || subscription.DateEnd < now)
            {
                SubscriptionErrorMessage = "Votre abonnement n'est pas valide pour aujourd'hui.";
                return;
            }

            // Check if the card has exceeded its usage for today.
            var today = now.Date;
            var usageCount = await _context.Ticket
                .Include(t => t.TicketPayment)
                .CountAsync(t => t.PaymentTime.HasValue &&
                                 t.PaymentTime.Value.Date == today &&
                                 t.TicketPayment != null &&
                                 t.TicketPayment.SubscriptionId == subscription.Id);

            if (usageCount >= subscription.MaxNumberOfUsesPerDay)
            {
                SubscriptionErrorMessage = "Cette carte a déjà été utilisée le nombre maximal de fois pour aujourd'hui.";
                return;
            }

            // Process the payment by marking the ticket as paid.
            CurrentTicket.PaymentTime = DateTime.Now;
            CurrentTicket.TicketPayment = new TicketPayment
            {
                PaymentAmountTotal = 0, // Assuming no charge when paying by subscription.
                PaymentAmountOfTax = 0,
                PaymentAmountBeforeTax = 0,
                PaymentMethod = "Carte d'abonnement",
                SubscriptionId = subscription.Id
            };

            _context.Ticket.Update(CurrentTicket);
            await _context.SaveChangesAsync();

            // Navigate to the ticket print view.
            ((MainWindow)App.Current.MainWindow).NavigateTo("Views/ViewTicketPrint.xaml", CurrentTicket);
        }

        private void Retour()
        {
            ((MainWindow)App.Current.MainWindow).NavigateTo("Views/ViewInputTicket.xaml", "subscription");
        }
    }
}
