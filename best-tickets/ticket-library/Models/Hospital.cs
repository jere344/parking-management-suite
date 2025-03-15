using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;
using System.IO;
using System.Windows.Media.Imaging;

namespace ticketlibrary.Models;

public class Hospital : BaseModel, INotifyPropertyChanged
{
    public string Name { get; set; }
    public string Address { get; set; }
    // Logo is a base64 string
    public string Logo { get; set; }

    // Password for authenticating the ticket machine (stored as a hash)
    public string Password { get; set; }

    public BitmapImage? HospitalLogo
    {
        get
        {
            if (string.IsNullOrEmpty(Logo))
            {
                return null;
            }
            var image = new BitmapImage();
            image.BeginInit();
            image.StreamSource = new MemoryStream(Convert.FromBase64String(Logo));
            image.CacheOption = BitmapCacheOption.OnLoad;
            image.EndInit();
            image.Freeze();
            return image;
        }
    }
    
    [NotMapped]
    private string _tempPassword;

    [NotMapped]
    public string TempPassword
    {
        get => _tempPassword ?? string.Empty;
        set 
        {
            if (_tempPassword != value)
            {
                _tempPassword = value;
                OnPropertyChanged(nameof(TempPassword));
            }
        }
    }

    // Add INotifyPropertyChanged implementation if not already in BaseModel
    #region INotifyPropertyChanged Implementation
    public event PropertyChangedEventHandler PropertyChanged;

    protected void OnPropertyChanged(string propertyName)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
    #endregion

    // Navigation Properties
    public virtual ICollection<Ticket> Tickets { get; set; }
    public virtual ICollection<Signal> Signals { get; set; }

    public virtual ICollection<PriceBracket> PriceBrackets { get; set; }
    public virtual ICollection<Code> Codes { get; set; }
    public virtual ICollection<SubscriptionTier> SubscriptionTiers { get; set; }
}
