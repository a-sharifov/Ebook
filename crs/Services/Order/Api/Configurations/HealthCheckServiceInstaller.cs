using Api.Core.ServiceInstaller.Interfaces;

namespace Api.Configurations;

internal sealed class HealthCheckServiceInstaller : IServiceInstaller
{
    public void Install(IServiceCollection services, IConfiguration configuration) => 
        services.AddHealthChecks()
        .AddNpgSql(Env.POSTGRE_CONNECTION_STRING)
        .AddRedis(Env.REDIS_CONNECTION_STRING);
}
