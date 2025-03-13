﻿using Domain.Core.Errors.Interfaces;
using Domain.Core.ValueObjects;

namespace Domain.Core.Errors;

/// <summary>
/// Class for error.
/// </summary>
public sealed class Error : ValueObject, IError
{
    /// <summary>
    /// Gets the code of the error.
    /// </summary>
    public string Code { get; }
    
    /// <summary>
    /// Gets the message of the error.
    /// </summary>
    public string Message { get; }

    /// <summary>
    /// Initializes a new instance of the <see cref="Error"/> class.
    /// </summary>
    /// <param name="code">The code of the error.</param>
    /// <param name="message">The message of the error.</param>
    public Error(string code, string message)
    {
        Code = code;
        Message = message;
    }

    /// <summary>
    /// Gets the none error.
    /// </summary>
    public static Error None => new(string.Empty, string.Empty);

    /// <summary>
    /// Gets the code of the error.
    /// </summary>
    /// <param name="error"> The error.</param>
    public static implicit operator string(Error error) => error?.Code ?? string.Empty;

    /// <summary>
    /// Gets the equality components.
    /// </summary>
    /// <returns>The equality components.</returns>
    public override IEnumerable<object> GetEqualityComponents()
    {
        yield return Code;
        yield return Message;
    }
}
