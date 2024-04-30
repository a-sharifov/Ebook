namespace Application.Authors.Commands.AddAuthor;

public sealed record AddAuthorCommand(string Pseudonym) : ICommand;
