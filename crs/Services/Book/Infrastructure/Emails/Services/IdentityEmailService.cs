﻿using Domain.UserAggregate;
using Infrastructure.Emails.Interfaces;
using Infrastructure.Emails.Models;
using Infrastructure.Emails.Options;
using Infrastructure.Endpoint.Options;
using Microsoft.Extensions.Options;
using System.Text.Encodings.Web;

namespace Infrastructure.Emails.Services;

public sealed class IdentityEmailService
    (IOptions<EmailOptions> options,
     IOptions<IdentityEndpointOptions> identityEndpointOptions) :
    EmailBaseService(options), 
    IIdentityEmailService
{
    private readonly IdentityEndpointOptions _identityEndpointOptions = identityEndpointOptions.Value;

    public async Task SendConfirmationEmailAsync(SendConfirmationEmailRequest request, CancellationToken cancellationToken = default)
    {
        var confirmEmailTemplatePath = EmailTemplatePath.ConfirmEmailTemplate;

        string confirmEmailTemplate =
            await File.ReadAllTextAsync(confirmEmailTemplatePath, cancellationToken);

        var confirmUrl =
           $@"{_identityEndpointOptions.BaseUrl}/api/v1/users/confirm-email?userId={request.UserId}&emailConfirmationToken={request.EmailConfirmationToken}&returnUrl={request.ReturnUrl}";

        var confirmUrlEncode = HtmlEncoder.Default.Encode(confirmUrl);

        confirmEmailTemplate =
            confirmEmailTemplate
            .Replace("{{firstName}}", request.FirstName)
            .Replace("{{lastName}}", request.LastName)
            .Replace("{{confirmationLink}}", confirmUrlEncode);

        var sendMessageRequest = new SendMessageRequest(
            To: request.Email,
            Subject: $"Ebook - confirm email",
            Body: confirmEmailTemplate
            );

        await SendMessageAsync(sendMessageRequest, cancellationToken);
    }

    public async Task SendConfirmationEmailAsync(User user, string returnUrl, CancellationToken cancellationToken = default)
    {
        var request =
            new SendConfirmationEmailRequest(
                user.FirstName.Value,
                user.LastName.Value,
                user.Id.Value.ToString(),
                user.Email.Value,
                user.EmailConfirmationToken!.Value,
                returnUrl);

        await SendConfirmationEmailAsync(request, cancellationToken);
    }

    public async Task SendForgotPasswordEmailAsync(SendForgotPasswordEmailRequest request, CancellationToken cancellationToken = default)
    {
        var forgotPasswordTemplatePath = EmailTemplatePath.ForgotPasswordTemplate;

        string forgotPasswordTemplate =
            await File.ReadAllTextAsync(forgotPasswordTemplatePath, cancellationToken);

        var resetPasswordUrl =
           $@"{_identityEndpointOptions.BaseUrl}/api/v1/users/confirm-forgot-password?userId={request.UserId}&resetPasswordToken={request.ResetPasswordToken}&returnUrl={request.ReturnUrl}";

        var resetPasswordUrlEncode = HtmlEncoder.Default.Encode(resetPasswordUrl);

        forgotPasswordTemplate =
            forgotPasswordTemplate
            .Replace("{{firstName}}", request.FirstName)
            .Replace("{{lastName}}", request.LastName)
            .Replace("{{resetPasswordLink}}", resetPasswordUrlEncode);

        var sendMessageRequest = new SendMessageRequest(
            To: request.Email,
            Subject: $"Eshop - reset password",
            Body: forgotPasswordTemplate);

        await SendMessageAsync(sendMessageRequest, cancellationToken);
    }

    public async Task SendForgotPasswordEmailAsync(User user, string returnUrl, CancellationToken cancellationToken = default)
    {
        var request =
            new SendForgotPasswordEmailRequest(
                user.Id.Value,
                user.FirstName,
                user.LastName,
                user.Email,
                user.ResetPasswordToken!,
                returnUrl);

        await SendForgotPasswordEmailAsync(request, cancellationToken);
    }

}
