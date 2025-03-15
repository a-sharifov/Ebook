using Domain.Core.Results;
using Infrasctructure.Grpc.Users;
using Infrastructure.Emails.Interfaces;
using Infrastructure.Emails.Models;

namespace Application.Emails.Commands.SendConfirmationUserMessage;

internal sealed class SendForgotPasswordUserMessageCommandHandler(
    IIdentityEmailService emailService,
    IUserGrpcService userGrpcService)
    : ICommandHandler<SendForgotPasswordUserMessageCommand>
{
    private readonly IIdentityEmailService _emailService = emailService;
    private readonly IUserGrpcService _userGrpcService = userGrpcService;

    public async Task<Result> Handle(SendForgotPasswordUserMessageCommand request, CancellationToken cancellationToken)
    {
        var userDetails = await _userGrpcService.GetUserInfoAsync(request.UserId, cancellationToken);

        var emailRequest = new SendForgotPasswordEmailRequest(
            Guid.Parse(userDetails.Id),
            userDetails.FirstName,
            userDetails.LastName,
            userDetails.Email,
            userDetails.ResetPasswordToken,
            request.ReturnUrl);

        await _emailService.SendForgotPasswordEmailAsync(emailRequest, cancellationToken);

        return Result.Success();
    }
}