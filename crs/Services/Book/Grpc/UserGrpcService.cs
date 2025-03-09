using Application.Users.Queries.GetUserById;
using Domain.UserAggregate;
using Grpc.Core;
using MediatR;
using Users.Protobuf;

namespace Grpc;

public sealed class UserGrpcService(ISender sender) : UserService.UserServiceBase
{
    public override async Task<UserDetails> GetUserDetails(GetUserDetailsRequest request, ServerCallContext context)
    {
        var id = Guid.Parse(request.Id);
        var query = new GetUserDetailsByIdQuery(id);

        var result = await sender.Send(query, context.CancellationToken);

        if (result.IsFailure)
        {
            throw new RpcException(new Status(StatusCode.NotFound, result.Error));
        }

        var userDetails = result.Value;

        var response = new UserDetails() {
            Id = userDetails.Id.ToString(),
            FirstName = userDetails.FirstName,
            LastName = userDetails.LastName,
            Email = userDetails.Email,
            Role = Enum.Parse<Role>(userDetails.Role),
            EmailConfirmationToken = userDetails.EmailConfirmationToken ?? string.Empty,
            ResetPasswordToken = userDetails.ResetPasswordToken ?? string.Empty,
            IsEmailConfirmed = userDetails.IsEmailConfirmed,
            };


        return response;
    }
}

