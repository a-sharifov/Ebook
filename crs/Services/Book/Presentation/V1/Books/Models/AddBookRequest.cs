using Microsoft.AspNetCore.Http;

namespace Presentation.V1.Books.Models;

public sealed record AddBookRequest
(
    [Required] string Title,
    [Required] string Description,
    [Required] int PageCount,
    [Required] decimal Price,
    [Required] Guid LanguageId,
    [Required] int Quantity,
    [Required] Guid AuthorId,
    [Required] Guid GenreId,
    IFormFile Poster
);
