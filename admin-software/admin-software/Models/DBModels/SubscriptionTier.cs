namespace admintickets.Models.DBModels;

public class SubscriptionTiers : BaseModel
{
    public string Name { get; set; }
    public DateTime Duration { get; set; }
    public int MaxNumberOfUsesPerDay { get; set; }
    public decimal Price { get; set; }
}
