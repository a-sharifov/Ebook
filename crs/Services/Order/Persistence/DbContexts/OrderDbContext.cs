using Domain.Core.Events;
using Domain.Core.Events.Interfaces;
using Microsoft.EntityFrameworkCore.Storage;
using Persistence.Configurations;

namespace Persistence.DbContexts;

public class OrderDbContext : DbContext
{
    public DbSet<Order> Orders { get; set; }
    public DbSet<OrderItem> OrderItems { get; set; }


#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider adding the 'required' modifier or declaring as nullable.
    public OrderDbContext()
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider adding the 'required' modifier or declaring as nullable.
    {
    }

#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider adding the 'required' modifier or declaring as nullable.
    public OrderDbContext(DbContextOptions<OrderDbContext> options) : base(options)
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider adding the 'required' modifier or declaring as nullable.
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder) =>
        modelBuilder
        .Ignore<IList<IDomainEvent>>()
        .ApplyConfigurationsFromAssembly(AssemblyReference.Assembly);

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder) =>
        optionsBuilder.UseNpgsql();
}
