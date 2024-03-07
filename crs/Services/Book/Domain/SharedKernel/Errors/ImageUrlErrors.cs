namespace Domain.SharedKernel.Errors;

public static class ImageUrlErrors
{
    public static Error CannotByEmpty =>
        new("ImageUrl.CannotByEmpty", "Image url cannot be empty");

    public static Error IsInvalid =>
        new("ImageUrl.IsInvalid", "Image url is invalid");

    public static Error IsTooLong =>
        new("ImageUrl.IsTooLong", "Image url is too long");
}