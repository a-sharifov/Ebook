﻿namespace Application.Users.Commands.Login;

public sealed record LoginCommand(
    string Email,
    string Password)
    : ICommand<LoginCommandResponse>;
