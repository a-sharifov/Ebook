using Users.Protobuf;
using static Users.Protobuf.UserService;

namespace Infrasctructure.Grpc.Users;

public sealed class UserGrpcService(UserServiceClient client) : IUserGrpcService
{
    private readonly UserServiceClient _client = client;

    public async Task<UserDetails> GetUserInfoAsync(Guid id, CancellationToken cancellationToken = default)
    {
        var request = new GetUserDetailsRequest() { Id = id.ToString() };
        return await _client.GetUserDetailsAsync(request, cancellationToken: cancellationToken);
    }
}