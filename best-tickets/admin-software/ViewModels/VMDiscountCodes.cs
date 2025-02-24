using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using ticketlibrary.Models;
using admintickets.Context;
using Microsoft.Win32;
using System;
using System.Collections.ObjectModel;
using System.Drawing;
using System.IO;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media.Imaging;
using System.Windows.Input;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.Drawing.Text;
using System.Linq;
using System.Windows.Controls; // For MessageBox
using System.Windows.Data;
using admintickets.Helpers;
using Microsoft.EntityFrameworkCore;

namespace admintickets.ViewModels
{
    public class VMDiscountCodes : ObservableObject
    {
        private BestTicketContext context;

        private ObservableCollection<Code> _filteredCodes;
        public ObservableCollection<Code> FilteredCodes
        {
            get => _filteredCodes;
            set => SetProperty(ref _filteredCodes, value);
        }

        private ObservableCollection<Code> _codes;
        public ObservableCollection<Code> Codes 
        {
            get => _codes;
            set {
                SetProperty(ref _codes, value);
                FilteredCodes = new ObservableCollection<Code>(Codes.Where(c => c.IsActive || ShowDisabled));
                OnPropertyChanged(nameof(FilteredCodes));
            }
        }

        private bool _showDisabled = false;
        public bool ShowDisabled
        {
            get => _showDisabled;
            set
            {
                SetProperty(ref _showDisabled, value);
                if (Codes != null)
                    FilteredCodes = new ObservableCollection<Code>(Codes.Where(c => c.IsActive || ShowDisabled));
                OnPropertyChanged(nameof(FilteredCodes));
            }
        }

        // Fields for the new discount code
        private string _newCodeName;
        public string NewCodeName
        {
            get => _newCodeName;
            set => SetProperty(ref _newCodeName, value);
        }

        private string _newCodeDescription;
        public string NewCodeDescription
        {
            get => _newCodeDescription;
            set => SetProperty(ref _newCodeDescription, value);
        }

        private decimal _newCodeReduction;
        public decimal NewCodeReduction
        {
            get => _newCodeReduction;
            set { 
                if (value >= 0 && value <= 100)
                    SetProperty(ref _newCodeReduction, value);
            }
        }

        private bool _newCodeIsActive;
        public bool NewCodeIsActive
        {
            get => _newCodeIsActive;
            set => SetProperty(ref _newCodeIsActive, value);
        }

        public ObservableCollection<Hospital> Hospitals { get; set; }

        private Hospital? _newCodeHospital;
        public Hospital? NewCodeHospital
        {
            get => _newCodeHospital;
            set => SetProperty(ref _newCodeHospital, value);
        }




        public VMDiscountCodes()
        {
            context = new BestTicketContext();
            Hospitals = new ObservableCollection<Hospital>(context.Hospital.ToList());
            Hospitals.Insert(0, new Hospital { Name = "Global", Id = -1 });

            Refresh();
        }

        private async Task Refresh()
        {
            Codes?.Clear();
            Codes = new ObservableCollection<Code>(await context.DiscountCode.ToListAsync());
            OnPropertyChanged(nameof(Codes));
        }

        public ICommand AddCodeCommand => new AsyncRelayCommand(async () =>
        {
            if (string.IsNullOrEmpty(NewCodeName) || string.IsNullOrEmpty(NewCodeDescription) || NewCodeReduction <= 0 || NewCodeReduction > 100)
            {
                MessageBox.Show("Please fill in all fields.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            var newCode = new Code
            {
                Name = NewCodeName,
                Description = NewCodeDescription ?? string.Empty,
                Reduction = NewCodeReduction,
                IsActive = NewCodeIsActive,
                HospitalId = NewCodeHospital?.Id == -1 ? null : NewCodeHospital?.Id
            };

            context.DiscountCode.Add(newCode);
            await context.SaveChangesAsync();
            await Refresh();
        });

        public ICommand DeleteCodeCommand => new AsyncRelayCommand<Code>(async (code) =>
        {
            context.DiscountCode.Remove(code);
            await context.SaveChangesAsync();
            await Refresh();
        });

        public ICommand ToggleCodeActiveCommand => new RelayCommand<Code>(code =>
        {
            code.IsActive = !code.IsActive;
            context.DiscountCode.Update(code);
            context.SaveChanges();
            Refresh();
        });

        public ICommand CopyCodeCommand => new RelayCommand<Code>((code) =>
        {
            if (code != null)
            {
                NewCodeName = code.Name;
                NewCodeDescription = code.Description;
                NewCodeReduction = Math.Round(code.Reduction, 2);
                NewCodeIsActive = code.IsActive;
                NewCodeHospital = Hospitals.FirstOrDefault(h => h.Id == code.HospitalId) ?? Hospitals.FirstOrDefault(h => h.Id == -1);
            }
        });
    }
}


