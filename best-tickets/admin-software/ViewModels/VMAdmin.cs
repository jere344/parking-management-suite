using CommunityToolkit.Mvvm.ComponentModel;
using ticketlibrary.Models;
using System.Windows;
using admintickets.Context;
using Microsoft.EntityFrameworkCore;
using System.Windows.Input;
using CommunityToolkit.Mvvm.Input;
using System.Collections.ObjectModel;
using System.Windows.Controls;

namespace admintickets.ViewModels;

public partial class VMAdmin : ObservableObject
{
    private readonly BestTicketContext context;


    private List<User>? users;

    private ObservableCollection<User>? filteredUsers;
    public ObservableCollection<User>? FilteredUsers
    {
        get { return filteredUsers; }
        set { filteredUsers = value; }
    }

    private User? selectedUser;
    public User? SelectedUser
    {
        get { return selectedUser; }
        set
        {
            SetProperty(ref selectedUser, value);
        }
    }

    private string? filterText;
    public string? FilterText
    {
        get { return filterText; }
        set
        {
            filterText = value;
            FilterUsers();
        }
    }

    private bool displayDisabled = false;
    public bool DisplayDisabled
    {
        get { return displayDisabled; }
        set
        {
            displayDisabled = value;
            FilterUsers();
        }
    }


    public ICommand DeleteImageCommand { get; }
    public ICommand SaveCommand { get; }
    public ICommand AddAccountCommand { get; }

    public VMAdmin()
    {
        context = new BestTicketContext();

        _ = InitializeAsync();

        DeleteImageCommand = new RelayCommand(DeleteImage_Execute);
        SaveCommand = new RelayCommand(Save_Execute);
        AddAccountCommand = new RelayCommand(AddAccount_Execute);
    }

    /// <summary>
    /// Permet d'initialiser les listes
    /// </summary>
    /// <returns></returns>
    private async Task InitializeAsync()
    {
        await GetUsers();
        FilterUsers();
    }

    /// <summary>
    /// Permet d'initialiser la liste des comptes
    /// </summary>
    /// <returns></returns>
    private async Task GetUsers()
    {
        users = await context.User.ToListAsync();
        filteredUsers = new ObservableCollection<User>(users);
        OnPropertyChanged(nameof(FilteredUsers));
        SelectedUser = users.FirstOrDefault();
    }


    /// <summary>
    /// Permet de filtrer les comptes pour la recherche
    /// </summary>
    public void FilterUsers()
    {
        var f = users ?? new List<User>();

        if (!string.IsNullOrEmpty(filterText))
            f = f.Where(a => (a.FirstName + " " + a.LastName).ToLower().Contains(filterText.ToLower())).ToList();

        filteredUsers = new ObservableCollection<User>(f);
        OnPropertyChanged(nameof(FilteredUsers));
    }


    // / <summary>
    // / Permet de supprimer l'image d'un compte
    // / </summary>
    private void DeleteImage_Execute()
    {
        if (selectedUser != null)
        {
            MessageBoxResult result = MessageBox.Show((string)Application.Current.FindResource("deleteImageMessage"), (string)Application.Current.FindResource("deleteImage"), MessageBoxButton.YesNo, MessageBoxImage.Question);
            if (result == MessageBoxResult.No)
                return;
            selectedUser.Picture = string.Empty;
            context.SaveChanges();
            OnPropertyChanged(nameof(selectedUser));
            FilterUsers();
        }
    }

    private void Save_Execute()
    {
        if (SelectedUser != null)
        {
            context.SaveChanges();
            FilterUsers();
        }
    }

    private void AddAccount_Execute()
    {
        string RandomPassword = ticketlibrary.Helpers.CryptographyHelper.GenerateRandomString(8);
        var newUser = new User
        {
            Password = ticketlibrary.Helpers.CryptographyHelper.HashPassword(RandomPassword),
            FirstName = "New",
            LastName = "User",
            Email = "new.user@example.com",
            Phone = "000-000-0000",
            AccountCreationDate = DateTime.Now,
            Picture = string.Empty,
        };
        context.User.Add(newUser);
        context.SaveChanges();
        users.Add(newUser);
        FilterUsers();
        SelectedUser = newUser;

        // Show a custom dialog with the password and a copy button
        ShowPasswordDialog(RandomPassword);
    }

    private void ShowPasswordDialog(string password)
    {
        Window dialog = null;
        dialog = new Window
        {
            Title = (string)Application.Current.FindResource("newAccount"),
            SizeToContent = SizeToContent.WidthAndHeight,
            WindowStartupLocation = WindowStartupLocation.CenterScreen,
            Content = new StackPanel
            {
                Margin = new Thickness(20),
                Children =
                {
                    new TextBlock { Text = (string)Application.Current.FindResource("newAccountMessage") + " " + password + Environment.NewLine + (string)Application.Current.FindResource("newAccountMessage2") },
                    new Button
                    {
                        Content = "Copy Password",
                        Margin = new Thickness(0, 10, 0, 0),
                        Command = new RelayCommand(() => Clipboard.SetDataObject(password))
                    },
                    new Button
                    {
                        Content = "OK",
                        Margin = new Thickness(0, 10, 0, 0),
                        Command = new RelayCommand(() => dialog.Close())
                    }
                }
            }
        };
        dialog.ShowDialog();
    }

}
