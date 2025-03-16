using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using ticketlibrary.Models;
using admintickets.Context;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Windows;
using System.Linq;
using System.Windows.Threading;

namespace admintickets.ViewModels
{
    public class VMGateControl : ObservableObject
    {
        private BestTicketContext context;
        private DispatcherTimer _timer;
        
        // Private backing field for the command
        private AsyncRelayCommand _sendSignalCommand;
        
        #region Properties
        
        private ObservableCollection<Hospital> _hospitals;
        public ObservableCollection<Hospital> Hospitals
        {
            get => _hospitals;
            set => SetProperty(ref _hospitals, value);
        }
        
        private Hospital _selectedHospital;
        public Hospital SelectedHospital
        {
            get => _selectedHospital;
            set 
            {
                if (SetProperty(ref _selectedHospital, value))
                {
                    if (_selectedHospital != null)
                    {
                        LoadActiveSignals();
                    }
                    else
                    {
                        ActiveEntrySignal = null;
                        ActiveExitSignal = null;
                    }
                }
            }
        }
        
        private string _selectedGateType = Signal.OpenEntryGate;
        public string SelectedGateType
        {
            get => _selectedGateType;
            set => SetProperty(ref _selectedGateType, value);
        }
        
        private TimeSpan _duration = TimeSpan.FromMinutes(1);
        public TimeSpan Duration
        {
            get => _duration;
            set => SetProperty(ref _duration, value);
        }
        
        private bool _isProcessing;
        public bool IsProcessing
        {
            get => _isProcessing;
            set => SetProperty(ref _isProcessing, value);
        }
        
        private string _statusMessage;
        public string StatusMessage
        {
            get => _statusMessage;
            set => SetProperty(ref _statusMessage, value);
        }
        
        private bool _showStatusMessage;
        public bool ShowStatusMessage
        {
            get => _showStatusMessage;
            set => SetProperty(ref _showStatusMessage, value);
        }

        private int _hoursValue = 0;
        public int HoursValue
        {
            get => _hoursValue;
            set
            {
                if (SetProperty(ref _hoursValue, value))
                {
                    UpdateDuration();
                }
            }
        }

        private int _minutesValue = 1;
        public int MinutesValue
        {
            get => _minutesValue;
            set
            {
                if (SetProperty(ref _minutesValue, value))
                {
                    UpdateDuration();
                }
            }
        }

        private int _secondsValue = 0;
        public int SecondsValue
        {
            get => _secondsValue;
            set
            {
                if (SetProperty(ref _secondsValue, value))
                {
                    UpdateDuration();
                }
            }
        }
        
        // Active signal properties
        private Signal _activeEntrySignal;
        public Signal ActiveEntrySignal
        {
            get => _activeEntrySignal;
            set => SetProperty(ref _activeEntrySignal, value);
        }
        
        private Signal _activeExitSignal;
        public Signal ActiveExitSignal
        {
            get => _activeExitSignal;
            set => SetProperty(ref _activeExitSignal, value);
        }
        
        #endregion
        
        public VMGateControl()
        {
            context = new BestTicketContext();
            
            // Initialize the command first
            _sendSignalCommand = new AsyncRelayCommand(SendSignal, CanSendSignal);
            
            // Initialize time values from default duration
            HoursValue = (int)Duration.TotalHours;
            MinutesValue = Duration.Minutes;
            SecondsValue = Duration.Seconds;
            
            // Load hospitals after command is initialized
            LoadHospitals();
            
            // Initialize timer to update signal status
            _timer = new DispatcherTimer
            {
                Interval = TimeSpan.FromSeconds(1)
            };
            _timer.Tick += Timer_Tick;
            _timer.Start();
        }
        
        private void Timer_Tick(object sender, EventArgs e)
        {
            // Update UI for active signals
            OnPropertyChanged(nameof(ActiveEntrySignal));
            OnPropertyChanged(nameof(ActiveExitSignal));
        }
        
