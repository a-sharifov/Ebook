namespace Application.Common.DTOs.Users;

public sealed record UserDto(
    Guid Id,
    string Email,
    string FirstName,
    string LastName,
    bool IsEmailConfirmed,
    string Role,
    string Gender);