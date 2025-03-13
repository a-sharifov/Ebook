using Domain.Core.Results;
using Infrasctructure.Grpc.Users;
using Infrastructure.Emails.Interfaces;
using Infrastructure.Emails.Models;

namespace Application.Emails.Commands;

internal sealed class SendConfirmationUserMessageCommandHandler(
    IIdentityEmailService emailService,
    IUserGrpcService userGrpcService)
    : ICommandHandler<SendConfirmationUserMessageCommand>
{
    private readonly IIdentityEmailService _emailService = emailService;
    private readonly IUserGrpcService _userGrpcService = userGrpcService;

    public async Task<Result> Handle(SendConfirmationUserMessageCommand request, CancellationToken cancellationToken)
    {
        var userDetails = await _userGrpcService.GetUserInfoAsync(request.UserId, cancellationToken);

        var emailRequest = new SendConfirmationEmailRequest(
            userDetails.FirstName,
            userDetails.LastName,
            userDetails.Id,
            userDetails.Email,
            userDetails.EmailConfirmationToken,
            request.ReturnUrl);

        await _emailService.SendConfirmationEmailAsync(emailRequest, cancellationToken);

        return Result.Success();
    }
}