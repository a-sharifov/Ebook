using Api.Core.ServiceInstaller.Interfaces;

namespace Api.Configurations;

internal sealed class HealthCheckServiceInstaller : IServiceInstaller
{
    public void Install(IServiceCollection services, IConfiguration configuration) =>
        services
        .AddHealthChecks()
        .AddSqlServer(
            connectionString: Env.POSTGRE_CONNECTION_STRING,
            name: "BookDb")
        .AddRedis(
            redisConnectionString: Env.REDIS_CONNECTION_STRING,
            name: "BookCaching");
}
