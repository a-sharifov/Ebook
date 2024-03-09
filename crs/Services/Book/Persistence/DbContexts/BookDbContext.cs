using Domain.AuthorAggregate;
using Domain.BookAggregate;
using Domain.CartAggregate;
using Domain.Core.Events.Interfaces;
using Domain.GenreAggregate;
using Domain.LanguageAggregate;
using Domain.UserAggregate;

namespace Persistence.DbContexts;

public class BookDbContext(DbContextOptions<BookDbContext> options) : DbContext(options)
{
    public DbSet<User> Users { get; set; }
    public DbSet<Genre> Genres { get; set; }
    public DbSet<Language> Languages { get; set; }
    public DbSet<Book> Books { get; set; }
    public DbSet<Author> Authors { get; set; }
    public DbSet<Cart> Carts { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder) =>
        modelBuilder
        .Ignore<IList<IDomainEvent>>()
        .ApplyConfigurationsFromAssembly(AssemblyReference.Assembly);
}
