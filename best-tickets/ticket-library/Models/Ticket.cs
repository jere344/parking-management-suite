using System.ComponentModel.DataAnnotations.Schema;

namespace ticketlibrary.Models;

public class Ticket : BaseModel
{ 
    public int HospitalId { get; set; }
    public DateTime CreationTime { get; set; }
    public DateTime? PaymentTime { get; set; }
    public DateTime? DepartureTime { get; set; }
    public int? TicketPaymentId { get; set; }
    public string TicketNumber { get; set; }

    // Navigation properties
    public virtual Hospital Hospital { get; set; }
    public virtual TicketPayment? TicketPayment { get; set; }

}
