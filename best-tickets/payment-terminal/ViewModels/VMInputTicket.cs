using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using System.Windows.Input;
using ticketlibrary.Models;
using paymentterminal.Context;
using System.Windows;

namespace paymentterminal.ViewModels
{
    public class VMInputTicket : ObservableObject
    {
        private readonly BestTicketContext _context;

        private string _ticketNumber;
        public string TicketNumber
        {
            get => _ticketNumber;
            set
            {
                // ticket format is xxx-xxx-xxx
                // while the user is typing, add dashes to the ticket number

                value = value.ToUpper();
                if (value.Length > 11)
                {
                    value = value[..11];
                    return;
                }
                

                ErrorMessage = null;
                
                // if it's a backspace
                if (value?.Length < _ticketNumber?.Length)
                {
                    // if we are backspacing over a dash, remove the dash and the number
                    if (value.Length == 3 || value.Length == 7)
                    {
                        _ticketNumber = value[..^1];
                    }
                    else
                    {
                        _ticketNumber = value;
                    }
                }
                else // if it's a new character
                {
                    // if we are at a position where a dash should be, add the dash and the number
                    if (value.Length == 3 || value.Length == 7)
                    {
                        _ticketNumber = value + "-";
                    }
                    else
                    {
                        _ticketNumber = value;
                    }
                }
                
                OnPropertyChanged(nameof(TicketNumber));

                if (TicketNumber.Length == 11)
                {
                    ValidateAndProceed();
                }
            }
        }

        private string _errorMessage;
        public string ErrorMessage
        {
            get => _errorMessage;
            set => SetProperty(ref _errorMessage, value);
        }

        private bool _canProceed;
        public bool CanProceed
        {
            get => _canProceed;
            set => SetProperty(ref _canProceed, value);
        }

        public ICommand ScanCommand { get; }
        public ICommand NextCommand { get; }
        public ICommand RetourCommand { get; }

        public string? NextStep { get; set; }

        public VMInputTicket()
        {
            _context = new BestTicketContext();

            ScanCommand = new RelayCommand(ScanTicket);
            NextCommand = new AsyncRelayCommand(ValidateAndProceed);
            RetourCommand = new RelayCommand(Retour);

            TicketNumber = string.Empty;
            ErrorMessage = string.Empty;
            CanProceed = false;



            NextStep = SimpleNavigationService.Instance.Parameter as string;
            SimpleNavigationService.Instance.Parameter = null;
            if (NextStep != "unique" && NextStep != "subscription")
            {
                MessageBox.Show("Invalid navigation parameter.");
                ((MainWindow)App.Current.MainWindow).NavigateTo("Views/ViewHome.xaml");
            }
        }

        private void ScanTicket()
        {
            TicketNumber = "39A-14D-870";
        }

        private async Task ValidateAndProceed()
        {
            ErrorMessage = string.Empty;
            CanProceed = false;

            if (string.IsNullOrWhiteSpace(TicketNumber))
            {
                ErrorMessage = "Veuillez entrer un numéro de ticket.";
                return;
            }

            // Query the database for the ticket.
            var ticket = await _context.Ticket.FirstOrDefaultAsync(t => t.TicketNumber == TicketNumber);
            if (ticket == null)
            {
                ErrorMessage = "Ticket non trouvé.";
                return;
            }

            // Check if the ticket has already been paid.
            if (ticket.PaymentTime.HasValue || ticket.TicketPaymentId.HasValue)
            {
                ErrorMessage = "Ce ticket a déjà été payé.";
                return;
            }

            // The ticket is valid.
            CanProceed = true;

            // Navigate to the next page 
            if (NextStep == "unique")
            {
                ((MainWindow)App.Current.MainWindow).NavigateTo("Views/ViewSingleTicketPayment.xaml", ticket);
            }
            else if (NextStep == "subscription")
            {
                ((MainWindow)App.Current.MainWindow).NavigateTo("Views/ViewSubscriberTicketPayment.xaml", ticket);
            }
        }

        private void Retour()
        {
            ((MainWindow)App.Current.MainWindow).NavigateTo("Views/ViewHome.xaml");
        }
    }
}
