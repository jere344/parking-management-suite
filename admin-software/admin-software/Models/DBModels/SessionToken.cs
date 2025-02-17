namespace admintickets.Models.DBModels;

public class SessionToken : BaseModel
{ 
    public int UserId { get; set; }
    public string Token { get; set; }
    public DateTime CreationDate { get; set; } = DateTime.Now;
    public DateTime ExpirationDate { get; set; }

    // Navigation properties
    public virtual User User { get; set; }
}
