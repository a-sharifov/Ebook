using Domain.BookAggregate.ValueObjects;

namespace Application.Books.Queries.GetPagedListBooks;

internal sealed class GetPagedListBooksQueryValidator : AbstractValidator<GetPagedListBooksQuery>
{
    public GetPagedListBooksQueryValidator()
    {
        RuleFor(x => x.PageNumber)
            .GreaterThan(0);

        RuleFor(x => x.PageSize)
            .GreaterThan(0)
            .LessThanOrEqualTo(1024);

        RuleFor(x => x.MinPrice)
            .GreaterThanOrEqualTo(0);

        RuleFor(x => x.MaxPrice)
            .GreaterThanOrEqualTo(0);

        RuleFor(x => x.Title)
            .MaximumLength(Title.MaxLength);
    }
}