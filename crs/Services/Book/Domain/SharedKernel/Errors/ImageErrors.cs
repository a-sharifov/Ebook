namespace Domain.SharedKernel.Errors;

public static class ImageErrors
{
    public static Error ImageNameCannotBeEmpty =>
        new("Image.CannotBeEmpty", "Image name cannot be empty");
}