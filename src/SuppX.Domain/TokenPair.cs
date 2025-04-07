namespace SuppX.Domain;

public class TokenPair
{
    public required string AccessToken { get; set; }
    public required string RefreshToken { get; set; }
}
