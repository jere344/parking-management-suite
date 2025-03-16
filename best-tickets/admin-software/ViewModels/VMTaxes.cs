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
    public class VMTaxes : ObservableObject
    {
        private BestTicketContext context;

        private ObservableCollection<Taxes> _taxes;
        public ObservableCollection<Taxes> Taxes
        {
            get => _taxes;
            set => SetProperty(ref _taxes, value);
        }

        // Fields for the new tax
        private string _newTaxName;
        public string NewTaxName
        {
            get => _newTaxName;
            set => SetProperty(ref _newTaxName, value);
        }

        private decimal _newTaxAmount;
        public decimal NewTaxAmount
        {
            get => _newTaxAmount;
            set => SetProperty(ref _newTaxAmount, value);
        }

        private string _newTaxDescription;
        public string NewTaxDescription
        {
            get => _newTaxDescription;
            set => SetProperty(ref _newTaxDescription, value);
        }

        public VMTaxes()
        {
            context = new BestTicketContext();
            Refresh();
        }

        private async Task Refresh()
        {
            Taxes = new ObservableCollection<Taxes>(await context.Taxe.ToListAsync());
            OnPropertyChanged(nameof(Taxes));
        }

        public ICommand AddTaxCommand => new AsyncRelayCommand(async () =>
        {
            if (string.IsNullOrEmpty(NewTaxName) || NewTaxAmount <= 0)
            {
                MessageBox.Show("Please enter a name and a valid amount.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            var newTax = new Taxes
            {
                Name = NewTaxName,
                Amount = NewTaxAmount,
                Description = string.IsNullOrEmpty(NewTaxDescription) ? null : NewTaxDescription
            };

            context.Taxe.Add(newTax);
            await context.SaveChangesAsync();
            await Refresh();
            
            // Clear input fields
            NewTaxName = string.Empty;
            NewTaxAmount = 0;
            NewTaxDescription = string.Empty;
        });

        public ICommand DeleteTaxCommand => new AsyncRelayCommand<Taxes>(async (tax) =>
        {
            try
            {
                context.Taxe.Remove(tax);
                await context.SaveChangesAsync();
                await Refresh();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error deleting tax: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        });

        public ICommand EditTaxCommand => new AsyncRelayCommand<Taxes>(async (tax) =>
        {
            try
            {
                context.Taxe.Update(tax);
                await context.SaveChangesAsync();
                await Refresh();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error updating tax: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        });

        public ICommand CopyTaxCommand => new RelayCommand<Taxes>((tax) =>
        {
            if (tax != null)
            {
                NewTaxName = tax.Name;
                NewTaxAmount = tax.Amount;
                NewTaxDescription = tax.Description ?? string.Empty;
            }
        });
    }
}
