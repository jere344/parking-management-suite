using System;
using System.Windows;
using System.Windows.Controls;
using GatesSoftware.ViewModels;

namespace GatesSoftware.Views
{
    public partial class ExitGate : Page
    {
        public ExitGate()
        {
            InitializeComponent();
            DataContext = new ExitGateViewModel();
        }

        private void OnExitButtonClick(object sender, RoutedEventArgs e)
        {
            // Handle exit button click event
            MessageBox.Show("Exit button clicked!");
        }
    }
}