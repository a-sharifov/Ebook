using Domain.UserAggregate.Errors;
using Domain.UserAggregate.Ids;
using Domain.UserAggregate.Repositories;

namespace Application.Users.Commands.DeleteUser;

internal sealed class DeleteUserCommandHandler(
    IUserRepository repository) 
    : ICommandHandler<DeleteUserCommand>
{
    private readonly IUserRepository _repository = repository;

    public async Task<Result> Handle(DeleteUserCommand request, CancellationToken cancellationToken)
    {
        var userId = new UserId(request.UserId);

        var isExists = await _repository.IsExistsAsync(userId, cancellationToken);

        if (!isExists)
        {
            return Result.Failure(
                UserErrors.UserDoesNotExist);
        }

        await _repository.DeleteByIdAsync(userId, cancellationToken);

        return Result.Success();
    }
}
