using Domain.Core.Enumerations;

namespace Domain.SharedKernel.Enumerations;

public class ImageBucket(int value, string name) : Enumeration<ImageBucket>(value, name)
{
    public static readonly ImageBucket Genres = new(0, "genres");
    public static readonly ImageBucket Books = new(1, "books");
    public static readonly ImageBucket Authors = new(2, "authors");
}
