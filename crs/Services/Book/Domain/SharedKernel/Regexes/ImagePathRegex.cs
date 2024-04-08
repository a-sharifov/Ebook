namespace Domain.SharedKernel.Regexes;

public partial class ImagePathRegex
{
    private const string ImagePathPattern = @"^(http(s?):)([/|.|\w|\s|-])*\.(?:jpg|gif|png)$";

    [GeneratedRegex(ImagePathPattern)]
    public static partial Regex Regex();
}
