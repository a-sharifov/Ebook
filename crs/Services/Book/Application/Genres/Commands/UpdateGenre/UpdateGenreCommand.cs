namespace Application.Genres.Commands.UpdateGenre;

public sealed record UpdateGenreCommand(
    Guid Id, 
    string Name
    ) : ICommand;
