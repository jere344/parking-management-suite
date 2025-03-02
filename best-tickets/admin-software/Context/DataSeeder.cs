using ticketlibrary.Models;
using ticketlibrary.Helpers;

namespace admintickets.Context;

public class DataSeeder
{
    /// <summary>
    /// Initialise la base de donn�es avec des donn�es de d�part
    /// </summary>
    /// <param name="context">Le contexte de la base de donn�es</param>
    public static void Seed(BestTicketContext context)
    {
        SeedAccounts(context);
        SeedTaxes(context);
        SeedPriceBrackets(context);
    }
    /// <summary>
    /// Ajoute les comptes par d�faut � la base de donn�es
    /// </summary>
    /// <param name="context">Le contexte de la base de donn�es</param>
    private static void SeedAccounts(BestTicketContext context)
    {
        if (!context.User.Any())
        {
            context.User.Add(new User
            {
                Email = "admin",
                Password = CryptographyHelper.HashPassword("admin"),
                FirstName = "Admin",
                LastName = "Admin",
                AccountCreationDate = DateTime.Now,
                AccountDisableDate = null,
                Picture = "",
                Phone = "123456789",
            }
            );
            context.SaveChanges();
        }
    }

    /// <summary>
    /// Ajoute les taxes par d�faut � la base de donn�es
    /// </summary>
    /// <param name="context">Le contexte de la base de donn�es</param>
    private static void SeedTaxes(BestTicketContext context)
    {
        if (!context.Taxe.Any())
        {
            context.Taxe.Add(new Taxes
            {
                Name = "TVQ",
                Amount = 0.1m,
            });
            context.Taxe.Add(new Taxes
            {
                Name = "TPS",
                Amount = 0.05m,
            });
            context.SaveChanges();
        }
    }

    /// <summary>
    /// Ajoute les tranches de prix par d�faut � la base de donn�es
    /// </summary>
    /// <param name="context">Le contexte de la base de donn�es</param>
    private static void SeedPriceBrackets(BestTicketContext context)
    {
        if (!context.PriceBracket.Any())
        {
            // bellow 1 hour it's free
            context.PriceBracket.Add(new PriceBracket
            {
                MinDuration = TimeSpan.FromHours(1),
                Price = 3,
            });
            context.PriceBracket.Add(new PriceBracket
            {
                MinDuration = TimeSpan.FromHours(2),
                Price = 5,
            });
            context.PriceBracket.Add(new PriceBracket
            {
                MinDuration = TimeSpan.FromHours(6),
                Price = 7,
            });
            context.PriceBracket.Add(new PriceBracket
            {
                MinDuration = TimeSpan.FromHours(12),
                Price = 10,
            });
            context.PriceBracket.Add(new PriceBracket
            {
                MinDuration = TimeSpan.FromHours(24),
                Price = 15,
            });
            // After 24 hours, the price stays the same
            context.SaveChanges();            
        }
    }
}