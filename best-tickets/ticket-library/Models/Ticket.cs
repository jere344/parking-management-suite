using System.ComponentModel.DataAnnotations.Schema;

namespace ticketlibrary.Models;

public class Ticket : BaseModel
{ 
    public int HospitalId { get; set; }
    public DateTime CreationTime { get; set; }
    public DateTime? PaymentTime { get; set; }
    public DateTime? DepartureTime { get; set; }
    public int? TicketPaymentId { get; set; }

    // Navigation properties
    public virtual Hospital Hospital { get; set; }
    public virtual TicketPayment? TicketPayment { get; set; }

    public string TicketNumber { 
        get {
            // generate a non reversible hash of the ticket in the format of "XXXX-XXXX"
            // We use the Id as well as the CreationTime to avoid a user trying out different id to get ticket numbers
            string fullhash = $"{Id}{CreationTime}".GetHashCode().ToString();
            return $"{fullhash.Substring(0, 4)}-{fullhash.Substring(4, 4)}".ToUpper();
        }
    }
}
