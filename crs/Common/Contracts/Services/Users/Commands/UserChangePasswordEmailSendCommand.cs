using EventBus.Common.Messages;

namespace Contracts.Services.Users.Commands;

public sealed record UserChangePasswordEmailSendCommand(
    Guid Id,
    Guid UserId,
    string ReturnUrl) : IntegrationCommand(Id);