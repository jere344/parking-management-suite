using System;
using System.ComponentModel;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using CommunityToolkit.Mvvm.Input;
using Newtonsoft.Json;
using ticketlibrary.Models;

namespace GatesSoftware.ViewModels
{
    public class ExitGateViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        private readonly HttpClient _httpClient = new HttpClient();

        private string _ticketId;
        public string TicketId
        {
            get => _ticketId;
            set
            {
                _ticketId = value;
                OnPropertyChanged(nameof(TicketId));
                ((AsyncRelayCommand)ValidateTicketCommand).NotifyCanExecuteChanged();
            }
        }

        public ICommand ValidateTicketCommand { get; }
        public ICommand ScanTicketCommand { get; }

        public ExitGateViewModel()
        {
            ValidateTicketCommand = new AsyncRelayCommand(ValidateTicket, () => !string.IsNullOrWhiteSpace(TicketId));
            ScanTicketCommand = new AsyncRelayCommand(ScanTicket);
        }

        private async Task ValidateTicket()
        {
            try
            {
                var sav = ((App)Application.Current).SavedSettings;
                int hospitalId = Convert.ToInt32(sav["hospital_id"]);
                string password = sav["gateway_password"].ToString();

                if (!int.TryParse(TicketId, out int ticketId))
                {
                    MessageBox.Show("Invalid Ticket ID format.");
                    return;
                }

                var payload = new
                {
                    hospital_id = hospitalId,
                    password = password,
                    ticket_id = ticketId
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
            // TODO : Integrate scanner hardware to capture ticket data.
            // For now, simulate scanning by auto-filling a Ticket ID and calling validation.
            MessageBox.Show("Scanning ticket... (simulate scanner integration)");
            TicketId = "123"; // Simulated scanned ticket id
            await ValidateTicket();
        }

        private void OnPropertyChanged(string propName) =>
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propName));
    }
}
