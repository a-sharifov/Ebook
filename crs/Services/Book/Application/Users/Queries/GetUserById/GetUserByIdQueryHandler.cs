using Application.Common.DTOs.Users;
using Domain.UserAggregate.Errors;
using Domain.UserAggregate.Ids;
using Domain.UserAggregate.Repositories;

namespace Application.Users.Queries.GetUserById;

internal sealed class GetUserByIdQueryHandler(IUserRepository repository) 
    : IQueryHandler<GetUserByIdQuery, UserDto>
{
    private readonly IUserRepository _repository = repository;

    public async Task<Result<UserDto>> Handle(GetUserByIdQuery request, CancellationToken cancellationToken)
    {
        var userId = new UserId(request.Id);
        var isExist = await _repository.IsExistAsync(userId, cancellationToken);

        if (!isExist)
        {
            return Result.Failure<UserDto>(
                UserErrors.UserDoesNotExist);
        }

        var user = await _repository.GetAsync(userId, cancellationToken: cancellationToken);

        var userDto = user.Adapt<UserDto>();

        return userDto;
    }
}
