namespace Application.Common.DTOs.Users;

public sealed record UserDetailsDto(
    Guid Id,
    string Email,
    string FirstName,
    string LastName,
    bool IsEmailConfirmed,
    string Role,
    string? EmailConfirmationToken,
    string? ResetPasswordToken
    );