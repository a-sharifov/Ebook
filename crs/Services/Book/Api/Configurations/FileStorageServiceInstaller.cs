using Amazon.Runtime;
using Amazon.S3;
using Api.Core.ServiceInstaller.Interfaces;

namespace Api.Configurations;

internal sealed class FileStorageServiceInstaller : IServiceInstaller
{ 
    public void Install(IServiceCollection services, IConfiguration configuration)
    {
        var awsOption = configuration.GetAWSOptions();
        awsOption.Credentials = new BasicAWSCredentials(
                Env.SERVER_ACCESS_KEY, 
                Env.SERVER_SECRET_KEY);

        services.AddDefaultAWSOptions(awsOption);

        services.AddAWSService<IAmazonS3>();
    }
}
