namespace wisecorp.Models.DBModels;

public class SecurityLog : BaseModel
{ 
    public string Code { get; set; }
    public string Description { get; set; }
    public string Ip { get; set; }
    public int? AccountId { get; set; }
    public DateTime Date { get; set; }

    public bool HasMoreInfo => Description.Contains('\n');

    //Nav propreties
    public virtual Account Account { get; set; }

    public const string LoginSuccess = "LoginSuccess";
    public const string LoginFailed = "LoginFailed";
    public const string SendSecurityCode = "SendSecurityCode";

    public const string AddAccount = "AddAccount";
    public const string EditAccount = "EditAccount";
    public const string DeleteAccount = "DeactivatedAccount";

}