using Domain.Core.UnitOfWorks.Interfaces;
using Domain.GenreAggregate.Errors;
using Domain.GenreAggregate.Ids;
using Domain.GenreAggregate.Repositories;
using Domain.GenreAggregate.ValueObjects;

namespace Application.Genres.Commands.UpdateGenre;

internal sealed class UpdateGenreCommandHandler(
    IGenreRepository repository, 
    IUnitOfWork unitOfWork)
    : ICommandHandler<UpdateGenreCommand>
{
    private readonly IGenreRepository _repository = repository;
    private readonly IUnitOfWork _unitOfWork = unitOfWork;

    public async Task<Result> Handle(UpdateGenreCommand request, CancellationToken cancellationToken)
    {
        var id = new GenreId(request.Id);
        var isExist = await _repository.IsExistAsync(id, cancellationToken);

        if (!isExist)
        {
            return Result.Failure(
                GenreErrors.IsNotExist);
        }

        var genre = await _repository.GetAsync(id, cancellationToken);

        var genreNameResult = GenreName.Create(request.Name);

        if (genreNameResult.IsFailure)
        {
            return genreNameResult;
        }

        genre.Update(genreNameResult.Value);

        await _repository.UpdateAsync(genre, cancellationToken);
        await _unitOfWork.Commit(cancellationToken);

        return Result.Success();
    }
}
