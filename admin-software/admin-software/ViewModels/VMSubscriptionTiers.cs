using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using admintickets.Models.DBModels;
using admintickets.Context;
using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using Microsoft.EntityFrameworkCore;

namespace admintickets.ViewModels
{
    public class VMSubscriptionTiers : ObservableObject
    {
        private BestTicketContext context;

        private ObservableCollection<SubscriptionTiers> _subscriptionTiers;
        public ObservableCollection<SubscriptionTiers> SubscriptionTiers
        {
            get => _subscriptionTiers;
            set => SetProperty(ref _subscriptionTiers, value);
        }

        public ObservableCollection<Hospital> Hospitals { get; set; }

        // Fields for the new subscription tier
        private string _newTierName;
        public string NewTierName
        {
            get => _newTierName;
            set => SetProperty(ref _newTierName, value);
        }

        private int _newTierDurationInDays;
        public int NewTierDurationInDays
        {
            get => _newTierDurationInDays;
            set => SetProperty(ref _newTierDurationInDays, value);
        }

        private int _newTierMaxUsesPerDay;
        public int NewTierMaxUsesPerDay
        {
            get => _newTierMaxUsesPerDay;
            set => SetProperty(ref _newTierMaxUsesPerDay, value);
        }

        private decimal _newTierPrice;
        public decimal NewTierPrice
        {
            get => _newTierPrice;
            set => SetProperty(ref _newTierPrice, value);
        }

        private Hospital? _newTierHospital;
        public Hospital? NewTierHospital
        {
            get => _newTierHospital;
            set => SetProperty(ref _newTierHospital, value);
        }

        public VMSubscriptionTiers()
        {
            context = new BestTicketContext();
            Hospitals = new ObservableCollection<Hospital>(context.Hospital.ToList());
            Hospitals.Insert(0, new Hospital { Name = "Global", Id = -1 });

            Refresh();
        }

        private async Task Refresh()
        {
            SubscriptionTiers = new ObservableCollection<SubscriptionTiers>(await context.SubscriptionTier.ToListAsync());
            OnPropertyChanged(nameof(SubscriptionTiers));
        }

        public ICommand AddTierCommand => new AsyncRelayCommand(async () =>
        {
            if (string.IsNullOrEmpty(NewTierName) ||
                NewTierDurationInDays == 0 ||
                NewTierMaxUsesPerDay == 0 ||
                NewTierPrice == 0)
            {
                MessageBox.Show("Please fill in all fields.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            var newTier = new SubscriptionTiers
            {
                Name = NewTierName,
                Duration = TimeSpan.FromDays(NewTierDurationInDays),
                MaxNumberOfUsesPerDay = NewTierMaxUsesPerDay,
                Price = NewTierPrice,
                HospitalId = NewTierHospital?.Id == -1 ? null : NewTierHospital?.Id
            };

            context.SubscriptionTier.Add(newTier);
            await context.SaveChangesAsync();
            await Refresh();
        });

        public ICommand DeleteTierCommand => new AsyncRelayCommand<SubscriptionTiers>(async (tier) =>
        {
            context.SubscriptionTier.Remove(tier);
            await context.SaveChangesAsync();
            await Refresh();
        });

        public ICommand CopyTierCommand => new RelayCommand<SubscriptionTiers>((tier) =>
        {
            if (tier != null)
            {
                NewTierName = tier.Name;
                NewTierDurationInDays = (int)tier.Duration.TotalDays;
                NewTierMaxUsesPerDay = tier.MaxNumberOfUsesPerDay;
                NewTierPrice = Math.Round(tier.Price, 2);
                NewTierHospital = Hospitals.FirstOrDefault(h => h.Id == tier.HospitalId) ?? Hospitals.FirstOrDefault(h => h.Id == -1);
            }
        });
    }
}
