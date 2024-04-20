using Application.Images.Commands.AddImage;
using Domain.BookAggregate;
using Domain.BookAggregate.Repositories;
using Domain.Core.UnitOfWorks.Interfaces;
using MediatR;

namespace Application.Books.Commands.AddBook;

internal sealed class AddBookCommandHandler(
    ISender sender,
    IUnitOfWork unitOfWork,
    IBookRepository repository)
    : ICommandHandler<AddBookCommand>
{
    private readonly ISender _sender = sender;
    private readonly IBookRepository _repository = repository;
    private readonly IUnitOfWork _unitOfWork = unitOfWork;

    public async Task<Result> Handle(AddBookCommand request, CancellationToken cancellationToken)
    {
        //var addImageCommand = 
        //    new AddImageCommand("", request.PosterStream, request.Poster);

        //var addImageCommandResult = await _sender.Send(addImageCommand, cancellationToken);

        //if (addImageCommandResult.IsFailure)
        //{
        //    return addImageCommandResult;
        //}

        //var bookDto = addImageCommandResult.Value;

        //await _repository.AddAsync();
        //await _unitOfWork.Commit(cancellationToken);

        return Result.Success();
    }

    //private static async Task<Result<Book>> CreateBookAsync()
    //{



    //}
}