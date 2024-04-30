using Domain.CartAggregate.Repositories;
using Domain.UserAggregate.Ids;

namespace Application.Carts.Queries.GetQuantityBookCart;

internal sealed class GetQuantityCartBookQueryHandler(ICartRepository repository) 
    : IQueryHandler<GetQuantityCartBookQuery, int>
{
    private readonly ICartRepository _repository = repository;

    public async Task<Result<int>> Handle(GetQuantityCartBookQuery request, CancellationToken cancellationToken)
    {
        var userId = new UserId(request.UserId);
        var quantity = await _repository.GetTotalQuantityBookAsync(userId, cancellationToken);
        return quantity;
    }
}
