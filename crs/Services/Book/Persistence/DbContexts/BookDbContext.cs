using Domain.AuthorAggregate;
using Domain.BookAggregate;
using Domain.CartAggregate;
using Domain.Core.Events.Interfaces;
using Domain.GenreAggregate;
using Domain.LanguageAggregate;
using Domain.SharedKernel.Entities;
using Domain.UserAggregate;
using Domain.UserAggregate.Entities;

namespace Persistence.DbContexts;

public class BookDbContext : DbContext
{
    public DbSet<User> Users { get; set; }
    public DbSet<Genre> Genres { get; set; }
    public DbSet<Language> Languages { get; set; }
    public DbSet<Book> Books { get; set; }
    public DbSet<Author> Authors { get; set; }
    public DbSet<Cart> Carts { get; set; }
    public DbSet<Wish> Wishes { get; set; }
    public DbSet<Image> Images { get; set; }

    // if you need migration in Persistence layer.
    public BookDbContext()
    {
    }

    public BookDbContext(DbContextOptions<BookDbContext> options) : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder) =>
        modelBuilder
        .Ignore<IList<IDomainEvent>>()
        .ApplyConfigurationsFromAssembly(AssemblyReference.Assembly);

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder) => 
        optionsBuilder.UseNpgsql();
}
