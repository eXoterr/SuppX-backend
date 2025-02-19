namespace SuppX.Domain;

public class User
{
    public int Id { get; set; }
    public Role Role { get; set; }
    public int RoleId { get; set; }
    public string? Login { get; set; }
    public string? Password { get; set; }
    public DateTime BannedAt { get; set; }
    public BanReason BanReason { get; set; }
}
