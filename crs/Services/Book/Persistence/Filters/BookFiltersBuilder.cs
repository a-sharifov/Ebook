using Domain.BookAggregate.Repositories.Requests;
using Domain.BookAggregate;
using System.Linq.Expressions;
namespace Persistence.Filters;

public class BookFiltersBuilder
{
    public static List<Expression<Func<Book, bool>>> Build(BookFilterRequest request)
    {
        var filters = new List<Expression<Func<Book, bool>>>();

        if (request.Title != null)
        {
            filters.Add(x => x.Title.Value.Contains(request.Title));
        }

        if (request.AuthorId != default)
        {
            filters.Add(x => x.Author.Id.Value == request.AuthorId);
        }

        if (request.MinPrice > 0)
        {
            filters.Add(x => x.Price > request.MinPrice);
        }

        if (request.MaxPrice > 0)
        {
            filters.Add(x => x.Price < request.MaxPrice);
        }

        if (request.GenreId != default)
        {
            filters.Add(x => x.Genre.Id.Value == request.GenreId);
        }

        return filters;
    }
}
