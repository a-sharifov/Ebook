﻿namespace Api;

/// <summary>
/// Environment variables.
/// </summary>
internal static class Env
{
    public static string USER_GRPC_URL => GetEnvironmentVariable("USER_GRPC_URL");

    // Rabbit MQ
    public static string RABBITMQ_DEFAULT_USER => GetEnvironmentVariable("RABBITMQ_DEFAULT_USER");
    public static string RABBITMQ_DEFAULT_PASS => GetEnvironmentVariable("RABBITMQ_DEFAULT_PASS");

    public static class ConnectionString
    {
        public static string RABBITMQ =>
            $"amqp://{RABBITMQ_DEFAULT_USER}:{RABBITMQ_DEFAULT_PASS}@rabbitmq:5672";
    }

    private static string GetEnvironmentVariable(string key) =>
    Environment.GetEnvironmentVariable(key) ??
   throw new Exception($"Environment variable {key} not found");
}