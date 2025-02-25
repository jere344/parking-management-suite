using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace GatesSoftware;

/// <summary>
/// Interaction logic for MainWindow.xaml
/// </summary>
public partial class MainWindow : Window
{
    public SimplerNavigationController NavigationController { get; set; }

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
        }
        else
        {
            NavigationController._NavigateTo("Views/FirstSetup.xaml");
        }
    }
}