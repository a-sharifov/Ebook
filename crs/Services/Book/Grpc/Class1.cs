using Application.Users.Queries.GetUserById;
using Grpc.Core;
using MediatR;
using Users.Protobuf;

namespace Grpc;

public sealed class UserGrpcService(ISender sender) : UserService.UserServiceBase
{
    public override async Task<UserDetails> GetUserDetails(GetUserDetailsRequest request, ServerCallContext context)
    {
        var id = Guid.Parse(request.Id);
        var query = new GetUserByIdQuery(id);

        var result = await sender.Send(query, context.CancellationToken);

        if (result.IsFailure)
        {
            throw new RpcException(new Status(StatusCode.NotFound, result.Error));
        }

        var user = result.Value;

        var userDetails = new UserDetails() {
            Id = user.Id,
            FirstName = user.FirstName,
            LastName = user.LastName,
            Email = user.Email,
            Role = user.Role,
            EmailConfirmationToken = user.EmailConfirmationToken,
            ResetPasswordToken = user.ResetPasswordToken,
            IsEmailConfirmed = user.IsEmailConfirmed,
            };

        return userDetails;
    }
}

