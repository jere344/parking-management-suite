using System.IO;
using System.Windows.Media.Imaging;

namespace wisecorp.Models.DBModels;

public class Account : BaseModel
{
    public int RoleId { get; set; }
    public string Email { get; set; } // we login with email
    public string Password { get; set; }
    public DateTime EmploymentDate { get; set; }
    public string FullName { get; set; }
    public string Phone { get; set; }

    // base64
    public string Picture { get; set; }


    /// <summary>
    /// Obtient l'image de profil de l'utilisateur
    /// </summary>
    public BitmapImage ProfilePicture
    { 
        get {
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
