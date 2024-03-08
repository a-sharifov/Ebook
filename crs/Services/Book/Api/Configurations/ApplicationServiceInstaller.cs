using Api.Core.ServiceInstaller.Interfaces;
using Api.Core.Pipelines;
using FluentValidation;

namespace Api.Configurations;

internal sealed class ApplicationServiceInstaller : IServiceInstaller
{
    public void Install(IServiceCollection services, IConfiguration configuration)
    {
        services.AddMediatR(configuration => configuration
        .RegisterServicesFromAssembly(Application.AssemblyReference.Assembly)
        .AddOpenBehavior(typeof(LoggingPipelineBehavior<,>))
        .AddOpenBehavior(typeof(ValidationPipelineBehavior<,>))
        );

        services.AddValidatorsFromAssembly(
            Application.AssemblyReference.Assembly,
            includeInternalTypes: true);
    }
}
