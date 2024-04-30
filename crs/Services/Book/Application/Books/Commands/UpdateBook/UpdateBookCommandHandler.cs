using Domain.AuthorAggregate.Ids;
using Domain.AuthorAggregate.ValueObjects;
using Domain.AuthorAggregate;
using Domain.BookAggregate;
using Domain.BookAggregate.Errors;
using Domain.BookAggregate.Ids;
using Domain.BookAggregate.Repositories;
using Domain.BookAggregate.ValueObjects;
using Domain.Core.UnitOfWorks.Interfaces;
using Domain.GenreAggregate.Ids;
using Domain.LanguageAggregate.Ids;
using Domain.SharedKernel.Ids;
using Domain.SharedKernel.ValueObjects;
using Domain.AuthorAggregate.Repositories;
using Domain.GenreAggregate.Repositories;
using Domain.LanguageAggregate.Repositories;
using Domain.SharedKernel.Repositories;
using MediatR;
using Application.Images.Commands.AddImage;
using Domain.SharedKernel.Enumerations;

namespace Application.Books.Commands.UpdateBook;

//TODO: change logic.
internal sealed class UpdateBookCommandHandler(
    IBookRepository bookRepository,
    ISender sender,
    IAuthorRepository authorRepository,
    IGenreRepository genreRepository,
    ILanguageRepository languageRepository,
    IImageRepository imageRepository,
    IUnitOfWork unitOfWork) : ICommandHandler<UpdateBookCommand>
{
    private readonly ISender _sender = sender;
    private readonly IAuthorRepository _authorRepository = authorRepository;
    private readonly IGenreRepository _genreRepository = genreRepository;
    private readonly ILanguageRepository _languageRepository = languageRepository;
    private readonly IImageRepository _imageRepository = imageRepository;
    private readonly IBookRepository _bookRepository = bookRepository;
    private readonly IUnitOfWork _unitOfWork = unitOfWork;

    public async Task<Result> Handle(UpdateBookCommand request, CancellationToken cancellationToken)
    {
        var id = new BookId(request.Id);
        var isExist = await _bookRepository.IsExistAsync(id, cancellationToken);

        if (!isExist)
        {
            return Result.Failure(
                BookErrors.BookIsNotExist);
        }

        var addImageCommand =
          new AddImageCommand(ImageBucket.Books, request.PosterStream, request.Poster);

        var imageDtoResult = await _sender.Send(addImageCommand, cancellationToken);

        if (imageDtoResult.IsFailure)
        {
            return imageDtoResult;
        }

        var imageId = imageDtoResult.Value.Id;

        var updateBookResult = await UpdateBookAsync(request, imageId, cancellationToken);

        if (updateBookResult.IsFailure)
        {
            return updateBookResult;
        }

        var updateBook = updateBookResult.Value;

        await _bookRepository.UpdateAsync(updateBook, cancellationToken);
        await _unitOfWork.Commit(cancellationToken);

        return Result.Success();
    }

    private async Task<Result<Book>> UpdateBookAsync(
    UpdateBookCommand request,
    Guid existImageId,
    CancellationToken cancellationToken)
    {
        var id = new BookId(request.Id);
        var title = Title.Create(request.Title).Value;
        var description = BookDescription.Create(request.Description).Value;
        var pageCount = PageCount.Create(request.PageCount).Value;
        var price = Money.Create(request.Price).Value;
        var quantity = QuantityBook.Create(request.Quantity).Value;
        var soldUnits = SoldUnits.Create(0).Value;

        var languageId = new LanguageId(request.LanguageId);
        var language = await _languageRepository.GetAsync(languageId, cancellationToken: cancellationToken);

        var genreId = new GenreId(request.GenreId);
        var genre = await _genreRepository.GetAsync(genreId, cancellationToken: cancellationToken);

        var imageId = new ImageId(existImageId);
        var image = await _imageRepository.GetAsync(imageId, cancellationToken: cancellationToken);
        await _imageRepository.UpdateAsync(image, cancellationToken);

        var pseudonym = Pseudonym.Create(request.AuthorPseudonym).Value;
        var isAuthorExists = await _authorRepository.IsExistAsync(pseudonym, cancellationToken);

        var author =
            isAuthorExists ? await _authorRepository.GetAsync(pseudonym, cancellationToken) :
            Author.Create(new AuthorId(Guid.NewGuid()), pseudonym).Value;

        var book = Book.Create(
            id, title, description, pageCount, price, language, quantity, soldUnits, author, image, genre).Value;

        return book;
    }

}