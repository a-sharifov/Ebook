using Api.Core.ServiceInstaller.Interfaces;
using Minio;

namespace Api.Configurations;

internal sealed class FileStorageServiceInstaller : IServiceInstaller
{
    public void Install(IServiceCollection services, IConfiguration configuration) => 
        services.AddMinio(configureClient => configureClient
        .WithEndpoint(Env.SERVER_ENDPOINT, 9000)
        .WithCredentials(Env.SERVER_ACCESS_KEY, Env.SERVER_SECRET_KEY)
        .WithSSL(false));
}
