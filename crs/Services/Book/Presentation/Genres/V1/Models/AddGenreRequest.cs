using Microsoft.Extensions.FileProviders;

namespace Presentation.Genres.V1.Models;

internal sealed record AddGenreRequest(
     IFileInfo GenreImage,
     string GenreName
    );