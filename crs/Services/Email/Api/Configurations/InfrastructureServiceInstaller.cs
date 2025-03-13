using Api.Core.ServiceInstaller.Interfaces;
using EventBus.MassTransit.Abstractions;
using EventBus.MassTransit.RabbitMQ.Services;
using Infrasctructure.Grpc.Users;
using Infrastructure.Emails.Interfaces;
using Infrastructure.Emails.Options;
using Infrastructure.Emails.Services;
using Infrastructure.Endpoint.Options;
using Users.Protobuf;

namespace Api.Configurations;

internal sealed class InfrastructureServiceInstaller : IServiceInstaller
{
    public void Install(IServiceCollection services, IConfiguration configuration)
    {
        services.AddTransient<IIdentityEmailService, IdentityEmailService>();
        services.AddTransient<IMessageBus, EventBusRabbitMQ>();


        services.AddGrpcClient<UserService.UserServiceClient>(x => 
            x.Address = new Uri(Env.USER_GRPC_URL));

        services.AddTransient<IUserGrpcService, UserGrpcService>();

        services.AddOptions<EmailOptions>()
        .Bind(configuration.GetSection(SD.EmailSectionKey))
        .ValidateDataAnnotations();
    }
}
