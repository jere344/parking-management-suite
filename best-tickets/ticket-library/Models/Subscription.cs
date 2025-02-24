namespace ticketlibrary.Models;

public class Subscription : BaseModel
{
    public DateTime DateStart { get; set; }
    public DateTime DateEnd { get; set; }
    public int MaxNumberOfUsesPerDay { get; set; }

    // Navigation properties
    public virtual ICollection<TicketPayment> TicketPayments { get; set; }
}
