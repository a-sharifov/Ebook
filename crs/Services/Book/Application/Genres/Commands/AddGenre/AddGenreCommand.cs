namespace Application.Genres.Commands.AddGenre;

public sealed record AddGenreCommand(string Name) : ICommand;
