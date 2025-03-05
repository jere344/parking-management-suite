using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using ticketlibrary.Models;
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
    public class VMPriceBrackets : ObservableObject
    {
        private BestTicketContext context;

        private ObservableCollection<PriceBracket> _priceBrackets;
        public ObservableCollection<PriceBracket> PriceBrackets
        {
            get => _priceBrackets;
            set => SetProperty(ref _priceBrackets, value);
        }

        public ObservableCollection<Hospital> Hospitals { get; set; }

        // Fields for the new price bracket
        private string _newBracketMinDuration;
        public string NewBracketMinDuration
        {
            get => _newBracketMinDuration;
            set => SetProperty(ref _newBracketMinDuration, value);
        }

        private decimal _newBracketPrice;
        public decimal NewBracketPrice
        {
            get => _newBracketPrice;
            set => SetProperty(ref _newBracketPrice, value);
        }

        private Hospital? _newBracketHospital;
        public Hospital? NewBracketHospital
        {
            get => _newBracketHospital;
            set => SetProperty(ref _newBracketHospital, value);
        }

        public VMPriceBrackets()
        {
            context = new BestTicketContext();
            Hospitals = new ObservableCollection<Hospital>(context.Hospital.ToList());
            Hospitals.Insert(0, new Hospital { Name = "Global", Id = -1 });
            Refresh();
        }

        private async Task Refresh()
        {
            PriceBrackets = new ObservableCollection<PriceBracket>(await context.PriceBracket.ToListAsync());
            OnPropertyChanged(nameof(PriceBrackets));
        }

        public ICommand AddBracketCommand => new AsyncRelayCommand(async () =>
        {
            if (string.IsNullOrEmpty(NewBracketMinDuration) || NewBracketPrice == 0)
            {
                MessageBox.Show("Please fill in all fields.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            if (!TimeSpan.TryParse(NewBracketMinDuration, out TimeSpan minDuration))
            {
                MessageBox.Show("Invalid duration format. Please enter a valid TimeSpan (e.g., '00:30:00').", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            var newBracket = new PriceBracket
            {
                // Using the property setter so that InternalMinDuration is set correctly
                MinDuration = minDuration,
                Price = NewBracketPrice,
                HospitalId = NewBracketHospital?.Id == -1 ? null : NewBracketHospital?.Id
            };

            context.PriceBracket.Add(newBracket);
            await context.SaveChangesAsync();
            await Refresh();
        });

        public ICommand DeleteBracketCommand => new AsyncRelayCommand<PriceBracket>(async (bracket) =>
        {
            context.PriceBracket.Remove(bracket);
            await context.SaveChangesAsync();
            await Refresh();
        });

        public ICommand CopyBracketCommand => new RelayCommand<PriceBracket>((bracket) =>
        {
            if (bracket != null)
            {
                NewBracketMinDuration = bracket.MinDuration.ToString();
                NewBracketPrice = bracket.Price;
                NewBracketHospital = Hospitals.FirstOrDefault(h => h.Id == bracket.HospitalId) ?? Hospitals.FirstOrDefault(h => h.Id == -1);
            }
        });
    }
}