        private async Task LoadHospitals()
        {
            try
            {
                var hospitals = await context.Hospital.ToListAsync();
                Hospitals = new ObservableCollection<Hospital>(hospitals);
                
                if (Hospitals.Count > 0)
                {
                    SelectedHospital = Hospitals.First();
                    // Manually notify command that it can execute now
                    _sendSignalCommand.NotifyCanExecuteChanged();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading hospitals: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
        
        private async Task LoadActiveSignals()
        {
            if (SelectedHospital == null)
                return;
                
            try
            {
                // Get active signals for the selected hospital
                var signals = await context.Signal
                    .Where(s => s.HospitalId == SelectedHospital.Id && s.EndTime > DateTime.Now)
                    .ToListAsync();
                    
                ActiveEntrySignal = signals.FirstOrDefault(s => s.SignalType == Signal.OpenEntryGate);
                ActiveExitSignal = signals.FirstOrDefault(s => s.SignalType == Signal.OpenExitGate);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading active signals: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
        
        // Replace the property definition with the backing field
        public ICommand SendSignalCommand => _sendSignalCommand;
        
        private bool CanSendSignal()
        {
            return SelectedHospital != null && Duration > TimeSpan.Zero && !IsProcessing;
        }
        
        private async Task SendSignal()
        {
            if (SelectedHospital == null)
                return;
            
            IsProcessing = true;
            ShowStatusMessage = false;
            
            try
            {
                // Check if there's an existing signal of the same type
                var existingSignal = await context.Signal
                    .FirstOrDefaultAsync(s => s.HospitalId == SelectedHospital.Id && 
                                        s.SignalType == SelectedGateType && 
                                        s.EndTime > DateTime.Now);
                                        
                if (existingSignal != null)
                {
                    // Update the existing signal
                    existingSignal.EndTime = DateTime.Now.Add(Duration);
                    context.Signal.Update(existingSignal);
                    StatusMessage = $"Updated existing signal for {SelectedHospital.Name}. The {GetGateTypeDisplayName()} gate will remain open for {GetDurationDisplayText()}.";
                }
                else
                {
                    // Create a new signal
                    var signal = new Signal
                    {
                        HospitalId = SelectedHospital.Id,
                        SignalType = SelectedGateType,
                        EndTime = DateTime.Now.Add(Duration)
                    };
                    
                    context.Signal.Add(signal);
                    StatusMessage = $"Signal sent successfully to {SelectedHospital.Name}. The {GetGateTypeDisplayName()} gate will open for {GetDurationDisplayText()}.";
                }
                
                await context.SaveChangesAsync();
                ShowStatusMessage = true;
                
                // Refresh active signals
                await LoadActiveSignals();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error sending signal: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            finally
            {
                IsProcessing = false;
            }
        }
        
        private string GetGateTypeDisplayName()
        {
            return SelectedGateType == Signal.OpenEntryGate ? "entry" : "exit";
        }
        
        private string GetDurationDisplayText()
        {
            if (Duration.TotalHours >= 1)
                return $"{Duration.TotalHours:0.#} hours";
            else if (Duration.TotalMinutes >= 1)
                return $"{Duration.TotalMinutes:0.#} minutes";
            else
                return $"{Duration.TotalSeconds:0.#} seconds";
        }
        

        public ICommand CancelSignalCommand => new AsyncRelayCommand<string>(CancelSignal);
        
        private async Task CancelSignal(string signalType)
        {
            if (SelectedHospital == null)
                return;
                
            try
            {
                var signal = await context.Signal
                    .FirstOrDefaultAsync(s => s.HospitalId == SelectedHospital.Id && 
                                        s.SignalType == signalType && 
                                        s.EndTime > DateTime.Now);
                                        
                if (signal != null)
                {
                    // End the signal immediately
                    signal.EndTime = DateTime.Now;
                    context.Signal.Update(signal);
                    await context.SaveChangesAsync();
                    
                    var gateType = signalType == Signal.OpenEntryGate ? "entry" : "exit";
                    StatusMessage = $"Signal for {gateType} gate cancelled successfully.";
                    ShowStatusMessage = true;
                    
                    // Refresh active signals
                    await LoadActiveSignals();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error cancelling signal: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void UpdateDuration()
        {
            Duration = new TimeSpan(HoursValue, MinutesValue, SecondsValue);
            _sendSignalCommand.NotifyCanExecuteChanged();
        }
    }
}
