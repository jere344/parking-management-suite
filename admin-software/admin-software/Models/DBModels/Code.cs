namespace admintickets.Models.DBModels;

public class Code : BaseModel
{
    public decimal Reduction { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public bool IsActive { get; set; }

    // Navigation properties
    public ICollection<TicketPayment> TicketPayments { get; set; }
}
