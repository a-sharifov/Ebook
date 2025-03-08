using Api.Core.ServiceInstaller.Interfaces;
using MassTransit;
using MessageBus.Users.Handlers.Commands;

namespace Api.Configurations;

internal sealed class MessageBusServiceInstaller : IServiceInstaller
{
    public void Install(IServiceCollection services, IConfiguration configuration) =>
        services.AddMassTransit(configure =>
        {
            configure.SetKebabCaseEndpointNameFormatter();
            configure.UsingRabbitMq((context, configurator) =>
            {
                configurator.Host("rabbitmq", "/", hostConfigurator =>
                {
                    hostConfigurator.Username(Env.RABBITMQ_DEFAULT_USER);
                    hostConfigurator.Password(Env.RABBITMQ_DEFAULT_PASS);
                });

                configurator.ReceiveEndpoint("user-created-confirmation-email-send-command", x =>
                {
                    x.ConfigureConsumer<UserCreatedConfirmationEmailSendCommandHandler>(context);
                });


                configurator.ConfigureEndpoints(context);
            });

            configure.AddConsumers(MessageBus.AssemblyReference.Assembly);
        });
}