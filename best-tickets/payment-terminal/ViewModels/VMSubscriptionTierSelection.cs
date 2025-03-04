using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Microsoft.EntityFrameworkCore;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using System.Windows.Input;
using ticketlibrary.Models;
using paymentterminal.Context;

namespace paymentterminal.ViewModels
{
    public class VMSubscriptionTierSelection : ObservableObject
    {
        private readonly BestTicketContext _context;

        // The selected hospital passed from the previous page
        public Hospital SelectedHospital { get; set; }

        // List of subscription tiers to display.
        public ObservableCollection<SubscriptionTier> SubscriptionTiers { get; set; }

        public ICommand SelectTierCommand { get; }
        public ICommand RetourCommand { get; }

        public VMSubscriptionTierSelection()
        {
            _context = new BestTicketContext();

            // Retrieve the hospital passed as navigation parameter
            SelectedHospital = SimpleNavigationService.Instance.Parameter as Hospital;
            SimpleNavigationService.Instance.Parameter = null;

            if (SelectedHospital == null)
            {
                MessageBox.Show("Erreur de navigation.");
                ((MainWindow)App.Current.MainWindow).NavigateTo("Views/ViewHome.xaml");
                return;
            }

            // If the hospital has no specific tiers, the globals ones applies.
            var tiers = SelectedHospital.SubscriptionTiers;
            if (tiers == null || tiers.Count == 0) {
                tiers = _context.SubscriptionTier
                    .Where(t => t.HospitalId == null)
                    .ToList();
            }
            SubscriptionTiers = new ObservableCollection<SubscriptionTier>(tiers.OrderBy(t => t.Price));

            SelectTierCommand = new RelayCommand<SubscriptionTier>(SelectTier);
            RetourCommand = new RelayCommand(Retour);
        }

        private void SelectTier(SubscriptionTier selectedTier)
        {
            if (MessageBox.Show($"Confirmez-vous l'abonnement {selectedTier.Name} pour {SelectedHospital.Name} ?", "Confirmation", MessageBoxButton.YesNo) == MessageBoxResult.No)
            {
                return;
            }

            byte[] hashBytes = System.Security.Cryptography.SHA256.Create().ComputeHash(System.Text.Encoding.UTF8.GetBytes(DateTime.Now.ToString() + SelectedHospital.Id + selectedTier.Id));
            string fullhash = BitConverter.ToString(hashBytes).Replace("-", "");
            string cardNumber = $"{fullhash.Substring(0, 4)}-{fullhash.Substring(4, 4)}".ToUpper();

            var subscription = new Subscription
            {
                DateStart = System.DateTime.Now,
                DateEnd = System.DateTime.Now + selectedTier.Duration,
                MaxNumberOfUsesPerDay = selectedTier.MaxNumberOfUsesPerDay,
                HospitalId = SelectedHospital.Id,
                CardNumber = cardNumber  
            };

            _context.Subscription.Add(subscription);
            _context.SaveChanges();

            ((MainWindow)App.Current.MainWindow).NavigateTo("Views/ViewSubscriptionCardPrinting.xaml", subscription);
        }

        private void Retour()
        {
            ((MainWindow)App.Current.MainWindow).NavigateTo("Views/ViewSelectHospitalSubscription.xaml");
        }
    }
}
