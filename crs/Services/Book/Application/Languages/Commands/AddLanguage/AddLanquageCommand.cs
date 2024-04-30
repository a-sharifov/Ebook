namespace Application.Languages.Commands.AddLanguage;

public sealed record AddLanquageCommand(
    string Name,
    string Code
    ) : ICommand;
