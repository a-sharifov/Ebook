
namespace Presentation.Core.Controllers;

public record ProblemDetails(
    string Title,
    int Status,
    string Detail,
    string Error,
    object Extensions);
