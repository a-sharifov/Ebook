using EventBus.Common.Messages;

namespace Contracts.Services.Users.Commands;

public sealed record UserCreatedConfirmationEmailSendCommand(
    Guid Id,
    Guid UserId,
    string ReturnUrl) : IntegrationCommand(Id);