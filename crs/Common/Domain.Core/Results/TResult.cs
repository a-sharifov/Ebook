using Domain.Core.Errors;
using System.Collections.Generic;

namespace Domain.Core.Results;

/// <summary>
/// Class generic for result.
/// </summary>
/// <typeparam name="TObject"> The value of the result.</typeparam>
public class Result<TObject> : Result
{
    /// <summary>
    /// The value of the result.
    /// </summary>
    private readonly TObject _value;

    /// <summary>
    /// Initializes a new instance of the <see cref="Result{TValue}"/> class.
    /// </summary>
    /// <param name="value"> The value.</param>
    /// <param name="isSuccess"> The success flag.</param>
    /// <param name="error"> The error.</param>
    protected internal Result(TObject value, bool isSuccess, Error error)
        : base(isSuccess, error)
        => _value = value;

    /// <summary>
    /// Result value if value null return exception
    /// </summary>
    public TObject Value => IsSuccess
        ? _value
        : throw new InvalidOperationException("The value of a failure result can not be accessed.");

    /// <summary>
    /// Create a success result.
    /// </summary>
    /// <param name="value"> The result value.</param>
    public static implicit operator Result<TObject>(TObject value) => Success(value);
}
