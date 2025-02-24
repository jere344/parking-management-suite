namespace admintickets.Models.DBModels;

public class Taxes : BaseModel
{
    public decimal Amount { get; set; } //15% = 0.15
    public string Name { get; set; }
    public string Description { get; set; }
}
