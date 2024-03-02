namespace Domain.Core.Errors.Interfaces;

/// <summary>
/// Interface for error.
/// </summary>
public interface IError
{
    /// <summary>
    /// Gets the code of the error.
    /// </summary>
    string Code { get; }

    /// <summary>
    /// Gets the message of the error.
    /// </summary>
    string Message { get; }
}