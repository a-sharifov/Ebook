using Domain.BookAggregate.Repositories.Requests;
using Domain.BookAggregate;
using System.Linq.Expressions;
using Domain.AuthorAggregate.Ids;
using Domain.GenreAggregate.Ids;
using Domain.LanguageAggregate.Ids;
namespace Persistence.Filters;

public class BookFiltersBuilder
{
    public static List<Expression<Func<Book, bool>>> Build(BookFilterRequest request)
    {
        var filters = new List<Expression<Func<Book, bool>>>();

        if (request.Title != null)
        {
            filters.Add(x => ((string)x.Title).Contains(request.Title));
        }

        if (request.MinPrice > 0)
        {
            filters.Add(x => x.Price > request.MinPrice);
        }

        if (request.MaxPrice > 0)
        {
            filters.Add(x => x.Price < request.MaxPrice);
        }

        if (request.AuthorId != default)
        {
            filters.Add(x => x.Author.Id == new AuthorId(request.AuthorId));
        }

        if (request.GenreId != default)
        {
            filters.Add(x => x.Genre.Id == new GenreId(request.GenreId));
        }

        if (request.LanguageId != default)
        {
            filters.Add(x => x.Language.Id == new LanguageId(request.LanguageId));
        }

        return filters;
    }
}
