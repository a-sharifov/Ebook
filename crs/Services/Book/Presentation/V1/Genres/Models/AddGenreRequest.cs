namespace Presentation.V1.Genres.Models;

public sealed record AddGenreRequest(
    [Required] string Name
    );
