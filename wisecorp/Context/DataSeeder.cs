using wisecorp.Models.DBModels;
using wisecorp.Helpers;

namespace wisecorp.Context;

public class DataSeeder
{
    /// <summary>
    /// Initialise la base de donn�es avec des donn�es de d�part
    /// </summary>
    /// <param name="context">Le contexte de la base de donn�es</param>
    public static void Seed(WisecorpContext context)
    {
        SeedAccounts(context);
    }
    /// <summary>
    /// Ajoute les comptes par d�faut � la base de donn�es
    /// </summary>
    /// <param name="context">Le contexte de la base de donn�es</param>
    private static void SeedAccounts(WisecorpContext context)
    {
        if (!context.Accounts.Any())
        {
            context.Accounts.Add(new Account
            {
                Email = "admin",
                Password = CryptographyHelper.HashPassword("admin"),
                FullName = "Admin",
                IsEnabled = true,
                EmploymentDate = DateTime.Now,
                Picture = "",
                Phone = "123456789",
            }
            );
        }
    }

}