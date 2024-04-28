using Application.Images.Commands.AddImage;
using Domain.AuthorAggregate;
using Domain.AuthorAggregate.Ids;
using Domain.AuthorAggregate.Repositories;
using Domain.AuthorAggregate.ValueObjects;
using Domain.BookAggregate;
using Domain.BookAggregate.Ids;
using Domain.BookAggregate.Repositories;
using Domain.BookAggregate.ValueObjects;
using Domain.Core.UnitOfWorks.Interfaces;
using Domain.GenreAggregate.Ids;
using Domain.GenreAggregate.Repositories;
using Domain.LanguageAggregate.Ids;
using Domain.LanguageAggregate.Repositories;
using Domain.SharedKernel.Enumerations;
using Domain.SharedKernel.Ids;
using Domain.SharedKernel.Repositories;
using Domain.SharedKernel.ValueObjects;
using MediatR;

namespace Application.Books.Commands.AddBook;

internal sealed class AddBookCommandHandler(
    ISender sender,
    IUnitOfWork unitOfWork,
    IBookRepository bookRepository,
    IAuthorRepository authorRepository,
    IGenreRepository genreRepository,
    ILanguageRepository languageRepository,
    IImageRepository imageRepository)
    : ICommandHandler<AddBookCommand>
{
    private readonly ISender _sender = sender;
    private readonly IAuthorRepository _authorRepository = authorRepository;
    private readonly IGenreRepository _genreRepository = genreRepository;
    private readonly ILanguageRepository _languageRepository = languageRepository;
    private readonly IImageRepository _imageRepository = imageRepository;
    private readonly IBookRepository _bookRepository = bookRepository;
    private readonly IUnitOfWork _unitOfWork = unitOfWork;

    public async Task<Result> Handle(AddBookCommand request, CancellationToken cancellationToken)
    {
        var addImageCommand =
            new AddImageCommand(ImageBucket.Books, request.PosterStream, request.Poster);

        var imageDtoResult = await _sender.Send(addImageCommand, cancellationToken);

        if (imageDtoResult.IsFailure)
        {
            return imageDtoResult;
        }

        var imageId = imageDtoResult.Value.Id;

        var bookResult = await CreateBookAsync(request, imageId, cancellationToken);

        if (bookResult.IsFailure)
        {
            return bookResult;
        }

        var book = bookResult.Value;

        await _bookRepository.AddAsync(book, cancellationToken);
        await _unitOfWork.Commit(cancellationToken);

        return Result.Success();
    }

    private async Task<Result<Book>> CreateBookAsync(
        AddBookCommand request,
        Guid existImageId,
        CancellationToken cancellationToken)
    {
        var id = new BookId(Guid.NewGuid());
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

        var pseudonym = Pseudonym.Create(request.AuthorPseudonym).Value;
        var isAuthorExists = await _authorRepository.IsExistsAsync(pseudonym, cancellationToken);

        var author =
            isAuthorExists ? await _authorRepository.GetAsync(pseudonym, cancellationToken) :
            Author.Create(new AuthorId(Guid.NewGuid()), pseudonym).Value;

        var book = Book.Create(
            id, title, description, pageCount, price, language, quantity, soldUnits, author, image, genre).Value;

        return book;
    }
}