using Domain.AuthorAggregate.Ids;
using Domain.AuthorAggregate.ValueObjects;
using Domain.BookAggregate;
using Domain.SharedKernel.Entities;

namespace Domain.AuthorAggregate;

public class Author : AggregateRoot<AuthorId>
{
    public FirstName FirstName { get; private set; }
    public LastName LastName { get; private set; }
    public Pseudonym Pseudonym { get; private set; }
    public Image Image { get; private set; }
    public AuthorDescription Description { get; private set; }

    private readonly List<Book> _authorBooks = [];
    public IReadOnlyCollection<Book> AuthorBooks => _authorBooks.AsReadOnly();

#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
    private Author() { }
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.

    private Author(
        AuthorId id,
        FirstName firstName,
        LastName lastName,
        Pseudonym pseudonym,
        Image image,
        AuthorDescription description)
    {
        Id = id;
        FirstName = firstName;
        LastName = lastName;
        Pseudonym = pseudonym;
        Image = image;
        Description = description;
    }

    public static Result<Author> Create(
        AuthorId id,
        FirstName firstName,
        LastName lastName,
        Pseudonym pseudonym,
        Image image,
        AuthorDescription description)
    {
        var author = new Author(id, firstName, lastName, pseudonym, image, description);

        // TODO: Add domain events

        return author;
    }
}
