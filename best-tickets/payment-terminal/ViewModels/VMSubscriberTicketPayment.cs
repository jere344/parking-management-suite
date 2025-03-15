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

        public TimeSpan StationmentTime => DateTime.Now - CurrentTicket.CreationTime;

        private Ticket _currentTicket;
        public Ticket CurrentTicket 
        {
            get => _currentTicket;
            set {
                SetProperty(ref _currentTicket, value);
                OnPropertyChanged(nameof(StationmentTime));
            }
        }

        private Subscription? _currentSubscription = null;
        public Subscription? CurrentSubscription
        {
            get => _currentSubscription;
            set => SetProperty(ref _currentSubscription, value);
        }

        private int? _subscriptionUsageCount = null;
        public int? SubscriptionUsageCount
        {
            get => _subscriptionUsageCount;
            set {
                SetProperty(ref _subscriptionUsageCount, value);
                OnPropertyChanged(nameof(SubscriptionUsageMessage));
            }
        }
        public string SubscriptionUsageMessage => (SubscriptionUsageCount != null && CurrentSubscription != null) ? $"Nombre d'utilisations aujourd'hui: {SubscriptionUsageCount}/{CurrentSubscription.MaxNumberOfUsesPerDay}" : string.Empty;

        // Holds the card number entered (or scanned).
        private string _subscriptionCardInput;
        public string SubscriptionCardInput
        {
            get => _subscriptionCardInput;
            set
            {
                // subscription format is xxxx-xxxx
                // while the user is typing, add dashes to the ticket number
                // See ticket number for logic

                value = value.ToUpper();
                if (value.Length > 9)
                {
                    value = value[..9];
                    return;
                }

                SubscriptionErrorMessage = string.Empty;
                
                if (value?.Length < _subscriptionCardInput?.Length)
                {
                    if (value.Length == 4)
                    {
                        _subscriptionCardInput = value[..^1];
                    }
                    else
                    {
                        _subscriptionCardInput = value;
                    }
                }
                else
                {
                    if (value.Length == 4)
                    {
                        _subscriptionCardInput = value + "-";
                    }
                    else
                    {
                        _subscriptionCardInput = value;
                    }
                }
                
                OnPropertyChanged(nameof(SubscriptionCardInput));

                if (_subscriptionCardInput.Length == 9)
                {
                    _ = ValidateCard();
                }
            }
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

        private async Task ValidateCard()
        {
            if (string.IsNullOrWhiteSpace(SubscriptionCardInput))
            {
                SubscriptionErrorMessage = "Veuillez entrer ou scanner votre carte d'abonnement.";
                return;
            }

            if (SubscriptionCardInput.Length != 9)
            {
                SubscriptionErrorMessage = "Le numéro de carte d'abonnement doit comporter 9 caractères au format xxxx-xxxx.";
                return;
            }

            var subscription = await _context.Subscription
                .FirstOrDefaultAsync(s => s.CardNumber == SubscriptionCardInput);

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
                                 t.TicketPayment != null &&
                                 t.TicketPayment.SubscriptionId == subscription.Id &&
                                 t.PaymentTime.Value.Date == today);

            if (usageCount >= subscription.MaxNumberOfUsesPerDay)
            {
                SubscriptionErrorMessage = "Cette carte a déjà été utilisée le nombre maximal de fois pour aujourd'hui.";
                return;
            }
            CurrentSubscription = subscription;
            SubscriptionUsageCount = usageCount;
        }

        /// <summary>
        /// Process the payment using the subscription card.
        /// Checks for subscription validity and daily usage limits.
        /// </summary>
        private async Task ProcessPayment()
        {
            if (CurrentSubscription == null)
            {
                return;
            }

            // Process the payment by marking the ticket as paid.
            CurrentTicket.PaymentTime = DateTime.Now;
            CurrentTicket.TicketPayment = new TicketPayment
            {
                PaymentAmountTotal = 0, // no charge when paying by subscription.
                PaymentAmountOfTax = 0,
                PaymentAmountBeforeTax = 0,
                PaymentMethod = CurrentSubscription.CardNumber,
                SubscriptionId = CurrentSubscription.Id
            };

            _context.Ticket.Update(CurrentTicket);
            await _context.SaveChangesAsync();

            ((MainWindow)App.Current.MainWindow).NavigateTo("Views/ViewTicketPrint.xaml", CurrentTicket);
        }

        private void Retour()
        {
            ((MainWindow)App.Current.MainWindow).NavigateTo("Views/ViewInputTicket.xaml", "subscription");
        }
    }
}
