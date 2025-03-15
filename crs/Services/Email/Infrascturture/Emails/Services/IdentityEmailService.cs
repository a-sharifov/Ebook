using Infrastructure.Emails.Interfaces;
using Infrastructure.Emails.Models;
using Infrastructure.Emails.Options;
using Infrastructure.Endpoint.Options;
using Microsoft.Extensions.Options;
using Org.BouncyCastle.Asn1.Ocsp;
using System.Text.Encodings.Web;

namespace Infrastructure.Emails.Services;

public sealed class IdentityEmailService
    (IOptions<EmailOptions> options,
     IOptions<IdentityEndpointOptions> identityEndpointOptions) :
    EmailBaseService(options), 
    IIdentityEmailService
{
    private readonly IdentityEndpointOptions _identityEndpointOptions = identityEndpointOptions.Value;

    public async Task SendChangePasswordEmailAsync(SendChangePasswordEmailRequest request, CancellationToken cancellationToken)
    {
        var changePasswordTemplatePath = EmailTemplatePath.ChangePasswordTemplate;

        string changePasswordTemplate =
            await File.ReadAllTextAsync(changePasswordTemplatePath, cancellationToken);


        var changePasswordUrl =
        $@"{request.ReturnUrl}?userId={request.UserId}&changePasswordToken={request.ResetPasswordToken}";

        changePasswordTemplate =
        changePasswordTemplate
        .Replace("{{firstName}}", request.FirstName)
            .Replace("{{lastName}}", request.LastName)
            .Replace("{{changePasswordLink}}", changePasswordUrl);

        var sendMessageRequest = new SendMessageRequest(
            To: request.Email,
            Subject: $"Ebook - reset email",
            Body: changePasswordTemplate
            );

        await SendMessageAsync(sendMessageRequest, cancellationToken);
    }

    public async Task SendConfirmationEmailAsync(SendConfirmationEmailRequest request, CancellationToken cancellationToken = default)
    {
        var confirmEmailTemplatePath = EmailTemplatePath.ConfirmEmailTemplate;

        string confirmEmailTemplate =
            await File.ReadAllTextAsync(confirmEmailTemplatePath, cancellationToken);

        var returnUrlEncode = Uri.EscapeDataString(request.ReturnUrl);

        var confirmUrl =
        $@"http://localhost/api/v1/users/confirm-email?userId={request.UserId}&emailConfirmationToken={request.EmailConfirmationToken}&returnUrl={returnUrlEncode}";

        confirmEmailTemplate =
            confirmEmailTemplate
            .Replace("{{firstName}}", request.FirstName)
            .Replace("{{lastName}}", request.LastName)
            .Replace("{{confirmationLink}}", confirmUrl);

        var sendMessageRequest = new SendMessageRequest(
            To: request.Email,
            Subject: $"Ebook - confirm email",
            Body: confirmEmailTemplate
            );

        await SendMessageAsync(sendMessageRequest, cancellationToken);
    }

    public async Task SendForgotPasswordEmailAsync(SendForgotPasswordEmailRequest request, CancellationToken cancellationToken = default)
    {
        var forgotPasswordTemplatePath = EmailTemplatePath.ForgotPasswordTemplate;

        string forgotPasswordTemplate =
            await File.ReadAllTextAsync(forgotPasswordTemplatePath, cancellationToken);

        var returnUrl = @$"{request.ReturnUrl}?userId={request.UserId}&resetPasswordToken={request.ResetPasswordToken}";
        var returnUrlEncode = Uri.EscapeDataString(returnUrl);

        var resetPasswordUrl =$@"http://localhost/api/v1/users/confirm-forgot-password?userId={request.UserId}&resetPasswordToken={request.ResetPasswordToken}&returnUrl={returnUrlEncode}";

        forgotPasswordTemplate =
            forgotPasswordTemplate
            .Replace("{{firstName}}", request.FirstName)
            .Replace("{{lastName}}", request.LastName)
            .Replace("{{resetPasswordLink}}", resetPasswordUrl);

        var sendMessageRequest = new SendMessageRequest(
            To: request.Email,
            Subject: $"Ebook - reset password",
            Body: forgotPasswordTemplate);

        await SendMessageAsync(sendMessageRequest, cancellationToken);
    }

}
