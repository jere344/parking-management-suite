namespace admintickets.Models.DBModels;

public class Subscription : BaseModel
{
    public DateTime DateStart { get; set; }
    public DateTime DateEnd { get; set; }
    public int MaxNumberOfUsesPerDay { get; set; }

    // Navigation properties
    public ICollection<TicketPayment> TicketPayments { get; set; }
}
