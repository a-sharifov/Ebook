using Domain.Core.UnitOfWorks.Interfaces;
using Persistence.DbContexts;

namespace Persistence;

public sealed class UnitOfWork(BookDbContext bookDbContext) : IUnitOfWork
{
    private readonly BookDbContext _bookDbContext = bookDbContext;

    public Task<int> Commit(CancellationToken cancellationToken = default) => 
        _bookDbContext.SaveChangesAsync(cancellationToken);
}
