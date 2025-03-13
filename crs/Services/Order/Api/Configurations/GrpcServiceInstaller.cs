using Api.Core.ServiceInstaller.Interfaces;

namespace Api.Configurations;

internal sealed class GrpcServiceInstaller : IServiceInstaller
{
    public void Install(IServiceCollection services, IConfiguration configuration)
    {
        services.AddGrpc();
    }
}
