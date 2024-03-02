using Domain.Core.Errors;

namespace Domain.Core.Results;

/// <summary>
/// class for result pattern.
/// </summary>
public class Result : IResult
{
    public bool IsSuccess { get; }
    public bool IsFailure => !IsSuccess;
    public Error Error { get; }

    /// <summary>
    /// initialize a new instance of the <see cref="Result"/> class.
    /// </summary>
    /// <param name="isSuccess"> The success.</param>
    /// <param name="error"> The error.</param>
    /// <exception cref="ArgumentException"></exception>
    protected Result(bool isSuccess, Error error)
    {
        if (isSuccess && error != Error.None ||
            !isSuccess && error == Error.None)
        {
            throw new ArgumentException(
                "invalid entry parameter");
        }

        IsSuccess = isSuccess;
        Error = error;
    }

    /// <summary>
    /// Create a success result.
    /// </summary>
    /// <returns> The result.</returns>
    public static Result Success() => new(true, Error.None);

    /// <summary>
    /// Create a Success return value.
    /// </summary>
    /// <typeparam name="TObject"> The value type.</typeparam>
    /// <param name="value"> The value.</param>
    /// <returns> The result.</returns>
    public static Result<TObject> Success<TObject>(TObject value) => new(value, true, Error.None);

    /// <summary>
    /// Create a result.
    /// if the value is null, return a failure result.
    /// else return a success result.
    /// </summary>
    /// <typeparam name="TObject"> The value type.</typeparam>
    /// <param name="value"> The value.</param>
    /// <param name="error"> The error.</param>
    /// <returns> The result.</returns>
    public static Result<TObject> Create<TObject>(TObject value, Error error)
        where TObject : class
        => value is null ? Failure<TObject>(error) : Success(value);

    /// <summary>
    /// Create a failure result.
    /// </summary>
    /// <param name="error"> The error.</param>
    /// <returns> The result.</returns>
    public static Result Failure(Error error) => new(false, error);

    /// <summary>
    /// Create a failure generic result.
    /// </summary>
    /// <typeparam name="TObject"> The value type.</typeparam>
    /// <param name="error"> The error.</param>
    /// <returns> The result.</returns>
    public static Result<TObject> Failure<TObject>(Error error) => new(default!, false, error);

    /// <summary>
    /// get first failure result or success result.
    /// </summary>
    /// <param name="results"> The results.</param>
    /// <returns> The result.</returns>
    public static Result FirstFailureOrSuccess(params Result[] results)
    {
        foreach (Result result in results)
        {
            if (result.IsFailure)
            {
                return result;
            }
        }

        return Success();
    }

    public static Result<T> Failure<T>(object refreshTokenIsExpired)
    {
        throw new NotImplementedException();
    }
}
