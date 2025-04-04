using ticketlibrary.Models;
using ticketlibrary.Helpers;

namespace paymentterminal.Context;

public class DataSeeder
{
    /// <summary>
    /// Initialise la base de donn�es avec des donn�es de d�part
    /// </summary>
    /// <param name="context">Le contexte de la base de donn�es</param>
    public static void Seed(BestTicketContext context)
    {
        SeedAccounts(context);
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

}