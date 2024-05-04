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
using Infrastructure.FileStorages.Interfaces;
using Persistence.DbContexts;
using Domain.SharedKernel.Entities;
using Domain.UserAggregate;
using Contracts.Enumerations;
using Domain.UserAggregate.Ids;
using Domain.UserAggregate.ValueObjects;
using Infrastructure.Hashing.Interfaces;
using Domain.WishAggregate.Ids;
using Domain.WishAggregate;
using Domain.CartAggregate.Ids;
using Domain.CartAggregate;
using Domain.AuthorAggregate;
using Domain.AuthorAggregate.Ids;
using Domain.AuthorAggregate.ValueObjects;
using Domain.BookAggregate;
using Domain.BookAggregate.Ids;
using Domain.BookAggregate.ValueObjects;

namespace Infrastructure.Seeds;

public class SeedDefaultProject(BookDbContext dbContext, IFileService fileService, IHashingService hashingService) : ISeeder
{
    private readonly BookDbContext _dbContext = dbContext;
    private readonly IFileService _fileService = fileService;
    private readonly IHashingService _hashingService = hashingService;

    public void Seed()
    {
        var isEmpty = _dbContext.Languages.Any();

        if (isEmpty)
        {
            return;
        }

        AddDefaultBuckets();
        UploadBookImages();

        AddUser("admin@gmail.com", "admin", "admin", "admin", Role.Admin);
        AddUser("user@gmail.com", "user", "user", "user", Role.User);

        var LiAuthor = AddAuthor("Li");
        var BenAuthor = AddAuthor("Ben");
        var StivenAuthor = AddAuthor("Stiven");

        var englishLanguage = AddLanguage("English", "en");
        var russianLanguage = AddLanguage("Russian", "ru");
        var azerbaijaniLanguage = AddLanguage("Azerbaijani", "az");

        var fantasyGenre = AddGenre("Fantasy");
        var classicGenre = AddGenre("Classic");
        var romanceGenre = AddGenre("Romance");
        var horrorGenre = AddGenre("Horror");

        var book1 = AddBook("Book1", "Description for Book1", 200, 9.99m, englishLanguage, 100, 10, LiAuthor, AddImage(ImageBucket.Books, "default.jpg", true), fantasyGenre);
        var book2 = AddBook("Book2", "Description for Book2", 300, 14.99m, russianLanguage, 80, 15, BenAuthor, AddImage(ImageBucket.Books, "default.jpg", true), classicGenre);
        var book3 = AddBook("Book3", "Description for Book3", 150, 7.99m, azerbaijaniLanguage, 60, 5, StivenAuthor, AddImage(ImageBucket.Books, "default.jpg", true), romanceGenre);
        var book4 = AddBook("Book4", "Description for Book4", 250, 12.99m, englishLanguage, 70, 8, BenAuthor, AddImage(ImageBucket.Books, "default.jpg", true), horrorGenre);
        var book5 = AddBook("Book5", "Description for Book5", 180, 8.99m, russianLanguage, 120, 20, LiAuthor, AddImage(ImageBucket.Books, "default.jpg", true), fantasyGenre);
        var book6 = AddBook("Book6", "Description for Book6", 270, 11.99m, englishLanguage, 90, 12, StivenAuthor, AddImage(ImageBucket.Books, "default.jpg", true), classicGenre);
        var book7 = AddBook("Book7", "Description for Book7", 220, 10.99m, russianLanguage, 85, 6, BenAuthor, AddImage(ImageBucket.Books, "default.jpg", true), romanceGenre);
        var book8 = AddBook("Book8", "Description for Book8", 190, 9.49m, azerbaijaniLanguage, 95, 7, LiAuthor, AddImage(ImageBucket.Books, "default.jpg", true), horrorGenre);
        var book9 = AddBook("Book9", "Description for Book9", 230, 13.99m, englishLanguage, 75, 9, StivenAuthor, AddImage(ImageBucket.Books, "default.jpg", true), classicGenre);
        var book10 = AddBook("Book10", "Description for Book10", 160, 8.49m, russianLanguage, 110, 11, BenAuthor, AddImage(ImageBucket.Books, "default.jpg", true), romanceGenre);


        _dbContext.SaveChanges();
    }

    private void UploadBookImages()
    {
        var defaultBookImagesPath = Path.Combine(
           AssemblyReference.AssemblyPath, "Seeds", "images", "books");

        _fileService.UploadFilesInBasePath(ImageBucket.Books, defaultBookImagesPath);
    }

    private void AddDefaultBuckets()
    {
        var buckets = ImageBucket.GetNames();

        foreach (var bucket in buckets)
        {
            _fileService.CreateBucket(bucket);
            _fileService.AddDefaultPolicyBucket(bucket);
        }
    }

