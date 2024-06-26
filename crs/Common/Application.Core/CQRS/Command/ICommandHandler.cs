﻿namespace Application.Core.CQRS.Command;

/// <summary>
/// Interface for command handlers - CQRS.
/// </summary>
/// <typeparam name="TCommand"> The command type.</typeparam>
public interface ICommandHandler<TCommand> : IRequestHandler<TCommand, Result>
    where TCommand : ICommand
{
}

/// <summary>
/// Interface for command handlers with a response - CQRS.
/// </summary>
/// <typeparam name="TCommand"> The command type.</typeparam>
/// <typeparam name="TResponse"> The response type.</typeparam>
public interface ICommandHandler<TCommand, TResponse> : IRequestHandler<TCommand, Result<TResponse>>
    where TCommand : ICommand<TResponse>
{
}