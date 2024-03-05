namespace Domain.Common.ValueObjects;

public class ImageUrl : ValueObject
{
    public string Value { get; private set; }
    public const int MaxLength = 2000;

    private ImageUrl(string value) => Value = value;

    public static Result<ImageUrl> Create(string imageUrl)
    {
        if (imageUrl.IsNullOrWhiteSpace())
        {
            return Result.Failure<ImageUrl>(
                ImageUrlErrors.CannotByEmpty);
        }

        imageUrl = imageUrl.Trim();

        if (imageUrl.Length > MaxLength)
        {
            return Result.Failure<ImageUrl>(
                ImageUrlErrors.IsTooLong);
        }

        if (!IsImageUrl(imageUrl))
        {
            return Result.Failure<ImageUrl>(
                ImageUrlErrors.IsInvalid);
        }

        return new ImageUrl(imageUrl);
    }

    public static bool IsImageUrl(string imageUrl) =>
       ImageUrlRegex.Regex().IsMatch(imageUrl);

    public override IEnumerable<object> GetEqualityComponents()
    {
        yield return Value;
    }
}