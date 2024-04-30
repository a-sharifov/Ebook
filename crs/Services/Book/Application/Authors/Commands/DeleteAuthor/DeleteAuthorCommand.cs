namespace Application.Authors.Commands.DeleteAuthor;

public sealed record DeleteAuthorCommand(Guid AuthorId) : ICommand;
