using System.ComponentModel.DataAnnotations.Schema;

namespace admintickets.Models.DBModels;

public class PriceBrackets : BaseModel
{ 
    public string InternalMaxDuration { get; set; }
    [NotMapped]
    public TimeSpan MaxDuration
    {
        get
        {
            if (TimeSpan.TryParse(InternalMaxDuration, out TimeSpan duration))
            {
                return duration;
            }
            return TimeSpan.Zero;
        }
        set
        {
            InternalMaxDuration = value.ToString();
        }
    }
    public decimal Price { get; set; }
}
