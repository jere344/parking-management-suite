using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System.IO;
using System.Windows;
using ticketlibrary.Models;
using ticketlibrary.Helpers;
using System.Diagnostics;
using System.Collections.Concurrent;

namespace admintickets.Context;

public class BestTicketContext : DbContext
{
    // Static collection to track all active contexts
    private static ConcurrentDictionary<int, BestTicketContext> _activeContexts = new ConcurrentDictionary<int, BestTicketContext>();
    private static int _lastContextId = 0;
    private int _contextId;

    public DbSet<User> User { get; set; }
    public DbSet<SessionToken> SessionToken { get; set; }
    public DbSet<Hospital> Hospital { get; set; }
    public DbSet<Code> DiscountCode { get; set; }
    public DbSet<SubscriptionTier> SubscriptionTier { get; set; }
    public DbSet<Ticket> Ticket { get; set; }
    public DbSet<TicketPayment> TicketPayment { get; set; }
    public DbSet<Taxes> Taxe { get; set; }
    public DbSet<PriceBracket> PriceBracket { get; set; }
    public DbSet<Subscription> Subscription { get; set; }
    public DbSet<Signal> Signal { get; set; }

    private bool isDebug = false;

    [Conditional("DEBUG")]
    private void IsDebugCheck(ref bool isDebug)
    {
        isDebug = true;
    }

    public BestTicketContext()
    {
        _contextId = Interlocked.Increment(ref _lastContextId);
        _activeContexts.TryAdd(_contextId, this);
    }
    
    public BestTicketContext(DbContextOptions<BestTicketContext> options): base(options)
    {
        _contextId = Interlocked.Increment(ref _lastContextId);
        _activeContexts.TryAdd(_contextId, this);
    }

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
    /// Configures the model and entity relationships
    /// </summary>
    /// <param name="modelBuilder">The builder being used to construct the model for this context</param>
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        // Configure Hospital cascading delete relationships
        modelBuilder.Entity<Hospital>()
            .HasMany(h => h.Tickets)
            .WithOne(t => t.Hospital)
            .HasForeignKey(t => t.HospitalId)
            .OnDelete(DeleteBehavior.Cascade);

        modelBuilder.Entity<Hospital>()
            .HasMany(h => h.Subscriptions)
            .WithOne(s => s.Hospital)
            .HasForeignKey(s => s.HospitalId)
            .OnDelete(DeleteBehavior.Cascade);

        modelBuilder.Entity<Hospital>()
            .HasMany(h => h.Codes)
            .WithOne(c => c.Hospital)
            .HasForeignKey(c => c.HospitalId)
            .OnDelete(DeleteBehavior.Cascade);

        // Configure User relationships
        modelBuilder.Entity<User>()
            .HasMany(u => u.SessionTokens)
            .WithOne(st => st.User)
            .HasForeignKey(st => st.UserId)
            .OnDelete(DeleteBehavior.Cascade);


        // Configure TicketPayment relationships
        modelBuilder.Entity<Code>()
            .HasMany(c => c.TicketPayments)
            .WithOne(tp => tp.CodeUsed)
            .HasForeignKey(tp => tp.CodeUsedId)
            .OnDelete(DeleteBehavior.SetNull);

        modelBuilder.Entity<Subscription>()
            .HasMany(s => s.TicketPayments)
            .WithOne(tp => tp.Subscription)
            .HasForeignKey(tp => tp.SubscriptionId)
            .OnDelete(DeleteBehavior.SetNull);

        // Configure other Hospital relationships
        modelBuilder.Entity<Hospital>()
            .HasMany(h => h.Signals)
            .WithOne(s => s.Hospital)
            .HasForeignKey(s => s.HospitalId)
            .OnDelete(DeleteBehavior.Cascade);

        modelBuilder.Entity<Hospital>()
            .HasMany(h => h.PriceBrackets)
            .WithOne(pb => pb.Hospital)
            .HasForeignKey(pb => pb.HospitalId)
            .OnDelete(DeleteBehavior.Cascade);

        modelBuilder.Entity<Hospital>()
            .HasMany(h => h.SubscriptionTiers)
            .WithOne(st => st.Hospital)
            .HasForeignKey(st => st.HospitalId)
            .OnDelete(DeleteBehavior.Cascade);
    }

    /// <summary>
    /// Disposes all active database contexts to prevent concurrent access issues
    /// </summary>
    public static void DisposeActiveContexts()
    {
        foreach (var contextEntry in _activeContexts.ToList())
        {
            if (contextEntry.Value != null)
            {
                try
                {
                    contextEntry.Value.Dispose();
                }
                catch { /* Ignore any errors during disposal */ }
                
                _activeContexts.TryRemove(contextEntry.Key, out _);
            }
        }
    }

    protected void Dispose(bool disposing)
    {
        if (disposing)
        {
            _activeContexts.TryRemove(_contextId, out _);
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

