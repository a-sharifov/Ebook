using Domain.Core.UnitOfWorks.Interfaces;
using Domain.GenreAggregate.Errors;
using Domain.GenreAggregate.Ids;
using Domain.GenreAggregate.Repositories;

namespace Application.Genres.Commands.DeleteGenre;

internal sealed class DeleteGenreCommandHandler(IUnitOfWork unitOfWork, IGenreRepository repository) : ICommandHandler<DeleteGenreCommand>
{
    private readonly IGenreRepository _repository = repository;
    private readonly IUnitOfWork _unitOfWork = unitOfWork;

    public async Task<Result> Handle(DeleteGenreCommand request, CancellationToken cancellationToken)
    {
        var id = new GenreId(request.Id);
        var isExist = await _repository.IsExistAsync(id, cancellationToken);

        if (!isExist)
        {
            return Result.Failure(
                GenreErrors.IsNotExist);
        }

        await _repository.DeleteAsync(id, cancellationToken);
        await _unitOfWork.Commit(cancellationToken);

        return Result.Success();
    }
}

