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

using Microsoft.Win32;
using System;
using System.Diagnostics;
using System.Threading.Tasks;
using QuestPDF.Fluent;
using System.Windows.Media;


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

                    // Set gate to temporarily open for 10 seconds
                    var mainWindow = (MainWindow)Application.Current.MainWindow;
                    mainWindow.GateStatusService.SetTemporaryOpenState(TimeSpan.FromSeconds(10));
                    
                    // Update UI immediately
                    Application.Current.Dispatcher.Invoke(() => {
                        mainWindow.GateStatusIndicator.Background = Brushes.Green;
                        mainWindow.GateStatusText.Text = "Open";
                    });

                    MessageBox.Show($"Ticket generated: {ticket.Id}\nPrinting ticket...");
                    _ = PrintTicketAsPdf(ticket);
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

        private static async Task PrintTicketAsPdf(Ticket CurrentTicket)
        {

            SaveFileDialog saveFileDialog = new SaveFileDialog
            {
                Filter = "PDF Files (*.pdf)|*.pdf",
                FileName = $"Ticket_{CurrentTicket.TicketNumber}.pdf"
            };

            if (saveFileDialog.ShowDialog() == true)
            {
                string filePath = saveFileDialog.FileName;
                await GenerateTicketPdf(CurrentTicket, filePath);
                if (MessageBox.Show("Ticket saved as PDF. Do you want to open it?", "Success", MessageBoxButton.YesNo, MessageBoxImage.Information) == MessageBoxResult.Yes)
                {
                    Process.Start(new ProcessStartInfo(filePath) { UseShellExecute = true });
                }
            }
        }

        // Helper methods for PDF/PNG generation
        private static async Task GenerateTicketPdf(Ticket ticket, string filePath)
        {
            try {
                var ticketDocument = new ticket_library.Documents.TicketDocument(ticket);
                ticketDocument.GeneratePdf(filePath);
            }
            catch (System.Exception ex)
            {
                MessageBox.Show($"Error generating PDF: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }

            await Task.CompletedTask;
        }
    }
}
