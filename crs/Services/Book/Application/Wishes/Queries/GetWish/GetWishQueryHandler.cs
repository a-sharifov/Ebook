using Application.Common.DTOs.Wishes;
using Domain.UserAggregate.Ids;
using Domain.WishAggregate.Repositories;

namespace Application.Wishes.Queries.GetWish;

internal sealed class GetWishQueryHandler(IWishRepository repository) : IQueryHandler<GetWishQuery, WishDto>
{
    private readonly IWishRepository _repository = repository;

    public async Task<Result<WishDto>> Handle(GetWishQuery request, CancellationToken cancellationToken)
    {
        var userId = new UserId(request.UserId);

        var wish = await _repository.GetAsync(userId, cancellationToken);
        var wishDto = wish.Adapt<WishDto>();

        return wishDto;
    }
}
