﻿namespace Presentation.V1.Users.Models;

public record RegisterRequest(
    [Required] string Email,
    [Required] string Password,
    [Required] string ConfirmPassword,
    [Required] string FirstName,
    [Required] string LastName,
    [Required] string Role,
    [Required] string Gender,
    [Required] string ReturnUrl);
