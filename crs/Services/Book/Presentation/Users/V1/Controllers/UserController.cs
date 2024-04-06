﻿using Application.Users.Commands.ConfirmEmail;
using Application.Users.Commands.Login;
using Application.Users.Commands.Register;
using Application.Users.Commands.RetryConfirmEmailSend;
using Application.Users.Commands.UpdateRefreshToken;
using Application.Users.Queries.GetGenders;
using Application.Users.Queries.GetRoles;
using Microsoft.AspNetCore.Authorization;
using Presentation.Users.V1.Models;

namespace Presentation.Users.V1.Controllers;

[Route("api/v{version:apiVersion}/users")]
[ApiVersion("1.0")]
public sealed class UserController(ISender sender) : ApiController(sender)
{
    [HttpPost("login")]
    public async Task<IActionResult> Login(
        [FromHeader] string audience,
        [FromBody] LoginRequest request)
    {
        var command = new LoginCommand(
            request.Email,
            request.Password,
            audience);

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
            request.Role,
            request.Gender,
            request.ReturnUrl);

        var result = await _sender.Send(command);
        return result.IsSuccess ? Ok()
            : HandleFailure(result);
    }

    [HttpPut("update-refresh-token")]
    public async Task<IActionResult> UpdateRefreshToken([FromHeader] UpdateRefreshTokenRequest request)
    {
        var command = new UpdateRefreshTokenCommand(
            request.Token,
            request.RefreshToken,
            request.Audience);

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
        var command = new ConfirmEmailCommand(request.UserId, request.EmailConfirmationToken);

        var result = await _sender.Send(command);
        return result.IsSuccess ? Redirect(request.ReturnUrl)
            : HandleFailure(result);
    }

    [HttpGet("roles")]
    public async Task<IActionResult> GetRoles()
    {
        var query = new GetRolesQuery();

        var result = await _sender.Send(query);
        return result.IsSuccess ? Ok(result.Value)
            : HandleFailure(result);
    }

    [HttpGet("genders")]
    public async Task<IActionResult> GetGenders()
    {
        var query = new GetGendersQuery();

        var result = await _sender.Send(query);
        return result.IsSuccess ? Ok(result.Value)
            : HandleFailure(result);
    }

    [Authorize(Roles = "Admin")]
    [HttpGet("test")]
    public IActionResult Test()
    {
        return Ok("Test");
    }
}