namespace Domain.SharedKernel.Errors;

public static class ImageNameErrors
{
    public static Error CannotBeEmpty =>
       new("ImageName.CannotBeEmpty", "Image name cannot be empty.");

    public static Error CannotBeLongerThan(int maxLength) =>
        new("ImageName.CannotBeLongerThan", $"Image name cannot be longer than {maxLength} characters");
}
