using Domain.LanguageAggregate.Ids;
using Domain.LanguageAggregate.ValueObjects;

namespace Domain.LanguageAggregate;

public class Language : AggregateRoot<LanguageId>
{
    public LanguageName Name { get; private set; }
    public LanguageCode Code { get; private set; }

    private Language(LanguageId id, LanguageName name, LanguageCode code)
    {
        Id = id;
        Name = name;
        Code = code;
    }

    public static Result<Language> Create(LanguageId id, LanguageName name, LanguageCode code)
    {
        var language = new Language(id, name, code);

        //TODO: Add domain events

        return Result.Success(language);
    }
}
