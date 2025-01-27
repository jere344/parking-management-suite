using CommunityToolkit.Mvvm.ComponentModel;
using wisecorp.Models.DBModels;
using System.Windows;
using wisecorp.Context;
using Microsoft.EntityFrameworkCore;
using System.Windows.Input;
using CommunityToolkit.Mvvm.Input;
using System.Collections.ObjectModel;

namespace wisecorp.ViewModels;

public partial class VMAdmin : ObservableObject
{
    private List<Account>? accounts;



    private ObservableCollection<Account>? filteredAccounts;
    public ObservableCollection<Account>? FilteredAccounts
    {
        get { return filteredAccounts; }
        set { filteredAccounts = value; }
    }


    private Account? selectedAccount;
    public Account? SelectedAccount
    {
        get { return selectedAccount; }
        set { selectedAccount = value; }
    }




    private string? filterText;
    public string? FilterText
    {
        get { return filterText; }
        set
        {
            filterText = value;
            FilterAccounts();
        }
    }


    private readonly WisecorpContext context;

    private bool displayDisabled = false;
    public bool DisplayDisabled
    {
        get { return displayDisabled; }
        set
        {
            displayDisabled = value;
            FilterAccounts();
        }
    }


    public ICommand DeleteImageCommand { get; }

    public VMAdmin()
    {
        context = new WisecorpContext();

        _ = InitializeAsync();

        DeleteImageCommand = new RelayCommand(DeleteImage_Execute);
    }

    /// <summary>
    /// Permet d'initialiser les listes
    /// </summary>
    /// <returns></returns>
    private async Task InitializeAsync()
    {
        await GetAccounts();
        FilterAccounts();
    }

    /// <summary>
    /// Permet d'initialiser la liste des comptes
    /// </summary>
    /// <returns></returns>
    private async Task GetAccounts()
    {
        accounts = await context.Accounts.ToListAsync();
        filteredAccounts = new ObservableCollection<Account>(accounts);
        OnPropertyChanged(nameof(FilteredAccounts));
        SelectedAccount = accounts.FirstOrDefault();
    }


    /// <summary>
    /// Permet de filtrer les comptes pour la recherche
    /// </summary>
    public void FilterAccounts()
    {
        var f = accounts ?? new List<Account>();

        if(!string.IsNullOrEmpty(filterText))
            f = f.Where(a => a.FullName.ToLower().Contains(filterText.ToLower())).ToList();

        filteredAccounts = new ObservableCollection<Account>(f);
        OnPropertyChanged(nameof(FilteredAccounts));
    }


    /// <summary>
    /// Permet de rediriger a la fenêytre de création de compte
    /// </summary>
    [RelayCommand]
    public static void RedirectToAdd()
    {
        var mainWindow = (MainWindow)App.Current.MainWindow;
        mainWindow.NavigateTo("Views/Admin/ViewAjoutAcc.xaml");
    }


    // / <summary>
    // / Permet de supprimer l'image d'un compte
    // / </summary>
    private void DeleteImage_Execute()
    {
        if (selectedAccount != null)
        {
            MessageBoxResult result = MessageBox.Show((string)Application.Current.FindResource("deleteImageMessage"), (string)Application.Current.FindResource("deleteImage"), MessageBoxButton.YesNo, MessageBoxImage.Question);
            if (result == MessageBoxResult.No)
                return;
            selectedAccount.Picture = string.Empty;
            context.SaveChanges();
            OnPropertyChanged(nameof(selectedAccount));
            FilterAccounts();
        }
    }
    
}
