namespace Api;

public static class Env
{
    //postgres
    public static string POSTGRE_CONNECTION_STRING => GetEnvironmentVariable("POSTGRE_CONNECTION_STRING");

    //redis
    public static string REDIS_CONNECTION_STRING => GetEnvironmentVariable("REDIS_CONNECTION_STRING");

    //security
    public static string AUTH_ISSUER => GetEnvironmentVariable("AUTH_ISSUER");
    public static string WEB_AUDIENCE => GetEnvironmentVariable("WEB_AUDIENCE");
    public static string JWT_SECURITY_KEY => GetEnvironmentVariable("JWT_SECURITY_KEY");
    public static string IDENTITY_ENDPOINT => GetEnvironmentVariable("IDENTITY_ENDPOINT");

    // Rabbit MQ
    public static string RABBITMQ_DEFAULT_USER => GetEnvironmentVariable("RABBITMQ_DEFAULT_USER");
    public static string RABBITMQ_DEFAULT_PASS => GetEnvironmentVariable("RABBITMQ_DEFAULT_PASS");

    private static string GetEnvironmentVariable(string key) =>
        Environment.GetEnvironmentVariable(key) ??
       throw new Exception($"Environment variable {key} not found");
}
