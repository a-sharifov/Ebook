namespace Application.Languages.Commands.DeleteLanguage;

public sealed record DeleteLanguageCommand(Guid Id) : ICommand;
