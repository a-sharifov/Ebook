using Api.Core.ServiceInstaller.Interfaces;
using Catalog.Persistence.Caching.Abstractions;
using Catalog.Persistence.Caching;
using Domain.AuthorAggregate.Ids;
using Domain.AuthorAggregate;
using Domain.BookAggregate.Ids;
using Domain.BookAggregate;
using Domain.CartAggregate.Ids;
using Domain.CartAggregate;
using Domain.GenreAggregate.Ids;
using Domain.GenreAggregate;
using Domain.LanguageAggregate.Ids;
using Domain.LanguageAggregate;
using Domain.UserAggregate.Ids;
using Domain.UserAggregate;
using Domain.SharedKernel.Ids;
using Domain.SharedKernel.Entities;
using Domain.WishAggregate.Ids;
using Domain.WishAggregate;
using Domain.CartAggregate.Entities;

namespace Api.Configurations;

internal sealed class CachedServiceInstaller : IServiceInstaller
{
    public void Install(IServiceCollection services, IConfiguration configuration)
    {
        services.AddTransient<ICachedEntityService<Author, AuthorId>, CachedEntityService<Author, AuthorId>>();
        services.AddTransient<ICachedEntityService<Book, BookId>, CachedEntityService<Book, BookId>>();
        services.AddTransient<ICachedEntityService<Cart, CartId>, CachedEntityService<Cart, CartId>>();
        services.AddTransient<ICachedEntityService<CartItem, CartItemId>, CachedEntityService<CartItem, CartItemId>>();
        services.AddTransient<ICachedEntityService<Genre, GenreId>, CachedEntityService<Genre, GenreId>>();
        services.AddTransient<ICachedEntityService<Language, LanguageId>, CachedEntityService<Language, LanguageId>>();
        services.AddTransient<ICachedEntityService<User, UserId>, CachedEntityService<User, UserId>>();
        services.AddTransient<ICachedEntityService<Image, ImageId>, CachedEntityService<Image, ImageId>>();
        services.AddTransient<ICachedEntityService<Wish, WishId>, CachedEntityService<Wish, WishId>>();
    }
}
