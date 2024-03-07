using Domain.Core.Errors;

namespace Domain.Core.Results.Interfaces;

/// <summary>
/// Interface for result.
/// </summary>
public interface IResult
{
    /// <summary>
    /// Gets the error.
    /// </summary>
    Error Error { get; }

    /// <summary>
    /// Gets a value indicating whether the result is failure.
    /// </summary>
    bool IsFailure { get; }

    /// <summary>
    /// Gets a value indicating whether the result is success.
    /// </summary>
    bool IsSuccess { get; }
}