using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System.IO;
using System.Windows;
using ticketlibrary.Models;
using ticketlibrary.Helpers;
using System.Diagnostics;

namespace paymentterminal.Context;

public class BestTicketContext : DbContext
{
    public DbSet<User> User { get; set; }
    public DbSet<SessionToken> SessionToken { get; set; }
    public DbSet<Hospital> Hospital { get; set; }
    public DbSet<Code> DiscountCode { get; set; }
    public DbSet<SubscriptionTiers> SubscriptionTier { get; set; }
    public DbSet<Ticket> Ticket { get; set; }
    public DbSet<TicketPayment> TicketPayment { get; set; }
    public DbSet<Taxes> Taxe { get; set; }
    public DbSet<PriceBracket> PriceBracket { get; set; }
    public DbSet<Subscription> Subscription { get; set; }


    private bool isDebug = false;

    [Conditional("DEBUG")]
    private void IsDebugCheck(ref bool isDebug)
    {
        isDebug = true;
    }
    public BestTicketContext() { }
    public BestTicketContext(DbContextOptions<BestTicketContext> options): base(options) { }

    /// <summary>
    /// Configure le contexte de la base de donn�es
    /// </summary>
    /// <param name="optionsBuilder">Le constructeur d'options pour configurer le contexte</param>
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        if (!optionsBuilder.IsConfigured)
        {
            var configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json")
                .Build();

            IsDebugCheck(ref isDebug);
            var connectionString = "";

            if (isDebug)
            {
                connectionString = configuration.GetConnectionString("DefaultConnection");
            }
            else
            {
                connectionString = configuration.GetConnectionString("ReleaseConnection");
            }


            optionsBuilder.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString));
            //use lazy loadings
            optionsBuilder.UseLazyLoadingProxies();
        } 
    }

    /// <summary>
    /// Authentifie un utilisateur avec son email et son mot de passe
    /// </summary>
    /// <param name="email">L'email de l'utilisateur</param>
    /// <param name="password">Le mot de passe de l'utilisateur</param>
    /// <returns>Le compte de l'utilisateur s'il est authentifi�, sinon null</returns>
    public User? Login(string email, string password)
    {
        var user = User.FirstOrDefault(a => a.Email == email);
        if (user == null)
        {
            return null;
        }
        if (CryptographyHelper.VerifyPassword(password, user.Password))
        {
            App.Current.ConnectedUser = user;
            return user;
        }
        return null;
    }
}

