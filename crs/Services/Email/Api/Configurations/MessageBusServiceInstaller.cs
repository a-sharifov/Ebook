using Api.Core.ServiceInstaller.Interfaces;
using Contracts.Extensions;
using Contracts.Services.Users.Commands;
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

                configurator.ReceiveEndpoint(
                    nameof(UserCreatedConfirmationEmailSendCommand).ToKebabCase(), x =>
                {
                    x.ConfigureConsumer<UserCreatedConfirmationEmailSendCommandHandler>(context);
                });

                configurator.ReceiveEndpoint(
                    nameof(UserRetryEmailConfirmationSendCommand).ToKebabCase(), x =>
                {
                    x.ConfigureConsumer<UserRetryEmailConfirmationSendCommandHandler>(context);
                });

                //configurator.ReceiveEndpoint("user-retry-email-confirmation-send-command", x =>
                //{
                //    x.ConfigureConsumer<UserConfirmItemMoveInCartToWishListCommandHandler>(context);
                //});


                configurator.ConfigureEndpoints(context);
            });

            configure.AddConsumers(MessageBus.AssemblyReference.Assembly);
        });
}