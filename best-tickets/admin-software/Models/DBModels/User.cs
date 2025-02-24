using System.IO;
using System.Windows.Media.Imaging;

namespace admintickets.Models.DBModels;


public class User : BaseModel
{
    public string Email { get; set; } // we login with email
    public string Password { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public DateTime AccountCreationDate { get; set; }
    public DateTime? AccountDisableDate { get; set; }
    public string Phone { get; set; }


    // Picture is a base64 string
    public string Picture { get; set; }

    // Navigation properties
    public virtual ICollection<SessionToken> SessionTokens { get; set; }

    // Computed properties
    [System.ComponentModel.DataAnnotations.Schema.NotMapped]
    public bool IsDisabled { 
        get => AccountDisableDate != null;
        set => AccountDisableDate = (value == true) ? DateTime.Now : null;
    }
    public string FullName => $"{FirstName} {LastName}";
    public BitmapImage ProfilePicture
    {
        get
        {
            if (string.IsNullOrEmpty(Picture))
            {
                return new BitmapImage(new Uri("pack://application:,,,/Resources/DefaultProfilePicture.webp"));
            }
            var image = new BitmapImage();
            image.BeginInit();
            image.StreamSource = new MemoryStream(Convert.FromBase64String(Picture));
            image.CacheOption = BitmapCacheOption.OnLoad;
            image.EndInit();
            image.Freeze();
            return image;
        }
    }
}
