using Application.Common.DTOs.Users;
using Application.Users.Queries.GetUserById;
using Domain.UserAggregate.Errors;
using Domain.UserAggregate.Ids;
using Domain.UserAggregate.Repositories;

namespace Application.Users.Queries.GetUserDetailsById;

internal sealed class GetUserByIdQueryHandler(IUserRepository repository) 
    : IQueryHandler<GetUserDetailsByIdQuery, UserDetailsDto>
{
    private readonly IUserRepository _repository = repository;

    public async Task<Result<UserDetailsDto>> Handle(GetUserDetailsByIdQuery request, CancellationToken cancellationToken)
    {
        var userId = new UserId(request.Id);
        var isExist = await _repository.IsExistAsync(userId, cancellationToken);

        if (!isExist)
        {
            return Result.Failure<UserDetailsDto>(
                UserErrors.UserDoesNotExist);
        }

        var user = await _repository.GetAsync(userId, cancellationToken: cancellationToken);

        var userDetailsDto = user.Adapt<UserDetailsDto>();

        return userDetailsDto;
    }
}
