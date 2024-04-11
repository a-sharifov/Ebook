namespace Domain.SharedKernel.Entities;

public class BucketName : ValueObject
{
    public const int MaxLength = 60;

    public string Value { get; private set; }

    private BucketName(string value) =>
        Value = value;

    public static Result<BucketName> Create(string name)
    {
        if (name.IsNullOrWhiteSpace())
        {
            return Result.Failure<BucketName>(
                FirstNameErrors.CannotBeEmpty);
        }

        if (name.Length > MaxLength)
        {
            return Result.Failure<BucketName>(
                FirstNameErrors.CannotBeLongerThan(MaxLength));
        }

        return new BucketName(name);
    }


    public override IEnumerable<object> GetEqualityComponents()
    {
        yield return Value;
    }
}