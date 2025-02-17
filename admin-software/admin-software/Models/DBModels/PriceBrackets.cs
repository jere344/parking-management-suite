namespace admintickets.Models.DBModels;

public class PriceBrackets : BaseModel
{ 
    public DateTime MaxDuration { get; set; }
    public decimal Price { get; set; }
}
