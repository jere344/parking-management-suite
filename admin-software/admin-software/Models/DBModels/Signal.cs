namespace admintickets.Models.DBModels;

public class Signal : BaseModel
{ 
    public int HospitalId { get; set; }
    public string SignalType { get; set; }
    public string Value { get; set; }
    public TimeSpan Duration { get; set; }
    
    // Navigation properties
    public virtual Hospital Hospital { get; set; }

    public const string OpenPortal = "OpenPortal";
}
