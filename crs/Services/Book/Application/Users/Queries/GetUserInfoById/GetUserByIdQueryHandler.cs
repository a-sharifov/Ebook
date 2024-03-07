using Application.Users.Common;
using Domain.UserAggregate.Errors;
using Domain.UserAggregate.Ids;
using Domain.UserAggregate.Repositories;

namespace Application.Users.Queries.GetUserInfoById;

internal sealed class GetUserByIdQueryHandler(IUserRepository userRepository) 
    : IQueryHandler<GetUserInfoByIdQuery, UserDto>
{
    private readonly IUserRepository _userRepository = userRepository;

    public async Task<Result<UserDto>> Handle(GetUserInfoByIdQuery request, CancellationToken cancellationToken)
    {
        var userId = new UserId(request.Id);

        var user = await _userRepository.GetByIdAsync(userId, cancellationToken);

        if (user is null)
        {
            return Result.Failure<UserDto>(
                UserErrors.UserDoesNotExist);
        }

        var userInfo = new UserDto(
            user.Id.Value, 
            user.Email.Value, 
            user.FirstName.Value,
            user.LastName.Value,
            user.IsEmailConfirmed,
            user.Role.Name,
            user.Gender.Name);

        return Result.Success(userInfo);
    }
}
