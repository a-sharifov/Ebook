using Application.Emails.Commands.SendConfirmationUserMessage;
using Domain.Core.Results;
using Infrasctructure.Grpc.Users;
using Infrastructure.Emails.Interfaces;
using Infrastructure.Emails.Models;

namespace Application.Emails.Commands.SendChangePasswordMessage;

public sealed record SendChangePasswordMessageCommand(
    Guid UserId,
    string ReturnUrl) : ICommand;

internal sealed class SendChangePasswordMessageCommandHandler(
    IIdentityEmailService emailService,
    IUserGrpcService userGrpcService)
    : ICommandHandler<SendChangePasswordMessageCommand>
{
    private readonly IIdentityEmailService _emailService = emailService;
    private readonly IUserGrpcService _userGrpcService = userGrpcService;

    public async Task<Result> Handle(SendChangePasswordMessageCommand request, CancellationToken cancellationToken)
    {
        var userDetails = await _userGrpcService.GetUserInfoAsync(request.UserId, cancellationToken);

        var emailRequest = new SendChangePasswordEmailRequest(
            userDetails.FirstName,
            userDetails.LastName,
            userDetails.Id,
            userDetails.Email,
            userDetails.ChangePasswordToken,
            request.ReturnUrl);

        await _emailService.SendChangePasswordEmailAsync(emailRequest, cancellationToken);

        return Result.Success();
    }
}