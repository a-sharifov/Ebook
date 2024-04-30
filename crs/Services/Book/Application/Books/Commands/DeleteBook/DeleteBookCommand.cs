namespace Application.Books.Commands.DeleteBook;

public sealed record DeleteBookCommand(Guid Id) : ICommand;
