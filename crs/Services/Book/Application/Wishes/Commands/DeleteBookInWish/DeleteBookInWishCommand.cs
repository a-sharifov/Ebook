namespace Application.Wishes.Commands.DeleteBookInWish;

public sealed record DeleteBookInWishCommand(
    Guid UserId,
    Guid BookId
    ) : ICommand;
