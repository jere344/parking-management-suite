using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.ObjectModel;
using System.IO;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using ticketlibrary.Models;
using paymentterminal.Context;
using System.Linq;

namespace paymentterminal.ViewModels
{
    public class VMSingleTicketPayment : ObservableObject
    {
    private readonly BestTicketContext _context;
        
        // The ticket fetched from the DB (assumed to be identified from a global property)
        public Ticket CurrentTicket { get; set; }

        public TimeSpan? StationmentTime => DateTime.Now - CurrentTicket.CreationTime;


        
        public ObservableCollection<Taxes> TaxesList { get; set; } = new ObservableCollection<Taxes>();

        // Reduction code input and error message
        private string _reductionCodeInput;
        public string ReductionCodeInput
        {
            get => _reductionCodeInput;
            set {
                ReductionErrorMessage = string.Empty;
                SetProperty(ref _reductionCodeInput, value);
            }
        }

        private Code? _codeUsed = null;
        public Code? CodeUsed
        {
            get => _codeUsed;
            set {
                _codeUsed = value;
                OnPropertyChanged();
                OnPropertyChanged(nameof(PaymentAmountAfterCode));
                OnPropertyChanged(nameof(PaymentAmountAfterTaxes));
                OnPropertyChanged(nameof(CodeAmountSaved));
                OnPropertyChanged(nameof(ReductionMessage));
                OnPropertyChanged(nameof(TotalTaxesAmount));
            }
        }

        private string _reductionErrorMessage;
        public string ReductionErrorMessage
        {
            get => _reductionErrorMessage;
            set => SetProperty(ref _reductionErrorMessage, value);
        }

        // price
        // CodeUsed?.Reduction / 100
        public decimal PaymentAmountAfterCode => PaymentOriginalAmount * (1 - (CodeUsed?.Reduction ?? 0) / 100);
        public decimal PaymentAmountAfterTaxes => PaymentAmountAfterCode * (1 + TaxesList.Sum(t => t.Amount));
        public decimal CodeAmountSaved => PaymentOriginalAmount - PaymentAmountAfterCode;
        public string ReductionMessage => CodeUsed == null ? "" : "- " + CodeAmountSaved.ToString("C") + " grâce au code de réduction " + CodeUsed.Name;
        public decimal TotalTaxesAmount => PaymentAmountAfterCode * TaxesList.Sum(t => t.Amount);

        private decimal _paymentOriginalAmount { get; set; }
        public decimal PaymentOriginalAmount
        {
            get => _paymentOriginalAmount;
            set {
                _paymentOriginalAmount = value;
                OnPropertyChanged();
                OnPropertyChanged(nameof(PaymentAmountAfterCode));
                OnPropertyChanged(nameof(PaymentAmountAfterTaxes));
                OnPropertyChanged(nameof(CodeAmountSaved));
                OnPropertyChanged(nameof(ReductionMessage));
                OnPropertyChanged(nameof(TotalTaxesAmount));
            }
        }
        
        // Add property to calculate total taxes amount


        // Commands
        public ICommand ApplyReductionCommand { get; }
        public ICommand ProceedCommand { get; }
        public ICommand RetourCommand { get; }

        public VMSingleTicketPayment()
        {
            // TODO: Adjust context creation as needed (e.g. dependency injection)
            _context = new BestTicketContext();

            ApplyReductionCommand = new AsyncRelayCommand(ApplyReductionCode);
            ProceedCommand = new RelayCommand(ProceedToPrint);
            RetourCommand = new RelayCommand(Retour);

            // Load the ticket and related information asynchronously.
            LoadTicketData();
        }

        private async void LoadTicketData()
        {
            try
            {
                // Fetch ticket with related hospital and payment data.
                Ticket? Passedticket = SimpleNavigationService.Instance.Parameter as Ticket;
                SimpleNavigationService.Instance.Parameter = null;
                if (Passedticket == null)
                {
                    MessageBox.Show("Ticket not found.");
                    return;
                }
                CurrentTicket = Passedticket;
                OnPropertyChanged(nameof(StationmentTime));

                var taxes = await _context.Taxe.ToListAsync();
                TaxesList = new ObservableCollection<Taxes>(taxes);
                OnPropertyChanged(nameof(TaxesList));

                // Load hospital brackets. If there are no hospital-specific brackets, use the global ones.
                var relevantBrackets = CurrentTicket.Hospital.PriceBrackets;
                if (relevantBrackets.Count == 0)
                {
                    relevantBrackets = await _context.PriceBracket.Where(pb => pb.HospitalId == null).ToListAsync();
                }

                // Get the first bracket where Time MinDuration is less than the stationment time
                var bracket = relevantBrackets.OrderByDescending(pb => pb.MinDuration).FirstOrDefault(pb => pb.MinDuration <= StationmentTime);
                PaymentOriginalAmount = bracket?.Price ?? 0;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Erreur lors du chargement des données : {ex.Message}");
            }
        }

        private async Task ApplyReductionCode()
        {
            ReductionErrorMessage = string.Empty;

            if (string.IsNullOrWhiteSpace(ReductionCodeInput))
            {
                return;
            }

            // Validate the reduction code from the database.
            var code = await _context.DiscountCode.FirstOrDefaultAsync(c =>
                c.Name == ReductionCodeInput &&
                c.IsActive &&
                (c.HospitalId == null || c.HospitalId == CurrentTicket.HospitalId));

            if (code == null)
            {
                ReductionErrorMessage = "Code de réduction invalide.";
                return;
            }

            CodeUsed = code;
        }

        private void ProceedToPrint()
        {
            CurrentTicket.PaymentTime = DateTime.Now;
            CurrentTicket.TicketPayment = new TicketPayment
            {
                PaymentAmountTotal = PaymentAmountAfterTaxes,
                PaymentAmountOfTax = TotalTaxesAmount,
                PaymentAmountBeforeTax = PaymentAmountAfterCode,
                PaymentMethod = "Carte bancaire",
                CodeUsedId = CodeUsed?.Id,
                CodeUsedReduction = CodeUsed?.Reduction,
            };

            _context.Ticket.Update(CurrentTicket);
            _context.SaveChanges();

            ((MainWindow)App.Current.MainWindow).NavigateTo("Views/ViewTicketPrint.xaml", CurrentTicket);
        }

        private void Retour()
        {
            ((MainWindow)App.Current.MainWindow).NavigateTo("Views/ViewInputTicket.xaml", "unique");
        }
    }
}
