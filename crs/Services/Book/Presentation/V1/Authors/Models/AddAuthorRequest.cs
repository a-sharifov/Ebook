namespace Presentation.V1.Authors.Models;

public sealed record AddAuthorRequest(
    [Required] string Pseudonym);
