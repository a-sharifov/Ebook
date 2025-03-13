namespace Infrastructure.Jwt.Options;

public class JwtOptions
{
    public string Issuer { get; set; } = null!;
    public string[] Audiences { get; set; } = null!;
    public string Key { get; set; } = null!;
    public int TokenExpirationTimeMinutes { get; set; }
    public int RefreshTokenExpirationTimeMinutes { get; set; }
}
