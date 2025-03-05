using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using ticketlibrary.Models;
using admintickets.Context;
using Microsoft.EntityFrameworkCore;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Windows;
using System.Diagnostics;
using Microsoft.Win32;
using QuestPDF.Fluent;

namespace admintickets.ViewModels
{
    public class VMSubscription : ObservableObject
    {
        private BestTicketContext context;
        private ObservableCollection<Subscription> _subscriptions;
        public ObservableCollection<Subscription> Subscriptions
        {
            get => _subscriptions;
            set => SetProperty(ref _subscriptions, value);
        }

        public VMSubscription()
        {
            context = new BestTicketContext();
            Refresh();
        }

        private async Task Refresh()
        {
            // Load subscriptions with related Hospital and TicketPayments details.
            Subscriptions = new ObservableCollection<Subscription>(
                await context.Subscription
                             .Include(s => s.Hospital)
                             .Include(s => s.TicketPayments)
                             .ToListAsync());
            OnPropertyChanged(nameof(Subscriptions));
        }

        // Command to print subscription card as PDF.
        public ICommand PrintSubscriptionCardAsPdfCommand => new AsyncRelayCommand<Subscription>(async (subscription) =>
        {
            if (subscription == null) return;

            SaveFileDialog saveFileDialog = new SaveFileDialog
            {
                Filter = "PDF Files (*.pdf)|*.pdf",
                FileName = $"SubscriptionCard_{subscription.Id}.pdf"
            };

            if (saveFileDialog.ShowDialog() == true)
            {
                string filePath = saveFileDialog.FileName;
                await GenerateSubscriptionCardPdf(subscription, filePath);
                if (MessageBox.Show("Subscription card saved as PDF. Do you want to open it?", 
                    "Success", MessageBoxButton.YesNo, MessageBoxImage.Information) == MessageBoxResult.Yes)
                {
                    Process.Start(new ProcessStartInfo(filePath) { UseShellExecute = true });
                }
            }
        });

        // Command to print subscription card as PNG.
        public ICommand PrintSubscriptionCardAsPngCommand => new AsyncRelayCommand<Subscription>(async (subscription) =>
        {
            if (subscription == null) return;

            SaveFileDialog saveFileDialog = new SaveFileDialog
            {
                Filter = "PNG Files (*.png)|*.png",
                FileName = $"SubscriptionCard_{subscription.Id}.png"
            };

            if (saveFileDialog.ShowDialog() == true)
            {
                string filePath = saveFileDialog.FileName;
                await GenerateSubscriptionCardPng(subscription, filePath);
                if (MessageBox.Show("Subscription card saved as PNG. Do you want to open it?", 
                    "Success", MessageBoxButton.YesNo, MessageBoxImage.Information) == MessageBoxResult.Yes)
                {
                    Process.Start(new ProcessStartInfo(filePath) { UseShellExecute = true });
                }
            }
        });

        // Helper methods for PDF/PNG generation
        private async Task GenerateSubscriptionCardPdf(Subscription subscription, string filePath)
        {
            try
            {
                var subscriptionDocument = new ticket_library.Documents.SubscriptionCardDocument(subscription);
                subscriptionDocument.GeneratePdf(filePath);
            }
            catch (System.Exception ex)
            {
                MessageBox.Show($"Error generating PDF: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            await Task.CompletedTask;
        }

        private async Task GenerateSubscriptionCardPng(Subscription subscription, string filePath)
        {
            try
            {
                var subscriptionDocument = new ticket_library.Documents.SubscriptionCardDocument(subscription);
                subscriptionDocument.GenerateImages(imageIndex => imageIndex == 0 
                    ? filePath 
                    : filePath.Replace(".png", $"_{imageIndex}.png"));
            }
            catch (System.Exception ex)
            {
                MessageBox.Show($"Error generating PNG: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            await Task.CompletedTask;
        }

        // Command to manually refresh the subscriptions list.
        public ICommand RefreshCommand => new AsyncRelayCommand(async () => await Refresh());
    }
}
