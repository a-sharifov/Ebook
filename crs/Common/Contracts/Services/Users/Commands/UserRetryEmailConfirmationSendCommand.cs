using EventBus.Common.Messages;

namespace Contracts.Services.Users.Commands;

public sealed record UserRetryEmailConfirmationSendCommand(
    Guid Id,
    Guid UserId,
    string ReturnUrl) : IntegrationCommand(Id);