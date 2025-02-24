namespace ticketlibrary.Models;

public class Code : BaseModel
{
    public decimal Reduction { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public bool IsActive { get; set; }
    public int? HospitalId { get; set; } // If not null, this code is only valid for this hospital

    // Navigation properties
    public virtual ICollection<TicketPayment> TicketPayments { get; set; }
    public virtual Hospital? Hospital { get; set; }
}
