using Domain.GenreAggregate.ValueObjects;

namespace Application.Genres.Commands.UpdateGenre;

internal sealed class UpdateGenreCommandValidator : AbstractValidator<UpdateGenreCommand>
{
    public UpdateGenreCommandValidator()
    {
        RuleFor(x => x.Id)
            .NotEmpty();

        RuleFor(x => x.Name)
            .NotEmpty()
            .MaximumLength(GenreName.MaxLength);
    }
}
