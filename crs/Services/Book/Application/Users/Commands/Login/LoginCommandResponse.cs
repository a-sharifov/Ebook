namespace Application.Users.Commands.Login;

public sealed record LoginCommandResponse(
    string Token,
    string RefreshToken);
