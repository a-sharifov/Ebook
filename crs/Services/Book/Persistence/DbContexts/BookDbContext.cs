using Domain.Core.Events.Interfaces;
using Domain.UserAggregate;

namespace Persistence.DbContexts;

public sealed class BookDbContext(DbContextOptions<BookDbContext> options) : DbContext(options)
{
    public DbSet<User> Users { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder) =>
        modelBuilder
        .Ignore<IList<IDomainEvent>>()
        .ApplyConfigurationsFromAssembly(AssemblyReference.Assembly);
}
