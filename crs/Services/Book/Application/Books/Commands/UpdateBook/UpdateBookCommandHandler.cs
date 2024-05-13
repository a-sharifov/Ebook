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
using Domain.LanguageAggregate;
using Domain.LanguageAggregate.Errors;
using Domain.GenreAggregate.Errors;
using Domain.GenreAggregate;
using Domain.SharedKernel.Errors;

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

        var book = await _bookRepository.GetAsync(id, cancellationToken);
        var updateBookResult = await UpdateBookAsync(book, request, imageId, cancellationToken);

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
        Book book,
        UpdateBookCommand request,
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

        var authorResult = await GetAuthorAsync(request.AuthorPseudonym, cancellationToken);

        var updateBookResult = UpdateBook(
            book,
            request,
            languageResult.Value,
            authorResult.Value,
            imageResult.Value,
            genreResult.Value,
            existImageId);

        return updateBookResult;
    }

    private static Result<Book> UpdateBook(
        Book book,
        UpdateBookCommand request,
        Language language,
        Author author,
        Image image,
        Genre genre,
        Guid existImageId)
    {
        var titleResult = Title.Create(request.Title);
        var descriptionResult = BookDescription.Create(request.Description);
        var pageCountResult = PageCount.Create(request.PageCount);
        var priceResult = Money.Create(request.Price);
        var quantityResult = QuantityBook.Create(request.Quantity);
        var soldUnitsResult = SoldUnits.Create(0);

        var firstFailureOrSuccess = Result.FirstFailureOrSuccess(
            titleResult, descriptionResult, pageCountResult, priceResult, quantityResult, soldUnitsResult);

        if(firstFailureOrSuccess.IsFailure)
        {
            return Result.Failure<Book>(firstFailureOrSuccess.Error);
        }

         book.Update(
            titleResult.Value, 
            descriptionResult.Value, 
            pageCountResult.Value,
            priceResult.Value,
            language, 
            quantityResult.Value, 
            soldUnitsResult.Value, 
            author, 
            image, 
            genre);

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
        var pseudonymResult = Pseudonym.Create(authorPseudonym);

        if (pseudonymResult.IsFailure)
        {
            return Result.Failure<Author>(pseudonymResult.Error);
        }

        var pseudonym = pseudonymResult.Value;

        var isAuthorExists = await _authorRepository.IsExistAsync(pseudonym, cancellationToken);

        var author =
            isAuthorExists ? await _authorRepository.GetAsync(pseudonym, cancellationToken) :
            Author.Create(new AuthorId(Guid.NewGuid()), pseudonym).Value;

        return author;
    }

}