using System;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using Newtonsoft.Json;
using ticketlibrary.Models;

namespace GatesSoftware.Services
{
    public class GateStatusService
    {
        private readonly HttpClient _httpClient = new HttpClient();
        private bool _temporaryOpenState = false;
        private DateTime _temporaryOpenUntil = DateTime.MinValue;

        public void SetTemporaryOpenState(TimeSpan duration)
        {
            _temporaryOpenState = true;
            _temporaryOpenUntil = DateTime.Now.Add(duration);
        }

        public async Task<bool> IsGateOpen(int hospitalId, string password, string gateType)
        {
            // Check if we're in a temporary open state
            if (_temporaryOpenState && DateTime.Now < _temporaryOpenUntil)
            {
                return true;
            }
            else if (_temporaryOpenState && DateTime.Now >= _temporaryOpenUntil)
            {
                // Reset temporary state if it expired
                _temporaryOpenState = false;
            }
            
            try
            {
                string signalType = gateType == "entry" ? Signal.OpenEntryGate : Signal.OpenExitGate;
                
                var payload = new
                {
                    hospital_id = hospitalId,
                    password = password,
                    signal_type = signalType
                };
                
                var content = new StringContent(JsonConvert.SerializeObject(payload), Encoding.UTF8, "application/json");
                var response = await _httpClient.PostAsync($"{Environment.GetEnvironmentVariable("API_BASE_URL")}/check_gate_signals", content);
                if (response.IsSuccessStatusCode)
                {
                    var json = await response.Content.ReadAsStringAsync();
                    dynamic result = JsonConvert.DeserializeObject(json);
                    
                    // If there are any active signals, the gate is open
                    return result.signals.Count > 0;
                }
                
                return false;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error checking gate status: {ex.Message}");
                return false;
            }
        }
    }
}
