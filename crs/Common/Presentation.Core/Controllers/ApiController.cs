using Domain.Core.Errors;
using Domain.Core.Results;
using Domain.Core.Results.Interfaces;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Presentation.Core.Controllers;

/// <summary>
/// Base class for API controllers.
/// </summary>
/// <remarks>
/// Initializes a new instance of the <see cref="ApiController"/> class.
/// </remarks>
/// <param name="sender"> The sender.</param>
[ApiController]
public abstract class ApiController(ISender sender) : ControllerBase
{
    /// <summary>
    /// The sender.
    /// </summary>
    protected readonly ISender _sender = sender;

    /// <summary>
    /// Handles the result of a request.
    /// </summary>
    /// <param name="result"> The result.</param>
    /// <returns> The result.</returns>
    /// <exception cref="InvalidCastException"> Thrown when the result is a success.</exception>
    protected IActionResult HandleFailure(Result result)
    {
        if (result.IsSuccess)
        { 
            throw new InvalidCastException();
        }

        if (result is IValidationResult validationResult)
        {
            return BadRequest(
                CreateProblemDetails(
                    "Validation error",
                    StatusCodes.Status400BadRequest,
                    result.Error,
                    validationResult.Errors
                )
            );
        }

        return BadRequest(
            CreateProblemDetails(
                "Bad Request",
                StatusCodes.Status400BadRequest,
                result.Error
            )
        );
    }

    /// <summary>
    /// Handles the result of a request.
    /// </summary>
    /// <param name="title"> The title.</param>
    /// <param name="status"> The status.</param>
    /// <param name="error"> The error.</param>
    /// <param name="errors"> The errors.</param>
    /// <returns></returns>
    private static ProblemDetails CreateProblemDetails(
        string title,
        int status,
        Error error,
        Error[]? errors = null) =>
        new(
            title,
            status,
            error.Message,
            $"https://httpstatuses.com/{status}",
            ( nameof(errors),  errors  )
        );

    protected Guid GetUserId()
    {
        var nameIdClaim = User.Claims.First(x => x.Type == "id");
        var id = Guid.Parse(nameIdClaim.Value);
        return id;
    }

    protected string GetBearerToken() =>
        Request.Headers["Authorization"].First()!["Bearer ".Length..];
}
