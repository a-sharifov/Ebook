using Domain.BookAggregate;
using Domain.LanguageAggregate.Ids;
using Domain.LanguageAggregate.ValueObjects;

namespace Domain.LanguageAggregate;

public class Language : AggregateRoot<LanguageId>
{
    public LanguageName Name { get; private set; }
    public LanguageCode Code { get; private set; }
    private readonly List<Book> _books = [];
    public IReadOnlyCollection<Book> Books => _books.AsReadOnly();

#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
    private Language() { }
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.

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

    public void Update(LanguageName name, LanguageCode code)
    {
        Name = name;
        Code = code;
    }
}
        