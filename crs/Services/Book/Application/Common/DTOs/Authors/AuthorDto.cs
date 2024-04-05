namespace Application.Common.DTOs.Authors;

public sealed record AuthorDto(
    Guid Id,
    string FirstName,
    string LastName,
    string Pseudonym,
    string Image,
    string Description
    );
