namespace wisecorp.Models.DBModels;

public class TicketPayment : BaseModel
{ 
    public decimal AmountAfterTax { get; set; }
    public decimal AmountOfTax { get; set; }
    public decimal AmountBeforeTax { get; set; }
    public string PaymentMethod { get; set; }
    public string? CodeUsed { get; set; }
    public decimal CodeUsedReduction { get; set; }
    public int? SubscriptionId { get; set; }

    // Navigation properties
    public Subscription? Subscription { get; set; }

}
