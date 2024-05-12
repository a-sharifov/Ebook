using Api.Core.ServiceInstaller.Interfaces;
using Api.Core.Pipelines;
using FluentValidation;
using Application.Core.MappingConfig;
using Infrastructure.Idempotence;
using MediatR;

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

        services.Decorate(typeof(INotificationHandler<>), typeof(IdempotentDomainEventHandler<>));

        services.AddValidatorsFromAssembly(
            Application.AssemblyReference.Assembly,
            includeInternalTypes: true);

        services.MappingConfigsFromAssembly(
            Application.AssemblyReference.Assembly);
    }
}
