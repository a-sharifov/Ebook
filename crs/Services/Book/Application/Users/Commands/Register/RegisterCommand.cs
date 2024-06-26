﻿namespace Application.Users.Commands.Register;

public sealed record RegisterCommand(
    string Email,
    string Password,
    string ConfirmPassword,
    string FirstName,
    string LastName,
    string ReturnUrl
    ) : ICommand;
