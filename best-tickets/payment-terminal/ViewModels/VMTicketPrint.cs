using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Microsoft.Win32;
using System;
using System.Diagnostics;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using ticketlibrary.Models;
using QuestPDF.Fluent;


namespace paymentterminal.ViewModels
{
    public class VMTicketPrint : ObservableObject
    {
        public Ticket CurrentTicket { get; set; }

        private bool _hasPrinted = false;
        public bool HasPrinted
        {
            get => _hasPrinted;
            set {
                SetProperty(ref _hasPrinted, value);
                (FinishCommand as RelayCommand)?.NotifyCanExecuteChanged();
            }
        }

        public IAsyncRelayCommand PrintTicketCommand { get; }
        public ICommand FinishCommand { get; }

        public VMTicketPrint()
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


            PrintTicketCommand = new AsyncRelayCommand(PrintTicketAsPdf);            
            FinishCommand = new RelayCommand(Finish, () => HasPrinted);
        }

        private async Task PrintTicketAsPdf()
        {
            if (CurrentTicket == null) return;

            SaveFileDialog saveFileDialog = new SaveFileDialog
            {
                Filter = "PDF Files (*.pdf)|*.pdf",
                FileName = $"Ticket_{CurrentTicket.TicketNumber}.pdf"
            };

            if (saveFileDialog.ShowDialog() == true)
            {
                string filePath = saveFileDialog.FileName;
                await GenerateTicketPdf(CurrentTicket, filePath);
                if (MessageBox.Show("Ticket saved as PDF. Do you want to open it?", "Success", MessageBoxButton.YesNo, MessageBoxImage.Information) == MessageBoxResult.Yes)
                {
                    Process.Start(new ProcessStartInfo(filePath) { UseShellExecute = true });
                }
                HasPrinted = true;
            }
        }

        // Helper methods for PDF/PNG generation
        private async Task GenerateTicketPdf(Ticket ticket, string filePath)
        {
            try {
                var ticketDocument = new ticket_library.Documents.TicketDocument(ticket);
                ticketDocument.GeneratePdf(filePath);
            }
            catch (System.Exception ex)
            {
                MessageBox.Show($"Error generating PDF: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }

            await Task.CompletedTask;
        }

        private void Finish()
        {
            // Navigate back to the Home page.
            ((MainWindow)App.Current.MainWindow).NavigateTo("Views/ViewHome.xaml");
        }
    }
}
