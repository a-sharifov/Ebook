namespace Application.Authors.Commands.UpdateAuthor;

public sealed record UpdateAuthorCommand(
    Guid Id, 
    string Pseudonym) 
    : ICommand;
