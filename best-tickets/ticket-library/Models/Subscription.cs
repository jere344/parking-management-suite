using System.ComponentModel.DataAnnotations.Schema;

namespace ticketlibrary.Models;

public class Subscription : BaseModel
{
    public DateTime DateStart { get; set; }
    public DateTime DateEnd { get; set; }
    public int MaxNumberOfUsesPerDay { get; set; }
    public int HospitalId { get; set; }
    public string CardNumber { get; set; }
    public decimal PricePaid { get; set; }

    // Navigation properties
    public virtual ICollection<TicketPayment> TicketPayments { get; set; }
    public virtual Hospital Hospital { get; set; }
}
