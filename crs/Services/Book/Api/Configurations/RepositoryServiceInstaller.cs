using Api.Core.ServiceInstaller.Interfaces;
using Domain.AuthorAggregate.Repositories;
using Domain.BookAggregate.Repositories;
using Domain.CartAggregate.Repositories;
using Domain.GenreAggregate.Repositories;
using Domain.LanguageAggregate.Repositories;
using Domain.SharedKernel.Repositories;
using Domain.UserAggregate.Repositories;
using Domain.WishAggregate.Repositories;
using Persistence.Repositories;

namespace Api.Configurations;

internal sealed class RepositoryServiceInstaller : IServiceInstaller
{
    public void Install(IServiceCollection services, IConfiguration configuration)
    {
        services.AddTransient<IAuthorRepository, AuthorRepository>();
        services.AddTransient<IBookRepository, BookRepository>();
        services.AddTransient<ICartRepository, CartRepository>();
        services.AddTransient<ICartItemRepository, CartItemRepository>();
        services.AddTransient<IGenreRepository, GenreRepository>();
        services.AddTransient<ILanguageRepository, LanguageRepository>();
        services.AddTransient<IUserRepository, UserRepository>();
        services.AddTransient<IImageRepository, ImageRepository>();
        services.AddTransient<IWishRepository, WishRepository>();
    }
}
