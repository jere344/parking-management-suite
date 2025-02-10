namespace wisecorp.Models.DBModels;

public class Signal : BaseModel
{ 
    public int HospitalId { get; set; }
    public string SignalType { get; set; }
    public string Value { get; set; }
    public DateTime SignalDate { get; set; }
    
    // Navigation properties
    public Hospital Hospital { get; set; }

    public const string OpenPortal = "OpenPortal";
}
