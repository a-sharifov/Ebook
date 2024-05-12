using Domain.Core.UnitOfWorks.Interfaces;
using Domain.GenreAggregate;
using Domain.GenreAggregate.Errors;
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
        var nameResult = GenreName.Create(request.Name);

        if (nameResult.IsFailure)
        {
            return nameResult;
        }

        var isExist = await _repository.IsNameExistAsync(nameResult.Value, cancellationToken);

        if (isExist)
        {
            return Result.Failure(
                GenreErrors.IsNameExist);
        }

        var id = new GenreId(Guid.NewGuid());
        var genre = Genre.Create(id, nameResult.Value).Value;

        await _repository.AddAsync(genre, cancellationToken);
        await _unitOfWork.Commit(cancellationToken);

        return Result.Success();
    }
}
