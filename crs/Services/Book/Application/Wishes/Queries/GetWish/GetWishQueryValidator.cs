namespace Application.Wishes.Queries.GetWish;

internal sealed class GetWishQueryValidator : AbstractValidator<GetWishQuery>
{
    public GetWishQueryValidator()
    {
        RuleFor(x => x.UserId)
          .NotEmpty();
    }
}