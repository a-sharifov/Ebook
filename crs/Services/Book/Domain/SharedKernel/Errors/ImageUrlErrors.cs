namespace Domain.SharedKernel.Errors;

public static class ImageUrlErrors
{
    public static Error CannotBeEmpty =>
     new("ImageUrl.CannotBeEmpty", "Image url cannot be empty.");

    public static Error CannotBeLongerThan(int maxLength) =>
        new("ImageUrl.CannotBeLongerThan", $"Image url cannot be longer than {maxLength} characters");
}