namespace Presentation.V1.Languages.Models;

public sealed record UpdateLanguageRequest(
    [Required] Guid Id,
    [Required] string Name,
    [Required] string Code
    );
