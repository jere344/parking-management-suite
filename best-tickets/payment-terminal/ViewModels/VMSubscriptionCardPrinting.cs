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
    public class VMSubscriptionCardPrinting : ObservableObject
    {
        // Assuming a SubscriptionCard model exists in ticketlibrary.Models.
        public Subscription CurrentSubscription { get; set; }

        private bool _hasPrinted = false;
        public bool HasPrinted
        {
            get => _hasPrinted;
            set 
            { 
                SetProperty(ref _hasPrinted, value);
                // Notify the command that the CanExecute condition may have changed
                (FinishCommand as RelayCommand)?.NotifyCanExecuteChanged();
            }
        }

        public IAsyncRelayCommand PrintSubscriptionCardCommand { get; }
        public ICommand FinishCommand { get; }

        public VMSubscriptionCardPrinting()
        {
            // Fetch the subscription card from navigation parameters.
            Subscription? passedCard = SimpleNavigationService.Instance.Parameter as Subscription;
            SimpleNavigationService.Instance.Parameter = null;
            if (passedCard == null)
            {
                MessageBox.Show("Subscription card not found.");
                return;
            }
            CurrentSubscription = passedCard;

            PrintSubscriptionCardCommand = new AsyncRelayCommand(PrintSubscriptionCardAsPdf);
            // Use RelayCommand that supports NotifyCanExecuteChanged
            FinishCommand = new RelayCommand(Finish, () => HasPrinted);
        }

        private async Task PrintSubscriptionCardAsPdf()
        {
            if (CurrentSubscription == null) return;

            SaveFileDialog saveFileDialog = new SaveFileDialog
            {
                Filter = "PDF Files (*.pdf)|*.pdf",
                FileName = $"SubscriptionCard_{CurrentSubscription.CardNumber}.pdf" // Assuming a CardNumber property.
            };

            if (saveFileDialog.ShowDialog() == true)
            {
                string filePath = saveFileDialog.FileName;
                await GenerateSubscriptionCardPdf(CurrentSubscription, filePath);
                if (MessageBox.Show("Subscription card saved as PDF. Do you want to open it?", "Success", MessageBoxButton.YesNo, MessageBoxImage.Information) == MessageBoxResult.Yes)
                {
                    Process.Start(new ProcessStartInfo(filePath) { UseShellExecute = true });
                }
            }
            HasPrinted = true;
        }

        // Helper method for PDF generation.
        private async Task GenerateSubscriptionCardPdf(Subscription card, string filePath)
        {
            try
            {
                // Assumes you have a document class for subscription cards.
                var cardDocument = new ticket_library.Documents.SubscriptionCardDocument(card);
                cardDocument.GeneratePdf(filePath);
            }
            catch (Exception ex)
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
