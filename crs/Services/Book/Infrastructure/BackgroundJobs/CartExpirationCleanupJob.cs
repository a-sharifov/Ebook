using Domain.CartAggregate;
using Microsoft.EntityFrameworkCore;
using Persistence.DbContexts;
using Persistence.Repositories;
using Quartz;

namespace Infrastructure.BackgroundJobs;

[DisallowConcurrentExecution]
public sealed class CartExpirationCleanupJob(BookDbContext dbContext) : IJob
{
    private readonly BookDbContext _dbContext = dbContext;

    public async Task Execute(IJobExecutionContext context)
    {
        var cart = await _dbContext
            .Set<Cart>()
            .AsNoTracking()
            .Where(x => x.ExpirationTime != null && x.ExpirationTime > DateTime.UtcNow)
            .FirstOrDefaultAsync();

        if(cart is null)
        {
            return;
        }

        await _dbContext.CartItems.Where(x => x.CartId == cart.Id)
            .ExecuteDeleteAsync();

        await _dbContext.SaveChangesAsync(context.CancellationToken);
    }
}
