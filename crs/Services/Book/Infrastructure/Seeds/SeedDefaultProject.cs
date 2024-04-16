using Domain.AuthorAggregate.Ids;
using Domain.AuthorAggregate.ValueObjects;
using Domain.AuthorAggregate;
using Domain.GenreAggregate.Ids;
using Domain.GenreAggregate.ValueObjects;
using Domain.GenreAggregate;
using Domain.LanguageAggregate.Ids;
using Domain.LanguageAggregate.ValueObjects;
using Domain.LanguageAggregate;
using Domain.SharedKernel.Enumerations;
using Domain.SharedKernel.Ids;
using Domain.SharedKernel.ValueObjects;
using Infrastructure.Core.Seeds;
using Infrastructure.FileStorage.Interfaces;
using Persistence.DbContexts;
using Domain.SharedKernel.Entities;

namespace Infrastructure.Seeds;

public class SeedDefaultProject(BookDbContext dbContext, IFileService fileService) : ISeeder
{
    private readonly BookDbContext _dbContext = dbContext;
    private readonly IFileService _fileService = fileService;

    public void Seed()
    {
        var isEmpty = _dbContext.Languages.Any();

        if (isEmpty)
        {
            return;
        }

        AddBuckets();
        UploadGenresImages();

        AddLanguages();
        AddGenres();

        _dbContext.SaveChanges();
    }

    private void AddBuckets()
    {
        var buckets = ImageBucket.GetNames();

        foreach (var bucket in buckets)
        {
            _fileService.CreateBucket(bucket);
            _fileService.AddDefaultPolicyBucket(bucket);
        }
    }

    private void AddLanguages()
    {

        AddLanguage("English", "en");
        AddLanguage("Russian", "ru");
        AddLanguage("Azerbaijani", "az");
    }

    private void UploadGenresImages()
    {
        var defaultGenreImagesPath = Path.Combine(
           AssemblyReference.AssemblyPath, "Seeds", "images", "genres");

            _fileService.UploadFilesInBasePath(ImageBucket.Genres, defaultGenreImagesPath);
    }

    private void AddGenres()
    {
        AddGenre("Fantasy");
        AddGenre("Classic");
        AddGenre("Romance");
        AddGenre("Horror");
    }

    /////////////////////////////////////////////////////////////

    private Language AddLanguage(string name, string code)
    {
        var id = Guid.NewGuid();
        var language = CreateLanguage(id, name, code);
        _dbContext.Add(language);
        return language;
    }

    private static Language CreateLanguage(Guid id, string name, string code)
    {
        var languageId = new LanguageId(id);
        var languageName = LanguageName.Create(name).Value;
        var languageCode = LanguageCode.Create(code).Value;
        var language = Language.Create(languageId, languageName, languageCode).Value;
        return language;
    }

    private Image CreateImage(Guid id, ImageBucket bucket, string name, bool isUnigueName = false)
    {
        var imageId = new ImageId(id);
        var imageName = ImageName.Create(name, isUnigueName).Value;
        var permanentUrl = _fileService.GeneratePermanentUrl(bucket.Name, name);
        var url = ImageUrl.Create(permanentUrl).Value;
        var image = Image.Create(imageId, bucket, imageName, url).Value;
        return image;
    }

    private Image AddImage(ImageBucket bucket, string name, bool isUnigueName = false)
    {
        var id = Guid.NewGuid();
        var image = CreateImage(id, bucket, name, isUnigueName);
        _dbContext.Add(image);
        return image;
    }

    private static Genre CreateGenre(Guid id, string name)
    {
        var genreId = new GenreId(id);
        var genreName = GenreName.Create(name).Value;
        var genre = Genre.Create(genreId, genreName).Value;
        return genre;
    }

    private Genre AddGenre(string name)
    {
        var id = Guid.NewGuid();
        var genre = CreateGenre(id, name);
        _dbContext.Add(genre);
        return genre;
    }

    private static Author CreateAuthor(Guid id, string firstName, string lastName, string pseudonym, Image image, string description)
    {
        var authorId = new AuthorId(id);
        var authorFirstName = FirstName.Create(firstName).Value;
        var authorLastNameAuthor = LastName.Create(lastName).Value;
        var authorPseudonymAuthor = Pseudonym.Create(pseudonym).Value;
        var authorDescprition = AuthorDescription.Create(description).Value;
        var author = Author.Create(authorId, authorFirstName, authorLastNameAuthor, authorPseudonymAuthor, image, authorDescprition).Value;
        return author;
    }

    //private Author AddAuthor(string firstName, string lastName, string pseudonym, Image image, string description)
    //{
    //    var id = Guid.NewGuid();
    //    var author = CreateAuthor(id, firstName, lastName, pseudonym, image, description);
    //    _dbContext.Add(author);
    //    return author;
    //}
}
