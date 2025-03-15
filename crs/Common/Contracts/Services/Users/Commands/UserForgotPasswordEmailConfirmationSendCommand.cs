using EventBus.Common.Messages;

namespace Contracts.Services.Users.Commands;

public sealed record UserForgotPasswordEmailConfirmationSendCommand(
    Guid Id,
    Guid UserId,
    string ReturnUrl) : IntegrationCommand(Id);
