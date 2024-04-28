using Domain.Core.UnitOfWorks.Interfaces;
using Domain.UserAggregate.Errors;
using Domain.UserAggregate.Ids;
using Domain.UserAggregate.Repositories;

namespace Application.Users.Commands.DeleteUser;

internal sealed class DeleteUserCommandHandler(
    IUserRepository repository, 
    IUnitOfWork unitOfWork)
    : ICommandHandler<DeleteUserCommand>
{
    private readonly IUserRepository _repository = repository;
    private readonly IUnitOfWork _unitOfWork = unitOfWork;

    public async Task<Result> Handle(DeleteUserCommand request, CancellationToken cancellationToken)
    {
        var userId = new UserId(request.UserId);

        var isExists = await _repository.IsExistAsync(userId, cancellationToken);

        if (!isExists)
        {
            return Result.Failure(
                UserErrors.UserDoesNotExist);
        }

        await _repository.DeleteAsync(userId, cancellationToken);

        return Result.Success();
    }
}
