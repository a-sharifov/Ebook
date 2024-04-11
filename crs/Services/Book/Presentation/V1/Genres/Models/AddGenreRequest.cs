using Microsoft.Extensions.FileProviders;

namespace Presentation.V1.Genres.Models;

internal sealed record AddGenreRequest(
     IFileInfo GenreImage,
     string GenreName
    );