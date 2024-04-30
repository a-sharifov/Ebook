namespace Application.Languages.Commands.UpdateLanguage;

public sealed record UpdateLanguageCommand(
    Guid Id,
    string Name,
    string Code
    ) : ICommand;
