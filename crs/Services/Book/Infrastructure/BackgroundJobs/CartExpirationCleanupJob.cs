using Domain.CartAggregate;
using Microsoft.EntityFrameworkCore;
using Persistence.DbContexts;
using Quartz;

namespace Infrastructure.BackgroundJobs;

[DisallowConcurrentExecution]
public sealed class CartExpirationCleanupJob(BookDbContext dbContext) : IJob
{
    private readonly BookDbContext _dbContext = dbContext;

    public async Task Execute(IJobExecutionContext context)
    {
        //var cart = await _dbContext
        //    .Set<Cart>()
        //    .Where(x => x.ExpirationTime != null && x.ExpirationTime > DateTime.UtcNow)
        //    .Include(x => x.Items)
        //    .ThenInclude(x => x.Book)
        //    .FirstOrDefaultAsync(context.CancellationToken);

        //if (cart is null)
        //{
        //    return;
        //}

        //cart.Clear();
        //await _dbContext.SaveChangesAsync(context.CancellationToken);
    
    }
}
