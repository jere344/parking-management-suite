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
using ticketlibrary.Models; // Contains Ticket model

namespace GatesSoftware.ViewModels
{
    public class EntryGateViewModel : ObservableObject
    {
        private readonly HttpClient _httpClient = new HttpClient();

        public ICommand GenerateTicketCommand { get; }

        public EntryGateViewModel()
        {
            GenerateTicketCommand = new AsyncRelayCommand(GenerateTicket);
        }

        private async Task GenerateTicket()
        {
            try
            {
                // Retrieve saved settings
                var sav = ((App)Application.Current).SavedSettings;
                int hospitalId = Convert.ToInt32(sav["hospital_id"]);
                string password = sav["gateway_password"].ToString();

                var payload = new
                {
                    hospital_id = hospitalId,
                    password = password
                };

                var content = new StringContent(JsonConvert.SerializeObject(payload), Encoding.UTF8, "application/json");
                var response = await _httpClient.PostAsync($"{Environment.GetEnvironmentVariable("API_BASE_URL")}/create_ticket", content);
                if (response.IsSuccessStatusCode)
                {
                    var json = await response.Content.ReadAsStringAsync();
                    Ticket ticket = JsonConvert.DeserializeObject<Ticket>(json);

                    // TODO : Print Ticket
                    MessageBox.Show($"Ticket generated: {ticket.Id}\nPrinting ticket...");
                }
                else
                {
                    MessageBox.Show("Error generating ticket. Possibly invalid credentials.");
                    ((App)Application.Current).SavedSettings.Clear();
                    ((MainWindow)Application.Current.MainWindow).NavigationController._NavigateTo("Views/FirstSetup.xaml");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error: {ex.Message}");
            }
        }
    }
}
