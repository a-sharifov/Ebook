namespace Monitoring.Api;

public static class Env
{
    public static string POSTGRES_CONNECTION_STRING => GetEnvironmentVariable("POSTGRES_CONNECTION_STRING");
    public static string POSTGRES_DB => GetEnvironmentVariable("POSTGRES_DB");
    public static string REDIS_CONNECTION_STRING => GetEnvironmentVariable("REDIS_CONNECTION_STRING");

    private static string GetEnvironmentVariable(string key) =>
        Environment.GetEnvironmentVariable(key) ??
       throw new Exception($"Environment variable {key} not found");
}
