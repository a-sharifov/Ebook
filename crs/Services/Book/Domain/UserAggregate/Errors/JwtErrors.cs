namespace Domain.UserAggregate.Errors;

public static class JwtErrors
{
    public static Error JwtInBlackList =>
      new("Jwt.JwtInBlackList", "Jwt in black list.");
}
