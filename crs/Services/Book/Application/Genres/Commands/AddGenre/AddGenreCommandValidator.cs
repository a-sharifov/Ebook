using Domain.GenreAggregate.ValueObjects;

namespace Application.Genres.Commands.AddGenre;

internal sealed class AddGenreCommandValidator : AbstractValidator<AddGenreCommand>
{
    public AddGenreCommandValidator()
    {
        RuleFor(x => x.Name)
            .MaximumLength(GenreName.MaxLength)
            .NotEmpty();        
    }
}