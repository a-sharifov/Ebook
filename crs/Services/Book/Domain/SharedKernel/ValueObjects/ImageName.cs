namespace Domain.SharedKernel.ValueObjects;

public sealed class ImageName : ValueObject
{
    public const int MaxLength = 500;

    public string Value { get; private set; }

    private ImageName(string value) =>
        Value = value;

    public static Result<ImageName> Create(string name)
    {
        if (name.IsNullOrWhiteSpace())
        {
            return Result.Failure<ImageName>(
                ImageNameErrors.CannotBeEmpty);
        }

        if (name.Length > MaxLength)
        {
            return Result.Failure<ImageName>(
                ImageNameErrors.CannotBeLongerThan(MaxLength));
        }

        var uniqueName = GenerateUniqueName(name);

        return new ImageName(uniqueName);
    }

    public static string GenerateUniqueName(string name) =>
        Guid.NewGuid().ToString() + "_" + name;

    public override IEnumerable<object> GetEqualityComponents()
    {
        yield return Value;
    }
}