using CommunityToolkit.Mvvm.ComponentModel;
using admintickets.Models.DBModels;
using System.Windows;
using admintickets.Context;
using Microsoft.EntityFrameworkCore;
using System.Windows.Input;
using CommunityToolkit.Mvvm.Input;
using System.Collections.ObjectModel;

namespace admintickets.ViewModels;

public partial class VMAdmin : ObservableObject
{
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
        set { selectedUser = value; }
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


    private readonly BestTicketContext context;

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

    public VMAdmin()
    {
        context = new BestTicketContext();

        _ = InitializeAsync();

        DeleteImageCommand = new RelayCommand(DeleteImage_Execute);
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
        users = await context.Users.ToListAsync();
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

        if(!string.IsNullOrEmpty(filterText))
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
    
}
