using Microsoft.AspNetCore.Http;

namespace Presentation.V1.Books.Models;

public sealed record UpdateBookRequest(
    [Required] Guid Id,
    [Required] string Title,
    [Required] string Description,
    [Required] int PageCount,
    [Required] decimal Price,
    [Required] Guid LanguageId,
    [Required] int Quantity,
    [Required] string AuthorPseudonym,
    [Required] Guid GenreId,
    IFormFile Poster
    );
