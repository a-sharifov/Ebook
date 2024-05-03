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
using Domain.GenreAggregate;
using Domain.GenreAggregate.Errors;
using Domain.GenreAggregate.Ids;
using Domain.GenreAggregate.Repositories;
using Domain.LanguageAggregate;
using Domain.LanguageAggregate.Errors;
using Domain.LanguageAggregate.Ids;
using Domain.LanguageAggregate.Repositories;
using Domain.SharedKernel.Enumerations;
using Domain.SharedKernel.Errors;
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
        var languageResult = await GetLanguageAsync(request.LanguageId, cancellationToken);

        if (languageResult.IsFailure)
        {
            return Result.Failure<Book>(languageResult.Error);
        }

        var genreResult = await GetGenreAsync(request.GenreId, cancellationToken);

        if (genreResult.IsFailure)
        {
            return Result.Failure<Book>(genreResult.Error);
        }

        var imageResult = await GetImageAsync(existImageId, cancellationToken);

        if (imageResult.IsFailure)
        {
            return Result.Failure<Book>(imageResult.Error);
        }

        var id = new BookId(Guid.NewGuid());
        var title = Title.Create(request.Title).Value;
        var description = BookDescription.Create(request.Description).Value;
        var pageCount = PageCount.Create(request.PageCount).Value;
        var price = Money.Create(request.Price).Value;
        var quantity = QuantityBook.Create(request.Quantity).Value;
        var soldUnits = SoldUnits.Create(0).Value;
        var authorResult = await GetAuthorAsync(request.AuthorPseudonym, cancellationToken);
     
        var book = Book.Create(
            id, title, description, pageCount, price, languageResult.Value, quantity, soldUnits, authorResult.Value, imageResult.Value, genreResult.Value).Value;

        return book;
    }

    private async Task<Result<Language>> GetLanguageAsync(Guid languageId, CancellationToken cancellationToken)
    {
        var id = new LanguageId(languageId);
        var languageIsExist = await _languageRepository.IsExistAsync(id, cancellationToken: cancellationToken);

        if (!languageIsExist)
        {
            return Result.Failure<Language>(LanguageErrors.IsNotExist);
        }

        var language = await _languageRepository.GetAsync(id, cancellationToken: cancellationToken);

        return language;
    }

    private async Task<Result<Genre>> GetGenreAsync(Guid genreId, CancellationToken cancellationToken)
    {
        var id = new GenreId(genreId);
        var genreIsExist = await _genreRepository.IsExistAsync(id, cancellationToken: cancellationToken);

        if (!genreIsExist)
        {
            return Result.Failure<Genre>(GenreErrors.IsNotExist);
        }

        var genre = await _genreRepository.GetAsync(id, cancellationToken: cancellationToken);
        return Result.Success(genre);
    }

    private async Task<Result<Image>> GetImageAsync(Guid imageId, CancellationToken cancellationToken)
    {
        var id = new ImageId(imageId);
        var imageIsExist = await _imageRepository.IsExistAsync(id, cancellationToken: cancellationToken);

        if (!imageIsExist)
        {
            return Result.Failure<Image>(ImageErrors.IsNotExist);
        }

        var image = await _imageRepository.GetAsync(id, cancellationToken: cancellationToken);
        return Result.Success(image);
    }

    private async Task<Result<Author>> GetAuthorAsync(string authorPseudonym, CancellationToken cancellationToken)
    {
        var pseudonym = Pseudonym.Create(authorPseudonym).Value;
        var isAuthorExists = await _authorRepository.IsExistAsync(pseudonym, cancellationToken);

        var author =
            isAuthorExists ? await _authorRepository.GetAsync(pseudonym, cancellationToken) :
            Author.Create(new AuthorId(Guid.NewGuid()), pseudonym).Value;

        return author;
    }
}