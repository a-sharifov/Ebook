namespace Application.Wishes.Commands.AddBookInWish;

public sealed record AddBookInWishCommand(
    Guid UserId,
    Guid BookId    
    ) : ICommand;
