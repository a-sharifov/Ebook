using Application.Common.DTOs.Carts;
using Domain.CartAggregate.Repositories;
using Domain.UserAggregate.Ids;

namespace Application.Carts.Queries.GetCart;

internal sealed class GetCartQueryHandler(ICartRepository repository) 
    : IQueryHandler<GetCartQuery, CartDto>
{
    private readonly ICartRepository _repository = repository;

    public async Task<Result<CartDto>> Handle(GetCartQuery request, CancellationToken cancellationToken)
    {
        var userId = new UserId(request.UserId);
        var cart = await _repository.GetAsync(userId, cancellationToken);
        var cartDto = cart.Adapt<CartDto>();

        return cartDto;
    }
}