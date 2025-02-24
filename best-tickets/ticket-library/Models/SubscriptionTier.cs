using System.ComponentModel.DataAnnotations.Schema;
using System.Windows;

namespace ticketlibrary.Models;

public class SubscriptionTiers : BaseModel
{
    public string Name { get; set; }
    public int MaxNumberOfUsesPerDay { get; set; }
    public decimal Price { get; set; }
    public int? HospitalId { get; set; } // If null, it means it is a global subscription tier


    // public TimeSpan Duration { get; set; }
    // thing is, Timespan get converted to mysql Time which has a max value of 838:59:59
    // so Duration will be a wrapper and we will store it as a string in the database
    public string InternalDuration { get; set; }
    [NotMapped]
    public TimeSpan Duration
    {
        get
        {
            if (TimeSpan.TryParse(InternalDuration, out TimeSpan duration))
            {
                return duration;
            }
            return TimeSpan.Zero;
        }
        set
        {
            InternalDuration = value.ToString();
        }
    }

    // Navigation Properties
    public virtual Hospital? Hospital { get; set; }

    // computed properties
    public string DurationString => Duration.Days.ToString() + " " + (string)Application.Current.FindResource("days");
}
