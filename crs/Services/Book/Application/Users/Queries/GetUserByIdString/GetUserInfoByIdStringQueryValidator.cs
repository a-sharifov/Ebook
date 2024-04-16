namespace Application.Users.Queries.GetUserByIdString;

internal sealed class GetUserInfoByIdStringQueryValidator : AbstractValidator<GetUserByIdStringQuery>
{
    public GetUserInfoByIdStringQueryValidator()
    {
        RuleFor(x => x.Id)
            .NotEmpty();

        RuleFor(x => x.Id)
            .Must(x => Guid.TryParse(x, out _))
            .WithMessage("Invalid Id");
    }
}
