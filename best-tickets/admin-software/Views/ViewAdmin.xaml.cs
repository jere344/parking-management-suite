using System.Windows;
using System.Windows.Controls;
using System;
using admintickets.ViewModels;
using System.Windows.Navigation;
using System.Threading;
using System.Globalization;

namespace admintickets.Views;

public partial class ViewAdmin : Page
{
    public ViewAdmin()
    {
        InitializeComponent();
        
        DataContext = new VMAdmin();

        this.Loaded += Page_Loaded;
    }

    private void Page_Loaded(object sender, RoutedEventArgs e)
    {
        ((MainWindow)Application.Current.MainWindow).SetWindowInfos("BestTickets - Admin", 600, 800);
    }
}
