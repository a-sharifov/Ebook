using Domain.BookAggregate.Repositories.Requests;
using Domain.BookAggregate;
using System.Linq.Expressions;

namespace Persistence.Filters;

public class BookFiltersBuilder
{
    public static List<Expression<Func<Book, bool>>> Build(BookFilterRequest request)
    {
        var filters = new List<Expression<Func<Book, bool>>>();

        if (request.Title is not null)
        {
            filters.Add(x => x.Title.Value == request.Title);
        }

        if (request.AuthorId != default)
        {
            filters.Add(x => x.Author.Id.Value == request.AuthorId);
        }

        if (request.MinPrice > 0)
        {
            filters.Add(x => x.Price.Value > request.MinPrice);
        }

        if (request.MaxPrice > 0)
        {
            filters.Add(x => x.Price.Value < request.MaxPrice);
        }

        if (request.GenreId != default)
        {
            filters.Add(x => x.Genre.Id.Value == request.GenreId);
        }

        return filters;
    }
}
