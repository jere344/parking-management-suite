using System.ComponentModel.DataAnnotations.Schema;

namespace ticketlibrary.Models;

public class Subscription : BaseModel
{
    public DateTime DateStart { get; set; }
    public DateTime DateEnd { get; set; }
    public int MaxNumberOfUsesPerDay { get; set; }

    // Navigation properties
    public virtual ICollection<TicketPayment> TicketPayments { get; set; }

    public string CardNumber { 
        get {
            // generate a non reversible hash of the card in the format of "XXXX-XXXX"
            // We use the Id as well as the DateStart to avoid a user trying out different id to get card numbers
            string fullhash = $"{Id}{DateStart}".GetHashCode().ToString();
            return $"{fullhash.Substring(0, 4)}-{fullhash.Substring(4, 4)}".ToUpper();
        }
    }
}
