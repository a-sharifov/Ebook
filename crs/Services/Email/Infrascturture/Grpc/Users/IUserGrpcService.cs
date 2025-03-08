using Users.Protobuf;

namespace Infrasctructure.Grpc.Users;

public interface IUserGrpcService
{
    public Task<UserDetails> GetUserInfoAsync(Guid id, CancellationToken cancellationToken = default);
}
