namespace Presentation.V1.Genres.Models;

public sealed record UpdateGenreRequest(
   [Required] Guid Id,
   [Required] string Name
    );
