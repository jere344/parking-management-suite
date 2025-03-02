using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Newtonsoft.Json;
using ticketlibrary.Models; // Contains Hospital model

namespace GatesSoftware.ViewModels
{
    public class FirstSetupViewModel : ObservableObject
    {
        private readonly HttpClient _httpClient = new HttpClient();

        public ObservableCollection<Hospital> Hospitals { get; set; } = new ObservableCollection<Hospital>();

        private Hospital _selectedHospital;
        public Hospital SelectedHospital
        {
            get => _selectedHospital;
            set
            {
                _selectedHospital = value;
                OnPropertyChanged(nameof(SelectedHospital));
                SaveCommand.NotifyCanExecuteChanged();
            }
        }

        private string _selectedGateType;
        public string SelectedGateType
        {
            get => _selectedGateType;
            set
            {
                _selectedGateType = value;
                OnPropertyChanged(nameof(SelectedGateType));
                SaveCommand.NotifyCanExecuteChanged();
            }
        }

        private string _password;
        // Bound via code-behind from the PasswordBox.
        public string Password
        {
            get => _password;
            set
            {
                _password = value;
                OnPropertyChanged(nameof(Password));
                SaveCommand.NotifyCanExecuteChanged();
            }
        }

        // Change the type to IAsyncRelayCommand so we can call NotifyCanExecuteChanged.
        public IAsyncRelayCommand SaveCommand { get; }

        public FirstSetupViewModel()
        {
            SaveCommand = new AsyncRelayCommand(SaveSettings, CanSave);
            LoadHospitals();
        }

        private bool CanSave() =>
            SelectedHospital != null &&
            !string.IsNullOrWhiteSpace(Password) &&
            !string.IsNullOrWhiteSpace(SelectedGateType);

        private async void LoadHospitals()
        {
            try
            {
                // Call GET /list_hospitals (adjust the base URL as needed)
                var response = await _httpClient.GetAsync($"{Environment.GetEnvironmentVariable("API_BASE_URL")}/list_hospitals");
                if (response.IsSuccessStatusCode)
                {
                    var json = await response.Content.ReadAsStringAsync();
                    var hospitals = JsonConvert.DeserializeObject<Hospital[]>(json);
                    Application.Current.Dispatcher.Invoke(() =>
                    {
                        Hospitals.Clear();
                        foreach (var hosp in hospitals)
                        {
                            Hospitals.Add(hosp);
                        }
                    });
                }
                else
                {
                    MessageBox.Show("Error loading hospitals.");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error: {ex.Message}");
            }
        }

        private async Task SaveSettings()
        {
            try
            {
                // Verify credentials via POST /verify_credentials
                var payload = new
                {
                    hospital_id = SelectedHospital.Id,
                    password = Password
                };
                var content = new StringContent(JsonConvert.SerializeObject(payload), Encoding.UTF8, "application/json");
                var response = await _httpClient.PostAsync($"{Environment.GetEnvironmentVariable("API_BASE_URL")}/verify_credentials", content);
                var json = await response.Content.ReadAsStringAsync();
                dynamic result = JsonConvert.DeserializeObject(json);
                if (result.valid == true)
                {
                    // Save settings (assumes SavedSettings is a dictionary in your App)
                    var sav = ((App)Application.Current).SavedSettings;
                    sav["gateway_password"] = Password;
                    sav["hospital_id"] = SelectedHospital.Id;
                    sav["gate_type"] = SelectedGateType;

                    App.SaveSettings(); // Will be autosave on exit, but we can still call it here to be sure

                    // now tell the user that we will close the app. Close when they click OK
                    if (MessageBox.Show("Settings saved. The application will now close. Please restart it.", "Settings saved", MessageBoxButton.OK) == MessageBoxResult.OK)
                    {
                        Application.Current.Shutdown();
                    }
                    else // if they close the message box, close the app anyway
                    {
                        Application.Current.Shutdown();
                    }

                }
                else
                {
                    MessageBox.Show("Invalid credentials. Please try again.");
                    ((App)Application.Current).SavedSettings.Clear();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error: {ex.Message}");
            }
        }
    }
}
