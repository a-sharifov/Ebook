using Domain.Core.UnitOfWorks.Interfaces;
using Domain.GenreAggregate;
using Domain.GenreAggregate.Ids;
using Domain.GenreAggregate.Repositories;
using Domain.GenreAggregate.ValueObjects;

namespace Application.Genres.Commands.AddGenre;

public sealed class AddGenreCommandHandler(IGenreRepository repository, IUnitOfWork unitOfWork) : ICommandHandler<AddGenreCommand>
{
    private readonly IGenreRepository _repository = repository;
    private readonly IUnitOfWork _unitOfWork = unitOfWork;

    public async Task<Result> Handle(AddGenreCommand request, CancellationToken cancellationToken)
    {
        var id = new GenreId(Guid.NewGuid());
        var image = request.Image.Adapt<Image>();
        var genreNameResult = GenreName.Create(request.Name);
        var genreResult = Genre.Create(id, genreNameResult.Value, image);

        await _repository.AddAsync(genreResult.Value, cancellationToken);
        await _unitOfWork.Commit(cancellationToken);

        return Result.Success();
    }
}
