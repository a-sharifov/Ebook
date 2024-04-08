using Application.Common.DTOs.Images;

public sealed record AddGenreCommand(
    string Name,
    ImageDto Image
    ) : ICommand;
