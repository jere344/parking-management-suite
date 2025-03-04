using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using ticketlibrary.Models;
using admintickets.Context;
using Microsoft.EntityFrameworkCore;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Linq;
using admintickets.Views;
using System.IO;
using Microsoft.Win32;
using System.Windows.Media.Imaging;
using System.Windows;
using System.Windows.Media;
using QuestPDF.Fluent;


namespace admintickets.ViewModels
{
    public class VMTickets : ObservableObject
    {
        private BestTicketContext context;

        private ObservableCollection<Ticket> _tickets;
        public ObservableCollection<Ticket> Tickets
        {
            get => _tickets;
            set => SetProperty(ref _tickets, value);
        }

        public VMTickets()
        {
            context = new BestTicketContext();
            Refresh();
        }

        private async Task Refresh()
        {
            Tickets = new ObservableCollection<Ticket>(
                await context.Ticket
                             .Include(t => t.Hospital)
                             .Include(t => t.TicketPayment)
                             .ToListAsync());
            OnPropertyChanged(nameof(Tickets));
        }

        public ICommand ViewPaymentCommand => new RelayCommand<Ticket>((ticket) =>
        {
            if (ticket?.TicketPayment != null)
            {
                // Open a floating window to display read-only ticket payment details.
                TicketPaymentDetailsWindow detailsWindow = new TicketPaymentDetailsWindow(ticket.TicketPayment);
                detailsWindow.ShowDialog();
            }
        });

        public ICommand PrintTicketAsPdfCommand => new AsyncRelayCommand<Ticket>(async (ticket) =>
        {
            if (ticket == null) return;

            SaveFileDialog saveFileDialog = new SaveFileDialog
            {
                Filter = "PDF Files (*.pdf)|*.pdf",
                FileName = $"Ticket_{ticket.Id}.pdf"
            };

            if (saveFileDialog.ShowDialog() == true)
            {
                string filePath = saveFileDialog.FileName;
                await GenerateTicketPdf(ticket, filePath);
                if (MessageBox.Show("Ticket saved as PDF. Do you want to open it?", "Success", MessageBoxButton.YesNo, MessageBoxImage.Information) == MessageBoxResult.Yes)
                {
                    // System.Diagnostics.Process.Start(filePath);
                    // The specified executable is not a valid application for this OS platform.'
                    // so we need to do that :
                    System.Diagnostics.Process.Start(new System.Diagnostics.ProcessStartInfo(filePath) { UseShellExecute = true });
                }
            }
        });

        public ICommand PrintTicketAsPngCommand => new AsyncRelayCommand<Ticket>(async (ticket) =>
        {
            if (ticket == null) return;

            SaveFileDialog saveFileDialog = new SaveFileDialog
            {
                Filter = "PNG Files (*.png)|*.png",
                FileName = $"Ticket_{ticket.Id}.png"
            };

            if (saveFileDialog.ShowDialog() == true)
            {
                string filePath = saveFileDialog.FileName;
                await GenerateTicketPng(ticket, filePath);
                if (MessageBox.Show("Ticket saved as PNG. Do you want to open it?", "Success", MessageBoxButton.YesNo, MessageBoxImage.Information) == MessageBoxResult.Yes)
                {
                    // System.Diagnostics.Process.Start(filePath);
                    // The specified executable is not a valid application for this OS platform.'
                    // so we need to do that :
                    System.Diagnostics.Process.Start(new System.Diagnostics.ProcessStartInfo(filePath) { UseShellExecute = true });
                }
            }
        });

        public ICommand SetTicketAsPaidCommand => new AsyncRelayCommand<Ticket>(async (ticket) =>
        {
            if (ticket == null || ticket.PaymentTime != null) return;

            ticket.PaymentTime = DateTime.Now;
            ticket.TicketPayment = new TicketPayment
            {
                PaymentAmountTotal = 0,  // Placeholder amount
                PaymentAmountOfTax = 0,
                PaymentAmountBeforeTax = 0,
                PaymentMethod = "admin-validated",
                SubscriptionId = null,
                CodeUsedId = null
            };
            
            await context.SaveChangesAsync();
            OnPropertyChanged(nameof(Tickets));

            await Refresh();
            MessageBox.Show("Ticket set as paid.", "Success", MessageBoxButton.OK, MessageBoxImage.Information);
        });

        public ICommand SetTicketAsUnpaidCommand => new AsyncRelayCommand<Ticket>(async (ticket) =>
        {
            if (ticket == null || ticket.PaymentTime == null) return;

            ticket.PaymentTime = null;
            ticket.TicketPayment = null;
            
            await context.SaveChangesAsync();
            OnPropertyChanged(nameof(Tickets));

            await Refresh();
            MessageBox.Show("Ticket set as unpaid.", "Success", MessageBoxButton.OK, MessageBoxImage.Information);
        });

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

        private async Task GenerateTicketPng(Ticket ticket, string filePath)
        {
            try {
                var ticketDocument = new ticket_library.Documents.TicketDocument(ticket);
                // document.GenerateImages(imageIndex => $"image{imageIndex}.png");
                ticketDocument.GenerateImages(imageIndex => imageIndex == 0 ? filePath : filePath.Replace(".png", $"_{imageIndex}.png"));
            }
            catch (System.Exception ex)
            {
                MessageBox.Show($"Error generating PNG: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }

            await Task.CompletedTask;
        }
    }
}
