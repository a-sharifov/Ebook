namespace Domain.SharedKernel.Errors;

public static class ImageErrors
{
    public static Error ImageNameCannotBeEmpty =>
        new("Image.CannotBeEmpty", "Image name cannot be empty");

    public static Error IsNotExist =>
        new("Image.IsNotExist", "Is not exist.");
}