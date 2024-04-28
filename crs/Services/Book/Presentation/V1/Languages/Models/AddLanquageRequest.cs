namespace Presentation.V1.Languages.Models;

public sealed record AddLanquageRequest(
    [Required] string Name,
    [Required] string Code
    );
