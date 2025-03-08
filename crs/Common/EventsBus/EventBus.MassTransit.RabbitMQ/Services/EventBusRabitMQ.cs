﻿using System.Runtime.Loader;
using System.Text.RegularExpressions;

namespace EventBus.MassTransit.RabbitMQ.Services;

public sealed class EventBusRabbitMQ(IBusControl busControl) : IMessageBus
{
    private readonly IBusControl _busControl = busControl;

    public async Task Publish<TEvent>
        (TEvent @event, CancellationToken cancellationToken = default)
        where TEvent : IIntegrationEvent => 
        await _busControl.Publish(@event, cancellationToken);

    public async Task Send<TCommand>
        (TCommand command, CancellationToken cancellationToken = default)
        where TCommand : IIntegrationCommand => 
        await _busControl.Send(command, cancellationToken);

    public async Task<ISendEndpoint> GetSendEndpoint(Uri address) => 
        await _busControl.GetSendEndpoint(address);

    public async Task<ISendEndpoint> GetSendEndpoint<TEvent>() 
        where TEvent : IIntegrationCommand
    {
        var address = Regex.Replace(
            typeof(TEvent).Name,
          "(?<!^)([A-Z][a-z]|(?<=[a-z])[A-Z0-9])",
          "-$1",
          RegexOptions.Compiled)
          .Trim()
          .ToLower();

        var endpoint = await GetSendEndpoint(new Uri("rabbitmq://rabbitmq/"+address));

        return endpoint;
    }
}
