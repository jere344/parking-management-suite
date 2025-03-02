using System.ComponentModel.DataAnnotations.Schema;

namespace ticketlibrary.Models;

public class PriceBracket : BaseModel
{ 
    public string InternalMinDuration { get; set; }
    [NotMapped]
    public TimeSpan MinDuration
    {
        get
        {
            if (TimeSpan.TryParse(InternalMinDuration, out TimeSpan duration))
            {
                return duration;
            }
            return TimeSpan.Zero;
        }
        set
        {
            InternalMinDuration = value.ToString();
        }
    }
    public decimal Price { get; set; }
    public int? HospitalId { get; set; } // no hospital means it's a global price bracket

    // Navigation properties
    public virtual Hospital? Hospital { get; set; }
}
