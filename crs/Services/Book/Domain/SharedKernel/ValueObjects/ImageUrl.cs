namespace Domain.SharedKernel.ValueObjects;

public sealed class ImageUrl : ValueObject
{
    public const int MaxLength = 500;

    public string Value { get; private set; }

    private ImageUrl(string value) =>
        Value = value;

    public static Result<ImageUrl> Create(string url)
    {
        if (url.IsNullOrWhiteSpace())
        {
            return Result.Failure<ImageUrl>(
                ImageUrlErrors.CannotBeEmpty);
        }

        if (url.Length > MaxLength)
        {
            return Result.Failure<ImageUrl>(
                ImageUrlErrors.CannotBeLongerThan(MaxLength));
        }

        return new ImageUrl(url);
    }


    public override IEnumerable<object> GetEqualityComponents()
    {
        yield return Value;
    }
}