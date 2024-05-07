using Infrastructure.Jwt.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Api.Middlewares;

public sealed class JwtBlackListMiddleware(
    RequestDelegate next,
    IJwtBlacklistManager blacklistManager)
{
    private readonly RequestDelegate _next = next;
    private readonly IJwtBlacklistManager _blacklistManager = blacklistManager;

    public async Task InvokeAsync(HttpContext context)
    {
        var authorizationHeader = context.Request.Headers.Authorization.FirstOrDefault();
        var isInBlacklist = authorizationHeader != null
            && await _blacklistManager.IsInListAsync(authorizationHeader["Bearer ".Length..]);

        if (!isInBlacklist)
        {
            await _next(context);
            return;
        }

        var problemDetails = new ProblemDetails
        {
            Status = StatusCodes.Status401Unauthorized,
            Title = "User unauthorized.",
            Type = "https://tools.ietf.org/html/rfc7231#section-6.6.1",
        };

        context.Response.StatusCode = StatusCodes.Status401Unauthorized;

        await context.Response.WriteAsJsonAsync(problemDetails);
    }

}
