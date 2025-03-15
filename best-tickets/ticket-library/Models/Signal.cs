using System.ComponentModel.DataAnnotations.Schema;

namespace ticketlibrary.Models;

public class Signal : BaseModel
{ 
    public int HospitalId { get; set; }
    public string SignalType { get; set; }
    public DateTime EndTime { get; set; }
    
    // Navigation properties
    public virtual Hospital Hospital { get; set; }

    public const string OpenEntryGate = "OpenEntryGate";
    public const string OpenExitGate = "OpenExitGate";
    
    [NotMapped]
    public bool IsActive => EndTime > DateTime.Now;
    
    [NotMapped]
    public TimeSpan RemainingTime => 
        IsActive ? EndTime - DateTime.Now : TimeSpan.Zero;
}
