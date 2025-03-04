﻿using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using System.Threading;
using System.Globalization;
using System.Windows.Media;
using System.Diagnostics;
using MaterialDesignThemes.Wpf;
using paymentterminal.Services;



namespace paymentterminal
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public NavigationController NavigationController { get; set; }
        public InactivityService InactivityService { get; set; }

        public MainWindow()
        {
            InitializeComponent();
            DataContext = this;
            MainNavigationFrame.NavigationUIVisibility = NavigationUIVisibility.Hidden;
            NavigationController = new NavigationController(MainNavigationFrame.NavigationService);
            InactivityService = new InactivityService();
            InactivityService.InactivityTimeout += (sender, e) => NavigateTo("Views/ViewHome.xaml");
            InactivityService.IsTicking = () => NavigationController.GetCurrentViewUri()?.ToString() != "Views/ViewHome.xaml";
            NavigationController.NavigateHome();
        }


        public void NavigateTo(string uri, object? parameter = null)
        {
            if (uri == "Views/Logout.xaml")
            {
                App.Current.ConnectedUser = null;
                // we also need to clear remember me on logout
                App.Current.SavedSettings["SessionToken"] = null;
                NavigateTo(Routes.HomeViews["Guest"]);

                NavigationController.ClearHistory();
                return;
            }
            NavigationController._NavigateTo(uri, parameter);
            DrawerHost.CloseDrawerCommand.Execute(Dock.Left, null);
        }

        public void ChangeAccount()
        {
            NavigationController.ClearHistory();
        }

        // Clicking on the logo will open the GitHub page
        private void OpenGitHub(object sender, RoutedEventArgs e)
        {
            Process.Start(new ProcessStartInfo
            {
                FileName = "https://github.com/jere344/parking-ticket",
                UseShellExecute = true
            });
        }

        public void SetWindowInfos(string title, double minHeight = 0, double minWidth = 0, double maxHeight = double.PositiveInfinity, double maxWidth = double.PositiveInfinity)
        {
            Window window = GetWindow(this);
            if (window != null)
            {
                window.Title = title;
                window.MinHeight = minHeight;
                window.MinWidth = minWidth;
                window.MaxHeight = maxHeight;
                window.MaxWidth = maxWidth;
            }
        }
    }
}