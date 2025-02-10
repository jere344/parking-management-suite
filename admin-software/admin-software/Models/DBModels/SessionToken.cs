namespace wisecorp.Models.DBModels;

public class SessionToken : BaseModel
{ 
    public int UserId { get; set; }
    public string Token { get; set; }
    public DateTime CreationDate { get; set; }
    public DateTime ExpirationDate { get; set; }

    // Navigation properties
    public virtual User User { get; set; }
}
