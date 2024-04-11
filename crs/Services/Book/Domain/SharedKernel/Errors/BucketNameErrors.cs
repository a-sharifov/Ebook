namespace Domain.SharedKernel.Errors;

public static class BucketNameErrors
{
    public static Error CannotBeEmpty =>
      new("BucketName.CannotBeEmpty", "Image name cannot be empty.");

    public static Error CannotBeLongerThan(int maxLength) =>
        new("BucketName.CannotBeLongerThan", $"Bucket name cannot be longer than {maxLength} characters");
}
