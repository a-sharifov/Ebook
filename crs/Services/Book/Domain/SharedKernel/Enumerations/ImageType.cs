using Domain.Core.Enumerations;

namespace Domain.SharedKernel.Enumerations;

public class ImageType(int value, string name) : Enumeration<ImageType>(value, name)
{
    public static readonly ImageType Jpg = new(0, nameof(Jpg));
    public static readonly ImageType Png = new(1, nameof(Png));
}
