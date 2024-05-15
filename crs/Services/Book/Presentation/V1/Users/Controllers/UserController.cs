using Application.Users.Commands.ConfirmEmail;
using Application.Users.Commands.Login;
using Application.Users.Commands.Register;
using Application.Users.Commands.RetryConfirmEmailSend;
using Application.Users.Commands.ChangePassword;
using Application.Users.Commands.UpdateRefreshToken;
using Microsoft.AspNetCore.Authorization;
using Presentation.V1.Users.Models;
using Application.Users.Commands.Logout;
using Application.Users.Commands.ForgotPassword;
using Application.Users.Commands.ConfirmForgotPassword;
using Application.Users.Commands.LoginByForgotPassword;

namespace Presentation.V1.Users.Controllers;

[Route("api/v{version:apiVersion}/users")]
[ApiVersion("1.0")]
public sealed class UserController(ISender sender) : ApiController(sender)
{
    [HttpPost("login")]
    public async Task<IActionResult> Login([FromBody] LoginRequest request)
    {
        var command = new LoginCommand(
            request.Email,
            request.Password);

        var result = await _sender.Send(command);

        return result.IsSuccess ? Ok(result.Value)
            : HandleFailure(result);
    }

    [HttpPost("login-by-forgot-password")]
    public async Task<IActionResult> LoginByForgotPassword([FromBody] LoginByForgotPasswordTokenRequest request)
    {
        var command = new LoginByForgotPasswordCommand(
            request.UserId,
            request.ResetPasswordToken);

        var result = await _sender.Send(command);

        return result.IsSuccess ? Ok(result.Value)
            : HandleFailure(result);
    }

    [HttpPost("register")]
    public async Task<IActionResult> Register([FromBody] RegisterRequest request)
    {
        var command = new RegisterCommand(
            request.Email,
            request.Password,
            request.ConfirmPassword,
            request.FirstName,
            request.LastName,
            request.ReturnUrl);

        var result = await _sender.Send(command);

        return result.IsSuccess ? Ok()
            : HandleFailure(result);
    }

    [HttpPut("update-refresh-token")]
    public async Task<IActionResult> UpdateRefreshToken([FromBody] UpdateRefreshTokenRequest request)
    {
        var command = new UpdateRefreshTokenCommand(
            request.Token,
            request.RefreshToken);

        var result = await _sender.Send(command);

        return result.IsSuccess ? Ok(result.Value)
            : HandleFailure(result);
    }

    [HttpPost("retry-confirm-email-send")]
    public async Task<IActionResult> RetryConfirmEmailSend([FromBody] RetryConfirmEmailSendRequest request)
    {
        var command = new RetryConfirmEmailSendCommand(
            request.Email,
            request.ReturnUrl);

        var result = await _sender.Send(command);

        return result.IsSuccess ? Ok()
            : HandleFailure(result);
    }

    [HttpPost("confirm-email")]
    public async Task<IActionResult> ConfirmEmail([FromQuery] ConfirmEmailRequest request)
    {
        var command = new ConfirmEmailCommand(
            request.UserId,
            request.EmailConfirmationToken);

        var result = await _sender.Send(command);

        return result.IsSuccess ? Redirect(request.ReturnUrl)
            : HandleFailure(result);
    }

    [HttpPost("forgot-password")]
    public async Task<IActionResult> ForgotPassword([FromBody] ForgotPasswordRequest request)
    {
        var command = new ForgotPasswordCommand(
            request.Email,
            request.ReturnUrl);

        var result = await _sender.Send(command);

        return result.IsSuccess ? Ok()
            : HandleFailure(result);
    }

    [HttpPost("confirm-forgot-password")]
    public async Task<IActionResult> ConfirmChangePassword([FromForm] ConfirmForgotPasswordRequest request)
    {
        var command = new ConfirmForgotPasswordCommand(
            request.UserId,
            request.ResetPasswordToken);

        var result = await _sender.Send(command);

        return result.IsSuccess ? Redirect(request.ReturnUrl)
            : HandleFailure(result);
    }

    [Authorize]
    [HttpPost("change-password")]
    public async Task<IActionResult> ChangePassword([FromBody] ChangePasswordRequest request)
    {
        var userId = GetUserId();
        var command = new ChangePasswordCommand(userId, request.Password);

        var result = await _sender.Send(command);

        return result.IsSuccess ? Ok()
            : HandleFailure(result);
    }

    [Authorize]
    [HttpPost("logout")]
    public async Task<IActionResult> Logout()
    {
        var bearerToken = GetBearerToken();
        var command = new LogoutCommand(bearerToken);

        var result = await _sender.Send(command);

        return result.IsSuccess ? Ok()
            : HandleFailure(result);
    }
}
