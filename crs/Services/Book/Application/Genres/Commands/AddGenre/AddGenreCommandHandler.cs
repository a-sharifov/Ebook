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
        var genreName = GenreName.Create(request.Name).Value;
        var genre = Genre.Create(id, genreName).Value;

        await _repository.AddAsync(genre, cancellationToken);
        await _unitOfWork.Commit(cancellationToken);

        return Result.Success();
    }
}
