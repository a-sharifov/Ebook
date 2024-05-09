namespace Presentation.V1.Authors.Models;

public sealed record UpdateAuthorRequest(
    [Required] Guid Id,
    [Required] string Pseudonym
    );
