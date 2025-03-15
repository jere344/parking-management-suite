using System;
using System.ComponentModel;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Newtonsoft.Json;
using ticketlibrary.Models;

namespace GatesSoftware.ViewModels
{
    public class ExitGateViewModel : ObservableObject
    {
        private readonly HttpClient _httpClient = new HttpClient();

        private string _ticketNumber;
        public string TicketNumber
        {
            get => _ticketNumber;
            set
            {
                // ticket format is xxx-xxx-xxx
                // while the user is typing, add dashes to the ticket number

                value = value.ToUpper();
                if (value.Length > 11)
                {
                    value = value[..11];
                }
                
                // if it's a backspace
                if (value?.Length < _ticketNumber?.Length)
                {
                    // if we are backspacing over a dash, remove the dash and the number
                    if (value.Length == 3 || value.Length == 7)
                    {
                        _ticketNumber = value[..^1];
                        // ---- : fix wpf stupid inconsistency
                        // ----when last input is a number, cursor doesn't move to the end
                        // ----but when last input is a letter, cursor moves to the end
                        // > Fixed, see TextBoxBehaviorsSubscription.cs and TextBoxBehaviors.cs
                    }
                    else
                    {
                        _ticketNumber = value;
                    }
                }
                else // if it's a new character
                {
                    // if we are at a position where a dash should be, add the dash and the number
                    if (value.Length == 3 || value.Length == 7)
                    {
                        _ticketNumber = value + "-";
                    }
                    else
                    {
                        _ticketNumber = value;
                    }
                }
                
                OnPropertyChanged(nameof(TicketNumber));
                ((AsyncRelayCommand)ValidateTicketCommand).NotifyCanExecuteChanged();
            }
        }

        public ICommand ValidateTicketCommand { get; }
        public ICommand ScanTicketCommand { get; }

        public ExitGateViewModel()
        {
            ValidateTicketCommand = new AsyncRelayCommand(ValidateTicket, () => !string.IsNullOrWhiteSpace(TicketNumber));
            ScanTicketCommand = new AsyncRelayCommand(ScanTicket);
        }

        private async Task ValidateTicket()
        {
            try
            {
                var sav = ((App)Application.Current).SavedSettings;
                int hospitalId = Convert.ToInt32(sav["hospital_id"]);
                string password = sav["gateway_password"].ToString();

                if (TicketNumber.Length != 11 || TicketNumber[3] != '-' || TicketNumber[7] != '-')
                {
                    MessageBox.Show("Invalid Ticket ID format.");
                    return;
                }

                var payload = new
                {
                    hospital_id = hospitalId,
                    password = password,
                    ticket_number = TicketNumber
                };

                var content = new StringContent(JsonConvert.SerializeObject(payload), Encoding.UTF8, "application/json");
                var response = await _httpClient.PostAsync($"{Environment.GetEnvironmentVariable("API_BASE_URL")}/validate_ticket", content);
                var json = await response.Content.ReadAsStringAsync();
                dynamic result = JsonConvert.DeserializeObject(json);
                if (result.valid == true)
                {
                    // TODO : Open Gate
                    MessageBox.Show("Ticket validated. Opening gate...");
                }
                else
                {
                    string errorMsg = result.error != null ? result.error.ToString() : "Ticket validation failed.";
                    MessageBox.Show(errorMsg);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error: {ex.Message}");
            }
        }

        private async Task ScanTicket()
        {
            TicketNumber = "1B3-BCD-E2A"; // Simulated scanned ticket id
            await ValidateTicket();
        }
    }
}
