using System.Windows;
using System.Windows.Controls;
using GatesSoftware.ViewModels;

namespace GatesSoftware.Views;

public partial class EntryGate : Page
{
    public EntryGate()
    {
        InitializeComponent();
        DataContext = new EntryGateViewModel();
    }

    private void OnEntryButtonClick(object sender, RoutedEventArgs e)
    {
        // Handle entry button click event
        MessageBox.Show("Entry button clicked!");
    }
}
