using System;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Threading;
using GatesSoftware.Services;

namespace GatesSoftware;

/// <summary>
/// Interaction logic for MainWindow.xaml
/// </summary>
public partial class MainWindow : Window
{
    public SimplerNavigationController NavigationController { get; set; }
    public GateStatusService GateStatusService { get; private set; } = new GateStatusService();
    private DispatcherTimer _gateStatusTimer;

    public MainWindow()
    {
        InitializeComponent();
        NavigationController = new SimplerNavigationController(MainNavigationFrame.NavigationService);

        // check if the gateway is already logged in
        var sav = ((App)App.Current).SavedSettings;
        if (sav.Contains("gateway_password") && sav.Contains("hospital_id") && sav.Contains("gate_type"))
        {
            if (sav["gate_type"]?.ToString() == "entry")
            {
                NavigationController._NavigateTo("Views/EntryGate.xaml");
            }
            else if (sav["gate_type"]?.ToString() == "exit")
            {
                NavigationController._NavigateTo("Views/ExitGate.xaml");
            }
            else
            {
                throw new Exception("Invalid gate type");
            }
            
            // Start timer to check gate status periodically
            _gateStatusTimer = new DispatcherTimer();
            _gateStatusTimer.Interval = TimeSpan.FromSeconds(5); // Check every 5 seconds
            _gateStatusTimer.Tick += async (s, e) => await CheckGateStatus();
            _gateStatusTimer.Start();
            
            // Initial check
            Task.Run(async () => await CheckGateStatus());
        }
        else
        {
            NavigationController._NavigateTo("Views/FirstSetup.xaml");
        }
    }
    
    private async Task CheckGateStatus()
    {
        var sav = ((App)App.Current).SavedSettings;
        if (sav.Contains("gateway_password") && sav.Contains("hospital_id") && sav.Contains("gate_type"))
        {
            int hospitalId = Convert.ToInt32(sav["hospital_id"]);
            string password = sav["gateway_password"].ToString();
            string gateType = sav["gate_type"].ToString();
            
            bool isOpen = await GateStatusService.IsGateOpen(hospitalId, password, gateType);
            
            // Update UI on UI thread
            Application.Current.Dispatcher.Invoke(() =>
            {
                GateStatusIndicator.Background = isOpen ? Brushes.Green : Brushes.Red;
                GateStatusText.Text = isOpen ? "Open" : "Closed";
            });
        }
    }
}