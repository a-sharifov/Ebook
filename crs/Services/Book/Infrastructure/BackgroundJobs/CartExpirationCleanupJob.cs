using Domain.CartAggregate.Repositories;
using Domain.Core.UnitOfWorks.Interfaces;
using Quartz;

namespace Infrastructure.BackgroundJobs;

[DisallowConcurrentExecution]
public sealed class CartExpirationCleanupJob(
    ICartRepository cartRepository,
    IUnitOfWork unitOfWork) : IJob
{
    private readonly ICartRepository _cartRepository = cartRepository;
    private readonly IUnitOfWork _unitOfWork = unitOfWork;

    public async Task Execute(IJobExecutionContext context)
    {
        //_cartRepository.GetDeleteExpiredAsync();

        await Console.Out.WriteLineAsync("Hello world!");
    }
}
