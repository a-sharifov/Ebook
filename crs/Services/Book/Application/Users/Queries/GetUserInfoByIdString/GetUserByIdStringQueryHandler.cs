using Application.Users.Common;
using Application.Users.Queries.GetUserInfoById;
using MediatR;

namespace Application.Users.Queries.GetUserInfoByIdString;

internal sealed class GetUserByIdStringQueryHandler(ISender sender) : 
    IQueryHandler<GetUserInfoByIdStringQuery, UserDto>
{
    private readonly ISender _sender = sender;

    public async Task<Result<UserDto>> Handle(GetUserInfoByIdStringQuery request, CancellationToken cancellationToken)
    {
        var userId = Guid.Parse(request.Id);
        var query = new GetUserInfoByIdQuery(userId);
        return await _sender.Send(query, cancellationToken);
    }
}
