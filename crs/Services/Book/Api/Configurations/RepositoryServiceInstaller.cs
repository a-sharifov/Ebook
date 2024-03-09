using Api.Core.ServiceInstaller.Interfaces;
using Domain.AuthorAggregate.Repositories;
using Domain.BookAggregate.Repositories;
using Domain.CartAggregate.Repositories;
using Domain.GenreAggregate.Repositories;
using Domain.LanguageAggregate.Repositories;
using Domain.UserAggregate.Repositories;
using Persistence.Repositories;

namespace Api.Configurations;

internal sealed class RepositoryServiceInstaller : IServiceInstaller
{
    public void Install(IServiceCollection services, IConfiguration configuration)
    {
        services.AddTransient<IAuthorRepository, AuthorRepository>();
        services.AddTransient<IBookRepository, BookRepository>();
        services.AddTransient<ICartRepository, CartRepository>();
        services.AddTransient<IGenreRepository, GenreRepository>();
        services.AddTransient<ILanguageRepository, LanguageRepository>();
        services.AddTransient<IUserRepository, UserRepository>();
    }
}
