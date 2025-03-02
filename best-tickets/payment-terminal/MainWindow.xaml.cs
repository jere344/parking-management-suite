﻿using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using System.Threading;
using System.Globalization;
using System.Windows.Media;
using System.Diagnostics;
using MaterialDesignThemes.Wpf;



namespace paymentterminal
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public NavigationController NavigationController { get; set; }

        public MainWindow()
        {
            InitializeComponent();
            DataContext = NavigationController;
            MainNavigationFrame.NavigationUIVisibility = NavigationUIVisibility.Hidden;
            NavigationController = new NavigationController(MainNavigationFrame.NavigationService);
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

        private void Next_Btn(object sender, RoutedEventArgs e)
        {
            if (this.NavigationController.CanGoForward)
                NavigationController.NavigateForward();
        }

        private void Back_Btn(object sender, RoutedEventArgs e)
        {
            if (this.NavigationController.CanGoBack)
                NavigationController.NavigateBack();
        }

        private void Home_Btn(object sender, RoutedEventArgs e)
        {
            NavigationController.NavigateHome();
        }

        private void Open_Settings(object sender, RoutedEventArgs e)
        {
            NavigateTo("Views/ViewSettings.xaml");
        }

        private void Test(object sender, RoutedEventArgs e)
        {
            // check if the user is connected
            if (App.Current.ConnectedUser == null)
            {
                MessageBox.Show((string)Application.Current.FindResource("must_be_connected"), (string)Application.Current.FindResource("error"), MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            NavigateTo("Views/ViewProfile.xaml");
        }

        private void Logout(object sender,RoutedEventArgs e)
        {
            NavigateTo("Views/Logout.xaml");
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