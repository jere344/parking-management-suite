namespace wisecorp.Models.DBModels;

public class TicketPayment : BaseModel
{
    public decimal PaymentAmountTotal { get; set; }
    public decimal PaymentAmountOfTax { get; set; }
    public decimal PaymentAmountBeforeTax { get; set; }
    public string PaymentMethod { get; set; }
    public int? CodeUsedId { get; set; }
    public decimal? CodeUsedReduction { get; set; }
    public int? SubscriptionId { get; set; }

    // Navigation properties
    public Code CodeUsed { get; set; }
    public Subscription Subscription { get; set; }
}