    /////////////////////////////////////////////////////////////

    private User AddUser(string email, string firstName, string lastName, string password, Role role)
    {
        var id = Guid.NewGuid();
    
        var user = CreateUser(id, email, firstName, lastName, password, role);
        _dbContext.Add(user);
        return user;
    }

    private Image AddImage(ImageBucket bucket, string name, bool isUnigueName = false)
    {
        var id = Guid.NewGuid();
        var image = CreateImage(id, bucket, name, isUnigueName);
        _dbContext.Add(image);
        return image;
    }

    private Language AddLanguage(string name, string code)
    {
        var id = Guid.NewGuid();
        var language = CreateLanguage(id, name, code);
        _dbContext.Add(language);
        return language;
    }

    private Genre AddGenre(string name)
    {
        var id = Guid.NewGuid();
        var genre = CreateGenre(id, name);
        _dbContext.Add(genre);
        return genre;
    }

    private Book AddBook(string title,
        string description,
        int pageCount,
        decimal price,
        Language language,
        int quantity,
        int soldUnits,
        Author author,
        Image image,
        Genre genre)
    {
        var id = Guid.NewGuid();
        var book = CreateBook(id, title, description, pageCount, price, language, quantity, soldUnits, author, image, genre);
        _dbContext.Add(book);
        return book;
    }

    private Author AddAuthor(string pseudonym)
    {
        var id = Guid.NewGuid();
        var author = CreateAuthor(id, pseudonym);
        _dbContext.Add(author);
        return author;
    }

    private Author CreateAuthor(Guid id, string pseudonym)
    {
        var authorId = new AuthorId(id);
        var authorPseudonym = Pseudonym.Create(pseudonym).Value;
        var author = Author.Create(authorId, authorPseudonym).Value;
        return author;
    }

    private User CreateUser(Guid id, string email, string firstName, string lastName, string password, Role role)
    {
        var userId = new UserId(id);
        var userEmail = Email.Create(email).Value;
        var userFirstName = FirstName.Create(firstName).Value;
        var userLastName = LastName.Create(lastName).Value;

        var generateSalt = _hashingService.GenerateSalt();
        var passwordSalt = PasswordSalt.Create(generateSalt).Value;

        var hash = _hashingService.Hash(password, generateSalt);
        var passwordHash = PasswordHash.Create(hash).Value;

        var wishId = new WishId(Guid.NewGuid());
        var wish = Wish.Create(wishId, userId).Value;

        var cartId = new CartId(Guid.NewGuid());
        var cart = Cart.Create(cartId, userId).Value;

        var user = User.Create(
            userId,
            userEmail,
            userFirstName,
            userLastName,
            passwordHash,
            passwordSalt,
            role,
            cart,
            wish).Value;

        return user;
    }

    private static Language CreateLanguage(Guid id, string name, string code)
    {
        var languageId = new LanguageId(id);
        var languageName = LanguageName.Create(name).Value;
        var languageCode = LanguageCode.Create(code).Value;
        var language = Language.Create(languageId, languageName, languageCode).Value;
        return language;
    }

    private Image CreateImage(Guid id, ImageBucket bucket, string name, bool isUniqueName = false)
    {
        var imageId = new ImageId(id);
        var imageName = ImageName.Create(name, isUniqueName).Value;
        var permanentUrl = _fileService.GeneratePermanentUrl(bucket.Name, name);
        var url = ImageUrl.Create(permanentUrl).Value;
        var image = Image.Create(imageId, bucket, imageName, url).Value;
        return image;
    }

    private static Genre CreateGenre(Guid id, string name)
    {
        var genreId = new GenreId(id);
        var genreName = GenreName.Create(name).Value;
        var genre = Genre.Create(genreId, genreName).Value;
        return genre;
    }

    private Book CreateBook(
        Guid id, 
        string title, 
        string description, 
        int pageCount, 
        decimal price, 
        Language language,
        int quantity,
        int soldUnits, 
        Author author, 
        Image image, 
        Genre genre)
    {
        var bookId = new BookId(id);
        var bookTitle = Title.Create(title).Value;
        var bookDescription = BookDescription.Create(description).Value;
        var bookPageCount = PageCount.Create(pageCount).Value;
        var bookPrice = Money.Create(price).Value;
        var quantityBook = QuantityBook.Create(quantity).Value;
        var bookSoldUnits = SoldUnits.Create(soldUnits).Value;

        var book = Book.Create(
            bookId, 
            bookTitle, 
            bookDescription, 
            bookPageCount, 
            bookPrice, 
            language, 
            quantityBook, 
            bookSoldUnits, 
            author, 
            image, 
            genre).Value;

        return book;
    }


}
