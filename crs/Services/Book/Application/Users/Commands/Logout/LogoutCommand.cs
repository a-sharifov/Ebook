namespace Application.Users.Commands.Logout;

public sealed record LogoutCommand(string Token) : ICommand;
