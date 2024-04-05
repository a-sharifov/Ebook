using Domain.Core.UnitOfWorks.Interfaces;
using Domain.GenreAggregate;
using Domain.GenreAggregate.Ids;
using Domain.GenreAggregate.Repositories;
using Domain.GenreAggregate.ValueObjects;
using Domain.SharedKernel.Entities;
using Domain.SharedKernel.ValueObjects;

namespace Application.Genres.Commands.AddGenre;

public sealed record AddGenreCommand(
    string Name,
    string ImageUrl
    ) : ICommand;

public sealed class AddGenreCommandHandler(IGenreRepository repository, IUnitOfWork unitOfWork) : ICommandHandler<AddGenreCommand>
{
    private readonly IGenreRepository _repository = repository;
    private readonly IUnitOfWork _unitOfWork = unitOfWork;

    public async Task<Result> Handle(AddGenreCommand request, CancellationToken cancellationToken)
    {
        //var id = new GenreId(Guid.NewGuid());
        //var nameResult = GenreName.Create(request.Name);
        //var imageUrlResult = ImageUrl.Create(request.ImageUrl);
        //var imageId = 
        //var image = Image.Create(imageUrl);

        //var genreResult = Genre.Create(id, name, image);

        //_repository.AddAsync();

        await _unitOfWork.Commit(cancellationToken);
        return Result.Success();
    }
}
