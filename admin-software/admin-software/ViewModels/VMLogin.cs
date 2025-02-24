using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System.Windows;
using System.Security.Principal;
using System.Windows.Input;
using admintickets.Context;
using admintickets.Models;
using admintickets.Models.DBModels;
using System.Security;
using System.Runtime.InteropServices;

namespace admintickets.ViewModels;

public class VMLogin : ObservableObject
{
    private string? email;
    public string? Email
    {
        get { return email; }
        set { email = value; }
    }

    //Partie pour le password pas en clair
    private SecureString? securePassword;

    /// <summary>
    /// Set le password en SecureString
    /// </summary>
    /// <param name="password">Mots de passe</param>
    public void SetPassword(SecureString password)
    {
        securePassword = password;
    }

    private string? password;
    public string? Password
    {
        get { return password; }
        set { password = value; }
    }

    private string errorMessage;
    public string ErrorMessage
    {
        get { return errorMessage; }
    }

    private readonly BestTicketContext context;

    public VMLogin()
    {
        errorMessage = "";
        context = new BestTicketContext();
        Login = new RelayCommand(Login_Execute);
    }

    public VMLogin(BestTicketContext context)
    {
        errorMessage = "";
        this.context = context;
        Login = new RelayCommand(Login_Execute);
    }


    /// <summary>
    /// Commande pour gérer le processus de connexion de l'utilisateur.
    /// Vérifie les champs de saisie, valide les informations d'identification et
    /// effectue la connexion si les informations sont correctes.
    /// </summary>
    public ICommand Login { get; }
    private void Login_Execute()
    {
        if (Email == null) { errorMessage = "Le courriel ne peut pas etre vide."; }
        if (Email == null && Password == null) { errorMessage = "Le mot de passe et le courriel ne peuvent pas etre vide."; }
        if (securePassword == null || securePassword.Length == 0)
        {
            errorMessage = "Le mot de passe ne peut pas etre vide.";
        }


        if (Email != null && securePassword != null)
        {
            errorMessage = "";

            User? user = context.Login(Email, ConvertToUnsecureString(securePassword));

            if (user == null)
            {
                errorMessage = "Le mot de passe ou le courriel sont incorrect";
            }
            else
            {
                MainWindow main = (MainWindow)Application.Current.MainWindow;
                if (rememberMe)
                {
                    SaveCredentials(user);
                }
                main.NavigationController.NavigateHome();
            }
        }


        OnPropertyChanged(nameof(ErrorMessage));
    }
    //Converti le mots de passe une string
    private static string ConvertToUnsecureString(SecureString securePassword)
    {
        IntPtr unmanagedString = IntPtr.Zero;
        try
        {
            unmanagedString = Marshal.SecureStringToGlobalAllocUnicode(securePassword);
            return Marshal.PtrToStringUni(unmanagedString);
        }
        finally
        {
            Marshal.ZeroFreeGlobalAllocUnicode(unmanagedString);
        }
    }

    private bool rememberMe = false;
    public bool RememberMe
    {
        get { return rememberMe; }
        set { rememberMe = value; }
    }
    /// <summary>
    /// Enregistre les informations d'identification de l'utilisateur dans la base de données.
    /// Crée un nouveau jeton de session avec un identifiant de compte, un jeton unique et une date d'expiration.
    /// </summary>
    private void SaveCredentials(User user)
    {
        SessionToken sessionToken = new()
        {
            UserId = user.Id,
            Token = Guid.NewGuid().ToString(),
            ExpirationDate = DateTime.Now.AddDays(5)
        };

        context.SessionToken.Add(sessionToken);
        context.SaveChanges();

        ((App)Application.Current).SavedSettings["SessionToken"] = sessionToken.Token;
    }
}
